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
using MediatR;
using Vulnerable.Cities.Core.Queries;
using Vulnerable.Net5.Soap.Api.ServiceContracts;

namespace Vulnerable.Net5.Soap.Api.Services
{
    public class AddressService : IAddressServiceContact
    {
        private readonly IMediator _mediator;

        public AddressService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <inheritdoc/>
        public PagedCityNameViewModel GetAllCityNames(int pageNumber, int pageSize) =>
            _mediator.Send(new GetAllCityNamesQuery(pageNumber, pageSize)).Result;

        /// <inheritdoc/>
        public GetCityByNameViewModel GetCityByName(string name) =>
            _mediator.Send(new GetCityByNameQuery(name)).Result;

        /// <inheritdoc/>
        public PagedCityNameViewModel GetCityNamesLikeName(string name, int pageNumber, int pageSize) =>
            _mediator.Send(new GetCityNameLikeNameQuery(name, pageNumber, pageSize)).Result;
    }
}
