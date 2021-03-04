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

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vulnerable.Domain.Contracts.Queries;
using Vulnerable.Domain.Queries;
using Vulnerable.Domain.Queries.Countries;
using Vulnerable.Shared;
using Vulnerable.Shared.Extensions;

namespace Vulnerable.Application.Queries.Countries
{
    public sealed class GetCountryNamesLikeNameQueryHandler : IRequestHandler<GetCountryNamesLikeNameQuery, PagedNameViewModel>
    {
        private readonly ICountryRepository _repository;

        public GetCountryNamesLikeNameQueryHandler(ICountryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<PagedNameViewModel> Handle(GetCountryNamesLikeNameQuery request, CancellationToken cancellationToken)
        {
            var (name, pageNumber, pageSize) = request;
            return _repository.GetCountryNamesLikeName(name, pageNumber, pageSize)
                .ContinueWith(fetchTask =>
                {
                    GuardAgainst.FaultedOrCancelled(fetchTask);
                    var count = _repository.GetTotalCountOfCountryNamesLikeName(name).ResultIfGreaterThanZero(cancellationToken);
                    return new PagedNameViewModel
                    {
                        Count = count,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Items = fetchTask.Result.ToList()
                    };
                }, cancellationToken);
        }
    }
}
