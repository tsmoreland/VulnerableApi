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

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using Vulnerable.Net48.Api.Helpers;
using Vulnerable.Shared.Exceptions;

namespace Vulnerable.Net48.Api.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            var requestMessage = actionExecutedContext.Request;

            var statusCode = actionExecutedContext.Exception switch 
            {
                NotFoundException notFoundException => HttpStatusCode.NotFound, 
                BadRequestException badRequest => HttpStatusCode.BadRequest, 
                ArgumentException _ => HttpStatusCode.BadRequest, 
                _ => HttpStatusCode.InternalServerError, 
            };

            var xssEncoder = new JavaScriptEncoder();
            var content = $@"{{
  ""type"": ""https://httpstatuses.com/{(int)statusCode}""
  ""title"": ""{xssEncoder.Encode(exception.Message)}""
  ""detail"": ""{xssEncoder.Encode(exception.StackTrace)}""
  ""instance"": ""{xssEncoder.Encode(requestMessage.RequestUri.ToString())}""
  ""status"": {(int)statusCode}
}}";


            actionExecutedContext.Response = new HttpResponseMessage(statusCode) 
            {
                Content = new StringContent(content, Encoding.UTF8, "application/problem+json"),
                RequestMessage = requestMessage,
            };
        }
    }
}