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
using Vulnerable.Application.Models.Queries;
using Vulnerable.Application.Queries.Cities;
using Vulnerable.Shared.Extensions;

namespace Vulnerable.Net48.Api.Soap
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    [WebService(Namespace = "http://vulnerableapp.com:8000/soap/CitiesService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class CitiesService : WebService
    {
        private readonly IMediator _mediator;

        public CitiesService()
        {
            _mediator = DependencyResolver.Current.GetService(typeof(IMediator)) as IMediator ??
                        throw new ConfigurationErrorsException("Unable to load IMediator from IoC container");
        }

        [WebMethod]
        public PagedNameViewModel GetAllCityNames(int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetAllCityNamesQuery(pageNumber, pageSize))
                .ResultOrThrow();

        [WebMethod]
        public CityViewModel GetCityById(int id) =>
            _mediator
                .Send(new GetCityByIdQuery(id))
                .ResultOrThrow();

        [WebMethod]
        public CityViewModel GetCityByName(string name) =>
            _mediator
                .Send(new GetCityByNameQuery(name))
                .ResultOrThrow();

        [WebMethod]
        public PagedNameViewModel GetCityNamesLikeName(string name, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetCityNameLikeNameQuery(name, pageNumber, pageSize))
                .ResultOrThrow();

        [WebMethod]
        public PagedCityViewModel GetCitiesByCountryId(int countryId, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetCitiesByCountryIdQuery(countryId, pageNumber, pageSize))
                .ResultOrThrow();

        [WebMethod]
        public PagedCityViewModel GetCitiesByCountryName(string countryName, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetCitiesByCountryNameQuery(countryName, pageNumber, pageSize))
                .ResultOrThrow();

        [WebMethod]
        public PagedCityViewModel GetCitiesByProvinceId(int provinceId, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetCitiesByProvinceIdQuery(provinceId, pageNumber, pageSize))
                .ResultOrThrow();

        [WebMethod]
        public PagedCityViewModel GetCitiesByProvinceName(string provinceName, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetCitiesByProvinceNameQuery(provinceName, pageNumber, pageSize))
                .ResultOrThrow();

    }
}
