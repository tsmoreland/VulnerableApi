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
using Swashbuckle.Swagger.Annotations;
using Vulnerable.Application.Models.Queries;
using Vulnerable.Application.Queries.Countries;
using Vulnerable.Net48.Api.Filters;
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
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
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
        [Route("api/countries")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "names", typeof(PagedNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
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
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetCountryByName(string name) =>
            Ok(await _mediator.Send(new GetCountryByNameQuery(name)));

        /// <summary>
        /// Get all Country Names
        /// </summary>
        /// <response code="200">country names</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/countries/names")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "country names", typeof(PagedNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetCountryNames(int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetCountryNamesQuery(pageNumber, pageSize)));

        /// <summary>
        /// Returns countries with continent id matching <paramref name="id"/>
        /// </summary>
        /// <response code="200">country details</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/continents/{id:int}/countries")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "countries", typeof(PagedCountryViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetCountriesByContinentId(int id, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetCountriesByContinentIdQuery(id, pageNumber, pageSize)));

        /// <summary>
        /// Returns countries with continent name matching <paramref name="name"/>
        /// </summary>
        /// <response code="200">country details</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/continents/{name}/countries")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "countries", typeof(PagedCountryViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetCountriesByContinentName(string name, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetCountriesByContinentNameQuery(name, pageNumber, pageSize)));
    }
}