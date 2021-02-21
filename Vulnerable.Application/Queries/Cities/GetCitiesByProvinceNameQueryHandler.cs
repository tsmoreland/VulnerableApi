﻿//
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

using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Application.Models.Queries;
using Vulnerable.Shared;

namespace Vulnerable.Application.Queries.Cities
{
    public sealed class GetCitiesByProvinceNameQueryHandler : IRequestHandler<GetCitiesByProvinceNameQuery, PagedCityViewModel>
    {
        private readonly ICityRepository _repository;
        private readonly IMapper _mapper;

        public GetCitiesByProvinceNameQueryHandler(ICityRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public Task<PagedCityViewModel> Handle(GetCitiesByProvinceNameQuery request, CancellationToken cancellationToken)
        {
            var provinceName = request.Name;
            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;

            var fetchTask = _repository.GetCitiesBy(c => c.Province != null && c.Province.Name == provinceName, pageNumber, pageSize);
            var countTask = _repository.GetTotalCountOfCitiesBy(c => c.Province != null && c.Province.Name == provinceName);
            return Task
                .WhenAll(fetchTask, countTask)
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);

                    return new PagedCityViewModel
                    {
                        Count = countTask.Result,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Items = _mapper.Map<List<CityViewModel>>(fetchTask.Result.ToList())
                    };
                }, cancellationToken);
        }
    }
}
