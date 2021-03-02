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
using Vulnerable.Domain.Queries;
using Vulnerable.Domain.Queries.Continents;
using Vulnerable.Net48.Api.Filters;
using Vulnerable.Shared.Models;

namespace Vulnerable.Net48.Api.Controllers
{
    /// <summary>
    /// Continents API
    /// </summary>
    public class ContinentsApiController : ApiController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Instantiates a new instance of the <see cref="ContinentsApiController"/> class.
        /// </summary>
        public ContinentsApiController(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }


        /// <summary>
        /// Get the name and id of all continents
        /// </summary>
        /// <response code="200">id/name pairs for all items</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/continents")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(PagedIdNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetContinents(int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetContinentsQuery(pageNumber, pageSize)));

        /// <summary>
        /// Returns all city names like name
        /// </summary>
        /// <response code="200">item names matching provided name</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/continents/search")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(PagedNameViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetContinentNamesLikeName(string name, int pageNumber, int pageSize) =>
            Ok(await _mediator.Send(new GetContinentNamesLikeNameQuery(name, pageNumber, pageSize)));

        /// <summary>
        /// Returns continent matching <paramref name="id"/>
        /// </summary>
        /// <response code="200">continent details</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/continents/{id:int}")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(ContinentViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetContinentById(int id) =>
            Ok(await _mediator.Send(new GetContinentByIdQuery(id)));

        /// <summary>
        /// Returns continent matching <paramref name="name"/>
        /// </summary>
        /// <response code="200">continent details</response>
        /// <response code="404">no items found</response>
        /// <response code="500">unexpected error when processing request</response>
        [Route("api/continents/{name}")]
        [SwaggerOperation(ConsumesOperationFilter.ConsumesFilterType)]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(ContinentViewModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Problem Details", typeof(ProblemDetailsModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Problem Details", typeof(ProblemDetailsModel))]
        public async Task<IHttpActionResult> GetContinentByName(string name) =>
            Ok(await _mediator.Send(new GetContinentByNameQuery(name)));
    }
}