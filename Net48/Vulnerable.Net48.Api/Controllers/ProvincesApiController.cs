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
using Vulnerable.Application.Queries.Provinces;
using Vulnerable.Net48.Api.Filters;
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
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetProvinces(int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetProvincesQuery(pageNumber, pageSize)));

        /// <summary>
        /// Returns all province names matching <paramref name="name"/> 
        /// </summary>
        /// <param name="name">name to compare against</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200"></response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "names", typeof(PagedNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetProvinceNamesLikeName(string name, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetProvinceNamesLikeNameQuery(name, pageNumber, pageSize)));

        /// <summary>
        /// returns province matching <paramref name="id"/>
        /// </summary>
        /// <response code="200"></response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "province", typeof(ProvinceViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetProvinceById(int id) =>
            Ok(await _mediator.Send(new GetProvinceByIdQuery(id)));

        /// <summary>
        /// returns province matching <paramref name="name"/>
        /// </summary>
        /// <response code="200"></response>
        /// <response code="404">item not found</response>
        /// <response code="500">unexpected error when processing request</response>
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "province", typeof(ProvinceViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetProvinceByName(string name) =>
            Ok(await _mediator.Send(new GetProvinceByNameQuery(name)));

        /// <summary>
        /// returns all province names
        /// </summary>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200"></response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "province names", typeof(PagedNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetAllProvinceNames(int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetAllProvinceNamesQuery(pageNumber, pageSize)));

        /// <summary>
        /// returns all province details for provinces matching country
        /// </summary>
        /// <param name="countryId">unique identifier to match country</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200"></response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "provinces", typeof(PagedProvinceViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetProvincesByCountryId(int countryId, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetProvincesByCountryIdQuery(countryId, pageNumber, pageSize)));

        /// <summary>
        /// returns all province details for provinces matching country
        /// </summary>
        /// <param name="countryName">unique identifier to match country</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        /// <response code="200"></response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "provinces", typeof(PagedProvinceViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetProvincesByCountryName(string countryName, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetProvincesByCountryNameQuery(countryName, pageNumber, pageSize)));

    }
}