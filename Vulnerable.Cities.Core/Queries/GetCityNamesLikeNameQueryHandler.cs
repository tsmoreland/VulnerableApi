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

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Vulnerable.Cities.Core.Contracts.Data;
using Vulnerable.Shared.Exceptions;

namespace Vulnerable.Cities.Core.Queries
{
    public sealed class GetCityNamesLikeNameQueryHandler : IRequestHandler<GetCityNameLikeNameQuery, PagedCityNameViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public GetCityNamesLikeNameQueryHandler(IMapper mapper, ICityRepository cityRepository)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _cityRepository = cityRepository ?? throw new System.ArgumentNullException(nameof(cityRepository));
        }

        public Task<PagedCityNameViewModel> Handle(GetCityNameLikeNameQuery request, CancellationToken cancellationToken)
        {
            return _cityRepository
                .GetCityNamesLikeName(request.Name, request.PageNumber, request.PageSize)
                .ContinueWith(t => 
                { 
                    if (t.IsCanceled)
                        throw new BadRequestException("Request has been cancelled");
                    if (t.IsFaulted)
                        throw new InternalServerErrorException("error occurred processing request", t.Exception);

                    return _mapper.Map<PagedCityNameViewModel>(t.Result);

                }, cancellationToken);
        }
    }
}
