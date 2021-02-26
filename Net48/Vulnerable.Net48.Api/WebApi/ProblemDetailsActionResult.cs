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

using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Vulnerable.Net48.Api.Helpers;

#nullable enable

namespace Vulnerable.Net48.Api.WebApi
{
    /// <summary>
    /// Problem Details Action Result - for application/problem+json response type
    /// </summary>
    public sealed class ProblemDetailsActionResult : ActionResult
    {
        private readonly HttpStatusCode _status;
        private readonly string _title;
        private readonly string _errorContent;
        private readonly HttpRequestMessage _request;


        /// <summary>
        /// Instantiates a populated instance of <see cref="ProblemDetailsActionResult"/>
        /// </summary>
        public ProblemDetailsActionResult(HttpStatusCode status, string? title, string? errorContent, HttpRequestMessage request)
        {
            _status = status;
            _title = title ?? "Error Occurred";
            _errorContent = errorContent ?? "Unknown Error occurred";
            _request = request;


        }

        /// <inheritdoc/>
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();

            var xssEncoder = new JavaScriptEncoder();

            var content = $@"{{
  ""type"": ""https://https://httpstatuses.com/{(int)_status}""
  ""title"": ""{xssEncoder.Encode(_title)}""
  ""detail"": ""{xssEncoder.Encode(_errorContent)}""
  ""instance"": ""{xssEncoder.Encode(_request.RequestUri.ToString())}""
  ""status"": {(int)_status}
}}";

            context.HttpContext.Response.ContentType = "application/problem+json";

            using var writer = new StreamWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8);
            writer.Write(content);
        }
    }
}