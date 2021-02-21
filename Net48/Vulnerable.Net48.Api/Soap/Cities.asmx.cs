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

using System.Configuration;
using System.Threading;
using System.Web.Mvc;
using System.Web.Services;
using MediatR;
using Vulnerable.Application.Models.Queries;
using Vulnerable.Application.Queries.Cities;

namespace Vulnerable.Net48.Api.Soap
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

        public Address()
        {
            _mediator = DependencyResolver.Current.GetService(typeof(IMediator)) as IMediator ??
                        throw new ConfigurationErrorsException("Unable to load IMediator from IoC container");
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public PagedNameViewModel GetAllCityNames(int pageNumber, int pageSize)
        {
            var task = _mediator.Send(new GetAllCityNamesQuery(pageNumber, pageSize));

            while (!task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                Thread.Sleep(1000);

            return task.Result;
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
