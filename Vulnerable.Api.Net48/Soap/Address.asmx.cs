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
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services;
using MediatR;
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Application.Models.Queries;
using Vulnerable.Application.Queries.Cities;
using Vulnerable.Infrastructure.Data.Net48;

namespace Vulnerable.Api.Net48.Soap
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    [WebService(Namespace = "http://core.soap.vulnerable-api.org:4993/soap/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Address : WebService
    {
        private readonly IMediator _mediator;
        private readonly ICityRepository _cityRepository;

        public Address()
        {
            try
            {
                var addresses = Dns.GetHostAddresses("vulnsqlserver");
                if (addresses?.Any() == true)
                {
                }
            }
            catch (Exception)
            {
                // ...
            }

            DataInitializer.SetupToReset();
            if (!(DependencyResolver.Current.GetService(typeof(AddressDbContext)) is AddressDbContext context))
                return; // should probably throw exception instead

            DataInitializer.Seed(context);

            _mediator = DependencyResolver.Current.GetService(typeof(IMediator)) as IMediator ??
                        throw new ConfigurationErrorsException("Unable to load IMediator from IoC container");
            _cityRepository = DependencyResolver.Current.GetService(typeof(ICityRepository)) as ICityRepository;

        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public PagedNameViewModel GetAllCityNames(int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetAllCityNamesQuery(pageNumber, pageSize)).Result;
        }

        [WebMethod]
        public CityViewModel GetCityByName(string name)
        {
            return _mediator.Send(new GetCityByNameQuery(name)).Result;
        }

        [WebMethod]
        public PagedNameViewModel GetCityNamesLikeName(string name, int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetCityNameLikeNameQuery(name, pageNumber, pageSize)).Result;
        }

    }
}
