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
using Vulnerable.Domain.Queries.Provinces;
using Vulnerable.Net48.Api.Filters;
using Vulnerable.Net48.Api.Infrastructure.ApiExamples;
using Vulnerable.Net48.Api.Infrastructure.ApiExamples.Provinces;
using Vulnerable.Shared.Models;

namespace Vulnerable.Net48.Api.Controllers
{
    /// <summary>
    /// API methods returning either Provinces or province names
    /// </summary>
    public sealed class ProvincesApiController : ApiController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Instantiates a new instance of the <see cref="ProvincesApiController"/> class
        /// </summary>
        public ProvincesApiController(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get id/name pairs of all provinces
        /// </summary>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200">id/name pairs for all items</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/provinces")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetProvinces(int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetProvincesQuery(pageNumber, pageSize)));

        /// <summary>
        /// Returns all province names matching <paramref name="name"/> 
        /// </summary>
        /// <param name="name">name to compare against</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200">item names matching provided name</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/provinces/search/{name}")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "names", typeof(PagedNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetProvinceNamesLikeName(string name, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetProvinceNamesLikeNameQuery(name, pageNumber, pageSize)));

        /// <summary>
        /// returns province matching <paramref name="id"/>
        /// </summary>
        /// <response code="200">province details</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/provinces/{id:int}")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "province", typeof(ProvinceViewModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(ProvinceViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(ProvinceViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetProvinceById(int id) =>
            Ok(await _mediator.Send(new GetProvinceByIdQuery(id)));

        /// <summary>
        /// returns province matching <paramref name="name"/>
        /// </summary>
        /// <response code="200">province details</response>
        /// <response code="404">item not found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/provinces/{name}")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "province", typeof(ProvinceViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(ProvinceViewModel))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetProvinceByName(string name) =>
            Ok(await _mediator.Send(new GetProvinceByNameQuery(name)));

        /// <summary>
        /// returns all cities matching <paramref name="provinceId"/>
        /// </summary>
        /// <param name="provinceId"></param>
        /// <param name="pageNumber">optional page number, by default page 1</param>
        /// <param name="pageSize">optional page size, by default all results</param>
        /// <returns>paged city details</returns>
        /// <response code="200">cities belonging to province</response>
        /// <response code="404">not matching cities</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/provinces/{provinceId:int}/cities")]
        [HttpGet]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "cities belonging to requested province", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetCitiesByProvinceId(int provinceId, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceIdQuery(provinceId, pageNumber, pageSize)));
        }

        /// <summary>
        /// returns all cities matching <paramref name="provinceName"/>
        /// </summary>
        /// <param name="provinceName"></param>
        /// <param name="pageNumber">optional page number, by default page 1</param>
        /// <param name="pageSize">optional page size, by default all results</param>
        /// <returns>paged city details</returns>
        /// <response code="200">cities belonging to province</response>
        /// <response code="404">not matching cities</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/provinces/{provinceName}/cities")]
        [HttpGet]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "cities belonging to requested province", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PagedIdNameViewModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(ProblemDetailsModelExamples))]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(ProblemDetailsModelExamples))]
        public async Task<IHttpActionResult> GetCitiesByProvinceName(string provinceName, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceNameQuery(provinceName, pageNumber, pageSize)));
        }

    }
}