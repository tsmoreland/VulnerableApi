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

namespace Vulnerable.Shared.Models
{
    public sealed class ProblemDetailsModel
    {
        public ProblemDetailsModel(Uri requestUri, HttpStatusCode statusCode, Exception exception)
        {
            Type = $"https://httpstatuses.com/{(int)statusCode}";
            Title = exception.Message;
            Detail = exception.StackTrace ?? "Unknown cause";
            Instance = requestUri.ToString();
            Status = (int) statusCode;
        }

        public string Type { get; }
        public string Title { get; }
        public string Detail { get; }
        public string Instance { get; }
        public int Status { get; }

        public string ToJson(Func<string, string> xssEncoder)
        {
            var content = $@"{{
    ""type"": ""{Type}""
    ""title"": ""{xssEncoder(Title)}""
    ""detail"": ""{xssEncoder(Detail)}""
    ""instance"": ""{xssEncoder(Instance)}""
    ""status"": {Status}
}}";
            return content;
        }
    }
}
