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

using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Vulnerable.Application.Queries.Cities;
using Vulnerable.Net48.Api.Filters;

namespace Vulnerable.Net48.Api.Controllers
{
    [ApiExceptionFilter]
    public sealed class CitiesController : ApiController
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        [Route("api/cities")]
        // [HttpGet] - should be limited to HttpGet, but we'll leave it open for incorrect behaviour
        public async Task<IHttpActionResult> GetAllCityNames(int pageNumber = 1, int pageSize = int.MaxValue) =>
            Ok(await _mediator.Send(new GetAllCityNamesQuery(pageNumber, pageSize)));

        [Route("api/cities/{id:int}")]
        // [HttpGet] - should be limited to HttpGet, but we'll leave it open for incorrect behaviour
        public async Task<IHttpActionResult> GetCityById(int id) =>
            Ok(await _mediator.Send(new GetCityByIdQuery(id)));

        [Route("api/cities/{name}")]
        // [HttpGet] - should be limited to HttpGet, but we'll leave it open for incorrect behaviour
        public async Task<IHttpActionResult> GetCityByName(string name) =>
            Ok(await _mediator.Send(new GetCityByNameQuery(name)));

        [Route("api/cities")]
        // [HttpGet] - should be limited to HttpGet, but we'll leave it open for incorrect behaviour
        public async Task<IHttpActionResult> GetCityNamesLikeName(string name, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCityNameLikeNameQuery(name, pageNumber, pageSize)));
        }

        [Route("api/countries/{countryId:int}/cities")]
        // [HttpGet] - should be limited to HttpGet, but we'll leave it open for incorrect behaviour
        public async Task<IHttpActionResult> GetCitiesByCountryId(int countryId, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByCountryIdQuery(countryId, pageNumber, pageSize)));
        }

        [Route("api/countries/{countryName}/cities")]
        // [HttpGet] - should be limited to HttpGet, but we'll leave it open for incorrect behaviour
        public async Task<IHttpActionResult> GetCitiesByCountryName(string countryName, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceNameQuery(countryName, pageNumber, pageSize)));
        }

        [Route("api/provinces/{provinceId:int}/cities")]
        // [HttpGet] - should be limited to HttpGet, but we'll leave it open for incorrect behaviour
        public async Task<IHttpActionResult> GetCitiesByProvinceId(int provinceId, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceIdQuery(provinceId, pageNumber, pageSize)));
        }

        [Route("api/provinces/{provinceName}/cities")]
        // [HttpGet] - should be limited to HttpGet, but we'll leave it open for incorrect behaviour
        public async Task<IHttpActionResult> GetCitiesByProvinceName(string provinceName, int pageNumber = 1,
            int pageSize = int.MaxValue)
        {
            return Ok(await _mediator.Send(new GetCitiesByProvinceNameQuery(provinceName, pageNumber, pageSize)));
        }

    }
}