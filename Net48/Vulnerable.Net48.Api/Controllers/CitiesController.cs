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

using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MediatR;
using Vulnerable.Application.Models.Queries;
using Vulnerable.Application.Queries.Cities;
using Vulnerable.Net48.Api.Filters;

namespace Vulnerable.Net48.Api.Controllers
{
    /// <summary>
    /// API methods returning either cities or city names
    /// </summary>
    [ApiExceptionFilter]
    public sealed class CitiesController : ApiController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Instaantiates a new instance of the <see cref="CitiesController"/> class
        /// </summary>
        public CitiesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets all the cities names
        /// </summary>
        /// <param name="pageNumber">optional page number, by default page 1</param>
        /// <param name="pageSize">optional page size, by default all results</param>
        /// <response code="200"></response>
        [Route("api/cities")]
        [HttpGet]
        [ResponseType(typeof(PagedNameViewModel))]
        public async Task<IHttpActionResult> GetCities(int pageNumber = 1, int pageSize = int.MaxValue) =>
            Ok(await _mediator.Send(new GetCitiesQuery(pageNumber, pageSize)));

        /// <summary>
        /// Gets all the cities ids and names
        /// </summary>
        /// <param name="pageNumber">optional page number, by default page 1</param>
        /// <param name="pageSize">optional page size, by default all results</param>
        /// <response code="200"></response>
        [Route("api/cities/name")]
        [HttpGet]
        [ResponseType(typeof(PagedNameViewModel))]
        public async Task<IHttpActionResult> GetAllCityNames(int pageNumber = 1, int pageSize = int.MaxValue) =>
            Ok(await _mediator.Send(new GetAllCityNamesQuery(pageNumber, pageSize)));

        /// <summary>
        /// Returns City matching <paramref name="id"/>
        /// </summary>
        /// <param name="id">id of the city to get</param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [Route("api/cities/{id:int}")]
        [HttpGet]
        [ResponseType(typeof(CityViewModel))]
        public async Task<IHttpActionResult> GetCityById(int id) =>
            Ok(await _mediator.Send(new GetCityByIdQuery(id)));

        /// <summary>
        /// Returns City matching <paramref name="name"/>
        /// </summary>
        /// <param name="name">name of the city to get</param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [Route("api/cities/{name}")]
        [HttpGet]
        [ResponseType(typeof(CityViewModel))]
        public async Task<IHttpActionResult> GetCityByName(string name) =>
            Ok(await _mediator.Send(new GetCityByNameQuery(name)));

        [Route("api/cities")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCityNamesLikeName(string name, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCityNameLikeNameQuery(name, pageNumber, pageSize)));
        }

        [Route("api/countries/{countryId:int}/cities")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCitiesByCountryId(int countryId, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByCountryIdQuery(countryId, pageNumber, pageSize)));
        }

        [Route("api/countries/{countryName}/cities")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCitiesByCountryName(string countryName, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceNameQuery(countryName, pageNumber, pageSize)));
        }

        [Route("api/provinces/{provinceId:int}/cities")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCitiesByProvinceId(int provinceId, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceIdQuery(provinceId, pageNumber, pageSize)));
        }

        [Route("api/provinces/{provinceName}/cities")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCitiesByProvinceName(string provinceName, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceNameQuery(provinceName, pageNumber, pageSize)));
        }

    }
}