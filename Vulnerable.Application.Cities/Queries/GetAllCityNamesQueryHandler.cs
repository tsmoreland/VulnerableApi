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
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Vulnerable.Application.Cities.Contracts.Data;
using Vulnerable.Shared;

namespace Vulnerable.Application.Cities.Queries
{
    public sealed class GetAllCityNamesQueryHandler : IRequestHandler<GetAllCityNamesQuery, PagedCityNameViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public GetAllCityNamesQueryHandler(IMapper mapper, ICityRepository cityRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        public Task<PagedCityNameViewModel> Handle(GetAllCityNamesQuery request, CancellationToken cancellationToken)
        {
            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;

            GuardAgainst.LessThanOrEqualToZero(pageNumber, nameof(pageNumber));
            GuardAgainst.LessThanOrEqualToZero(pageSize, nameof(pageSize));

            var namesTask = _cityRepository.GetAllCityNames(pageNumber, pageSize);
            var countTask = _cityRepository.GetTotalCountOfCities();

            return Task.WhenAll(namesTask, countTask)
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);

                    return new PagedCityNameViewModel
                    {
                        Count = countTask.Result,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Names = namesTask.Result
                    };
                }, cancellationToken);
        }
    }
}
