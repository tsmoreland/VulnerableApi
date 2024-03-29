﻿//
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

using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Domain.Contracts.Queries;
using Vulnerable.Domain.Queries;
using Vulnerable.Domain.Queries.Cities;
using Vulnerable.Shared;
using Vulnerable.Shared.Extensions;

namespace Vulnerable.Application.Queries.Cities
{
    public sealed class GetCityNamesLikeNameQueryHandler : IRequestHandler<GetCityNamesLikeNameQuery, PagedNameViewModel>
    {
        private readonly ICityRepository _cityRepository;

        public GetCityNamesLikeNameQueryHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        public Task<PagedNameViewModel> Handle(GetCityNamesLikeNameQuery request, CancellationToken cancellationToken)
        {
            GuardAgainst.NullOrEmpty(request.Name, "name");
            GuardAgainst.LessThanOrEqualToZero(request.PageNumber, "pageNumber");
            GuardAgainst.LessThanOrEqualToZero(request.PageSize, "pageSize");

            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;
            return _cityRepository.GetCityNamesLikeName(request.Name, pageNumber, pageSize)
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    int count = _cityRepository.GetTotalCountOfCityNamesLikeName(request.Name).ResultIfGreaterThanZero(cancellationToken);
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
