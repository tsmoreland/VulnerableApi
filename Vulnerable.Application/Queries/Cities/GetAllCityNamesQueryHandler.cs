//
// Copyright © 2021 Terry Moreland
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
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Application.Models.Queries;
using Vulnerable.Shared;
using Vulnerable.Shared.Extensions;

namespace Vulnerable.Application.Queries.Cities
{
    public sealed class GetAllCityNamesQueryHandler : IRequestHandler<GetAllCityNamesQuery, PagedNameViewModel>
    {
        private readonly ICityRepository _repository;

        public GetAllCityNamesQueryHandler(ICityRepository cityRepository)
        {
            _repository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        public Task<PagedNameViewModel> Handle(GetAllCityNamesQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;

            GuardAgainst.LessThanOrEqualToZero(pageNumber, nameof(pageNumber));
            GuardAgainst.LessThanOrEqualToZero(pageSize, nameof(pageSize));

            return _repository.GetAllCityNames(pageNumber, pageSize)
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    var items = t.Result;

                    // would prefer to go parallel but entityframework doesn't support parallel operations against 
                    // the same dbContext, at least EF6 doesn't
                    var count = _repository.GetTotalCountOfCities().ResultIfGreaterThanZero(cancellationToken);

                    return new PagedNameViewModel
                    {
                        Count = count,
                        PageNumber =pageNumber,
                        PageSize = pageSize,
                        Items = items.ToList()
                    };
                }, cancellationToken);
        }
    }
}
