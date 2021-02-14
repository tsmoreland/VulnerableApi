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

using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vulnerable.Cities.Core.Queries;
using Vulnerable.Net5.Soap.Api.ServiceContracts;

namespace Vulnerable.Net5.Soap.Api.Services
{
    public class AddressService : IAddressServiceContact
    {
        private readonly IServiceProvider _serviceProvider;

        public AddressService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <inheritdoc/>
        public PagedCityNameViewModel GetAllCityNames(int pageNumber, int pageSize)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return mediator.Send(new GetAllCityNamesQuery(pageNumber, pageSize)).Result;
        }

        /// <inheritdoc/>
        public GetCityByNameViewModel GetCityByName(string name)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            return mediator.Send(new GetCityByNameQuery(name)).Result;
        }

        /// <inheritdoc/>
        public PagedCityNameViewModel GetCityNamesLikeName(string name, int pageNumber, int pageSize)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return mediator.Send(new GetCityNameLikeNameQuery(name, pageNumber, pageSize)).Result;
        }
    }
}
