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

using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using Vulnerable.Domain.Queries;
using Vulnerable.Domain.Queries.Cities;
using Vulnerable.Domain.Queries.Countries;
using Vulnerable.Domain.Queries.Provinces;
using Vulnerable.Net48.Api.Filters;
using Vulnerable.Net48.Api.Infrastructure.ApiExamples;
using Vulnerable.Net48.Api.Infrastructure.ApiExamples.Countries;
using Vulnerable.Shared.Models;

namespace Vulnerable.Net48.Api.Controllers
{
    /// <summary>
    /// Countries CRUD API Controller
    /// </summary>
    public class CountriesApiController : ApiController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Instantiates a new instance of the <see cref="CountriesApiController"/> class.
        /// </summary>
        public CountriesApiController(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get all countries as name/id pairs
        /// </summary>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200">id/name pairs for all items</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetCountries(int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetCountriesQuery(pageNumber, pageSize)));

        /// <summary>
        /// Get Country names like <paramref name="name"/>
        /// </summary>
        /// <param name="name">name to compare against</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200">item names matching provided name</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries/search/{name}")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "names", typeof(PagedNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetCountryNamesLikeName(string name, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetCountryNamesLikeNameQuery(name, pageNumber, pageSize)));

        /// <summary>
        /// Get countries whose name is like <paramref name="id"/>
        /// </summary>
        /// <response code="200">country details</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries/{id:int}")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "country", typeof(CountryViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(CountryViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetCountryById(int id) =>
            Ok(await _mediator.Send(new GetCountryByIdQuery(id)));

        /// <summary>
        /// Get countries whose name is like <paramref name="name"/>
        /// </summary>
        /// <response code="200">country details</response>
        /// <response code="404">item not found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries/{name}")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "country", typeof(CountryViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(CountryViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetCountryByName(string name) =>
            Ok(await _mediator.Send(new GetCountryByNameQuery(name)));

        /// <summary>
        /// returns all cities matching <paramref name="countryId"/>
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="pageNumber">optional page number, by default page 1</param>
        /// <param name="pageSize">optional page size, by default all results</param>
        /// <returns>paged city details</returns>
        /// <response code="200">cities belonging to country</response>
        /// <response code="404">not matching cities</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries/{countryId:int}/cities")]
        [HttpGet]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "cities belonging to requested country", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetCitiesByCountryId(int countryId, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByCountryIdQuery(countryId, pageNumber, pageSize)));
        }

        /// <summary>
        /// returns all cities matching <paramref name="countryName"/>
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="pageNumber">optional page number, by default page 1</param>
        /// <param name="pageSize">optional page size, by default all results</param>
        /// <returns>paged city details</returns>
        /// <response code="200">cities belonging to country</response>
        /// <response code="404">not matching cities</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries/{countryName}/cities")]
        [HttpGet]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "cities belonging to requested country", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetCitiesByCountryName(string countryName, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceNameQuery(countryName, pageNumber, pageSize)));
        }


        /// <summary>
        /// returns all province details for provinces matching country
        /// </summary>
        /// <param name="countryId">unique identifier to match country</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200">province details</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries/{id:int}/provinces")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "provinces", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetProvincesByCountryId(int countryId, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetProvincesByCountryIdQuery(countryId, pageNumber, pageSize)));

        /// <summary>
        /// returns all province details for provinces matching country
        /// </summary>
        /// <param name="name">unique identifier to match country</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200"></response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries/{name}/provinces")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "provinces", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetProvincesByCountryName(string name, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetProvincesByCountryNameQuery(name, pageNumber, pageSize)));
    }
}