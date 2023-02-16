//
// Copyright © 2020 Terry Moreland
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Domain.Contracts.Queries;
using Vulnerable.Domain.Queries;
using Vulnerable.Domain.Queries.Continents;
using Vulnerable.Shared;
using Vulnerable.Shared.Extensions;

namespace Vulnerable.Application.Queries.Continents
{
    public sealed class GetContinentsQueryHandler : IRequestHandler<GetContinentsQuery, PagedIdNameViewModel>
    {
        private readonly IContinentRepository _repository;

        public GetContinentsQueryHandler(IContinentRepository repository)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public Task<PagedIdNameViewModel> Handle(GetContinentsQuery request, CancellationToken cancellationToken)
        {
            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;

            GuardAgainst.LessThanOrEqualToZero(pageNumber, nameof(pageNumber));
            GuardAgainst.LessThanOrEqualToZero(pageSize, nameof(pageSize));

            return _repository.GetContinents(pageNumber, pageSize)
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    (int Id, string Name)[]? items = t.Result;

                    // would prefer to go parallel but entityframework doesn't support parallel operations against 
                    // the same dbContext, at least EF6 doesn't
                    int count = _repository.GetTotalCountOfContinents().ResultIfGreaterThanZero(cancellationToken);

                    return new PagedIdNameViewModel
                    {
                        Count = count,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Items = items.Select(tuple => new IdNameViewModel { Id = tuple.Id, Name = tuple.Name}).ToList()
                    };
                }, cancellationToken);

        }
    }
}
