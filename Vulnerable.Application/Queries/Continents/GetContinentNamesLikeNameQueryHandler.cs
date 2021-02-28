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

using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Application.Models.Queries;
using Vulnerable.Shared;
using Vulnerable.Shared.Extensions;

namespace Vulnerable.Application.Queries.Continents
{
    public sealed class GetContinentNamesLikeNameQueryHandler : IRequestHandler<GetContinentNamesLikeNameQuery, PagedNameViewModel>
    {
        private readonly IContinentRepository _repository;

        public GetContinentNamesLikeNameQueryHandler(IContinentRepository repository)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }
        public Task<PagedNameViewModel> Handle(GetContinentNamesLikeNameQuery request, CancellationToken cancellationToken)
        {
            var name = request.Name;
            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;

            GuardAgainst.LessThanOrEqualToZero(pageNumber, nameof(pageNumber));
            GuardAgainst.LessThanOrEqualToZero(pageSize, nameof(pageSize));

            return _repository.GetContinentNamesLikeName(name, pageNumber, pageSize)
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    var count = _repository
                        .GetTotalCountOfContinentNamesLikeName(name)
                        .ResultIfGreaterThanZero(cancellationToken);
                    return new PagedNameViewModel
                    {
                        Count = count,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Items = t.Result.ToList()
                    };
                }, cancellationToken);
        }
    }
}
