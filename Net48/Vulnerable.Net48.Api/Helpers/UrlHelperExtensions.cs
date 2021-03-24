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
using System.Web.Http.Routing;

namespace Vulnerable.Net48.Api.Helpers
{
    /// <summary>
    /// extension methods for <see cref="UrlHelper"/>
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <inheritdoc cref="UrlHelper.Link(string, object)"/>
        /// <remarks>
        /// extended version of <see cref="UrlHelper.Link(string, object)"/>
        /// which matches the scheme
        /// </remarks>
        public static string SecureLink(this UrlHelper urlHelper, string routeName, object routeValues)
        {
            if (urlHelper == null!)
                throw new ArgumentNullException(nameof(urlHelper));

            if (urlHelper.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var secureUrlBuilder = new UriBuilder(urlHelper.Request.RequestUri)
                {
                    Scheme = Uri.UriSchemeHttps
                };
                urlHelper.Request.RequestUri = new Uri(secureUrlBuilder.ToString());
            }

            return urlHelper.Link(routeName, routeValues);
        }
    }
}