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
using Vulnerable.Application.Models.Queries;
using Vulnerable.Application.Queries.Countries;

namespace Vulnerable.Net48.Api.Soap
{
    /// <summary>
    /// Summary description for CountriesService
    /// </summary>
    [WebService(Namespace = "http://vulnerableapp.com/soap/CountriesService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class CountriesService : WebService
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Instantiates a new instance of the <see cref="CountriesService"/> class.
        /// </summary>
        public CountriesService()
        {
            _mediator = DependencyResolver.Current.GetService(typeof(IMediator)) as IMediator ??
                        throw new ConfigurationErrorsException("Unable to load IMediator from IoC container");
        }

        /// <summary>
        /// Get all countries as name/id pairs
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public PagedIdNameViewModel GetCountries(int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetCountriesQuery(pageNumber, pageSize)).Result;
        }

        /// <summary>
        /// Get Country names like <paramref name="name"/>
        /// </summary>
        [WebMethod]
        public PagedNameViewModel GetCountryNamesLikeName(string name, int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetCountryNamesLikeNameQuery(name, pageNumber, pageSize)).Result;
        }

        /// <summary>
        /// Get countries whose name is like <paramref name="id"/>
        /// </summary>
        [WebMethod]
        public CountryViewModel GetCountryById(int id)
        {
            return _mediator.Send(new GetCountryByIdQuery(id)).Result;
        }

        /// <summary>
        /// Get countries whose name is like <paramref name="name"/>
        /// </summary>
        [WebMethod]
        public CountryViewModel GetCountryByName(string name)
        {
            return _mediator.Send(new GetCountryByNameQuery(name)).Result;
        }

        /// <summary>
        /// Get all Country Names
        /// </summary>
        [WebMethod]
        public PagedNameViewModel GetCountryNames(int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetCountryNamesQuery(pageNumber, pageSize)).Result;
        }

        /// <summary>
        /// Returns countries with continent id matching <paramref name="continentId"/>
        /// </summary>
        [WebMethod]
        public PagedCountryViewModel GetCountriesByContinentId(int continentId, int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetCountriesByContinentIdQuery(continentId, pageNumber, pageSize)).Result;
        }

        /// <summary>
        /// Returns countries with continent name matching <paramref name="continentName"/>
        /// </summary>
        [WebMethod]
        public PagedCountryViewModel GetCountriesByContinentName(string continentName, int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetCountriesByContinentNameQuery(continentName, pageNumber, pageSize)).Result;
        }
    }
}
