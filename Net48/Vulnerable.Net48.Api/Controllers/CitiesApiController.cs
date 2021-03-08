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

using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using Vulnerable.Domain.Queries;
using Vulnerable.Domain.Queries.Cities;
using Vulnerable.Net48.Api.Filters;
using Vulnerable.Net48.Api.Infrastructure.ApiExamples.Cities;
using Vulnerable.Shared.Models;

namespace Vulnerable.Net48.Api.Controllers
{
    /// <summary>
    /// API methods returning either cities or city names
    /// </summary>
    [ApiExceptionFilter]
    public sealed class CitiesApiController : ApiController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Instaantiates a new instance of the <see cref="CitiesApiController"/> class
        /// </summary>
        public CitiesApiController(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets all the cities names
        /// </summary>
        /// <param name="pageNumber">optional page number, by default page 1</param>
        /// <param name="pageSize">optional page size, by default all results</param>
        /// <response code="200">id/name pairs for all items</response>
        /// <response code="404">item not found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/cities")]
        [HttpGet]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "name/id pairs", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        public async Task<IHttpActionResult> GetCities(int pageNumber = 1, int pageSize = int.MaxValue) =>
            Ok(await _mediator.Send(new GetCitiesQuery(pageNumber, pageSize)));

        /// <summary>
        /// Returns City matching <paramref name="id"/>
        /// </summary>
        /// <param name="id">id of the city to get</param>
        /// <response code="200">city</response>
        /// <response code="404">city not found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/cities/{id:int}")]
        [HttpGet]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "city object", typeof(CityViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetCityById(int id) =>
            Ok(await _mediator.Send(new GetCityByIdQuery(id)));

        /// <summary>
        /// Returns City matching <paramref name="name"/>
        /// </summary>
        /// <param name="name">name of the city to get</param>
        /// <response code="200">city</response>
        /// <response code="404">city not found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/cities/{name}")]
        [HttpGet]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "city object", typeof(CityViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetCityByName(string name) =>
            Ok(await _mediator.Send(new GetCityByNameQuery(name)));

        /// <summary>
        /// returns city names similar to <paramref name="name"/>
        /// </summary>
        /// <param name="name">name to match others against</param>
        /// <param name="pageNumber">optional page number, by default page 1</param>
        /// <param name="pageSize">optional page size, by default all results</param>
        /// <returns>city names matching <paramref name="name"/></returns>
        /// <response code="200">similar city names</response>
        /// <response code="404">not matching cities</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/cities/search/{name}")]
        [HttpGet]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "similar city names", typeof(PagedNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedNameViewModelExamples))]
        public async Task<IHttpActionResult> GetCityNamesLikeName(string name, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCityNamesLikeNameQuery(name, pageNumber, pageSize)));
        }

    }
}