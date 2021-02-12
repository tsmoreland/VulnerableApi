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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Application.Features.Address.Queries.Cities
{
    public sealed class GetCityByNameQueryHandler : IRequestHandler<GetCityByNameQuery, CityViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public GetCityByNameQueryHandler(IMapper mapper, ICityRepository cityRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        /// <inheritdoc/>
        public async Task<CityViewModel> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
        {
            // TODO: rework so we don't need async/await ensuring the exception bubbles up
            var city = await _cityRepository.GetCityByName(request.Name); // TODO: update interface to accept cancellation token
            if (city == null)
                throw new KeyNotFoundException(nameof(City));  // add NotFoundException and pass nameof(City) + request.Name
            return _mapper.Map<CityViewModel>(city);

        }
    }
}
