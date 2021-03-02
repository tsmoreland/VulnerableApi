//
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

using System.Configuration;
using System.Web.Mvc;
using System.Web.Services;
using MediatR;
using Vulnerable.Domain.Queries;
using Vulnerable.Domain.Queries.Continents;
using Vulnerable.Shared.Extensions;

namespace Vulnerable.Net48.Api.Soap
{
    /// <summary>
    /// Summary description for ContinentsService
    /// </summary>
    [WebService(Namespace = "http://vulnerableapp.com/soap/ContinentsService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ContinentsService : WebService
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Instantiates a new instance of the <see cref="ContinentsService"/> class.
        /// </summary>
        public ContinentsService()
        {
            _mediator = DependencyResolver.Current.GetService(typeof(IMediator)) as IMediator ??
                        throw new ConfigurationErrorsException("Unable to load IMediator from IoC container");
            
        }

        /// <summary>
        /// Get the name and id of all continents
        /// </summary>
        [WebMethod]
        public PagedIdNameViewModel GetContinents(int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetContinentsQuery(pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// Returns all city names like name
        /// </summary>
        [WebMethod]
        public PagedNameViewModel GetContinentNamesLikeName(string name, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetContinentNamesLikeNameQuery(name, pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// Returns continent matching <paramref name="id"/>
        /// </summary>
        [WebMethod]
        public ContinentViewModel GetContinentById(int id) =>
            _mediator
                .Send(new GetContinentByIdQuery(id))
                .ResultOrThrow();

        /// <summary>
        /// Returns continent matching <paramref name="name"/>
        /// </summary>
        [WebMethod]
        public ContinentViewModel GetContinentByName(string name) =>
            _mediator
                .Send(new GetContinentByNameQuery(name))
                .ResultOrThrow();
    }
}
