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
using System.Web.Mvc;
using System.Web.Services;
using MediatR;
using Vulnerable.Domain.Entities;
using Vulnerable.Shared.Extensions;
using Commands = Vulnerable.Domain.Commands;
using CityCommands = Vulnerable.Domain.Commands.Cities;
using Queries = Vulnerable.Domain.Queries;
using CityQueries = Vulnerable.Domain.Queries.Cities;

namespace Vulnerable.Net48.Api.Soap
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    [WebService(Namespace = "http://vulnerableapp.com/soap/CitiesService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class CitiesService : WebService
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Instantiates a new instance of the <see cref="CitiesService"/> class.
        /// </summary>
        public CitiesService()
        {
            _mediator = DependencyResolver.Current.GetService(typeof(IMediator)) as IMediator ??
                        throw new ConfigurationErrorsException("Unable to load IMediator from IoC container");
        }

        /// <summary>
        /// Add a new city 
        /// </summary>
        /// <param name="city">city to add</param>
        [WebMethod]
        public Commands.CreateResultViewModel<City> CreateCity(CityCommands.CityCreateModel city) =>
            _mediator
                .Send(new CityCommands.CreateCityCommand(city))
                .ResultOrThrow();

        /// <summary>
        /// Retuns all cities as name, id pairs
        /// </summary>
        [WebMethod]
        public Queries.PagedIdNameViewModel GetCities(int pageNumber, int pageSize) =>
            _mediator
                .Send(new CityQueries.GetCitiesQuery(pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// Returns City matching <paramref name="id"/> or error if not found
        /// </summary>
        [WebMethod]
        public CityQueries.CityViewModel GetCityById(int id) =>
            _mediator
                .Send(new CityQueries.GetCityByIdQuery(id))
                .ResultOrThrow();

        /// <summary>
        /// Returns City matching <paramref name="name"/> or error if not found
        /// </summary>
        [WebMethod]
        public CityQueries.CityViewModel GetCityByName(string name) =>
            _mediator
                .Send(new CityQueries.GetCityByNameQuery(name))
                .ResultOrThrow();

        /// <summary>
        /// Return all city names that are like <paramref name="name"/>
        /// </summary>
        [WebMethod]
        public Queries.PagedNameViewModel GetCityNamesLikeName(string name, int pageNumber, int pageSize) =>
            _mediator
                .Send(new CityQueries.GetCityNamesLikeNameQuery(name, pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// Returns all cities with country matching <paramref name="countryId"/>
        /// </summary>
        [WebMethod]
        public Queries.PagedIdNameViewModel GetCitiesByCountryId(int countryId, int pageNumber, int pageSize) =>
            _mediator
                .Send(new CityQueries.GetCitiesByCountryIdQuery(countryId, pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// Returns all cities with country matching <paramref name="countryName"/>
        /// </summary>
        [WebMethod]
        public Queries.PagedIdNameViewModel GetCitiesByCountryName(string countryName, int pageNumber, int pageSize) =>
            _mediator
                .Send(new CityQueries.GetCitiesByCountryNameQuery(countryName, pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// Returns all cities with province matching <paramref name="provinceId"/>
        /// </summary>
        [WebMethod]
        public Queries.PagedIdNameViewModel GetCitiesByProvinceId(int provinceId, int pageNumber, int pageSize) =>
            _mediator
                .Send(new CityQueries.GetCitiesByProvinceIdQuery(provinceId, pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// Returns all cities with province matching <paramref name="provinceName"/>
        /// </summary>
        [WebMethod]
        public Queries.PagedIdNameViewModel GetCitiesByProvinceName(string provinceName, int pageNumber, int pageSize) =>
            _mediator
                .Send(new CityQueries.GetCitiesByProvinceNameQuery(provinceName, pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// Update a new city 
        /// </summary>
        /// <param name="id">id of the city to update</param>
        /// <param name="city">city to add</param>
        [WebMethod]
        public CityQueries.CityViewModel UpdateCity(int id, CityCommands.CityCreateModel city) =>
            _mediator
                .Send(new CityCommands.EditCityCommand(id, city))
                .ResultOrThrow();

        /// <summary>
        /// Deletes a city
        /// </summary>
        /// <param name="id">id of the city to delete</param>
        [WebMethod]
        public void DeleteCity(int id) =>
            _mediator
                .Send(new CityCommands.DeleteCityComamnd(id))
                .ResultOrThrow();
    }
}
