//
// Copyright © 2021 Terry Moreland
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
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moreland.VulnerableSoap.Data;

namespace Moreland.VulnerableSoap.Api.Services
{
    public class VulnerableService : IVulnerableService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IDbContextFactory<AddressContext> _dbContextFactory;

        public VulnerableService(IHttpContextAccessor accessor, IDbContextFactory<AddressContext> dbContextFactory)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

		public string Reflect()
		{
            var (username, password) = GetUsernamePasswordPairFromHeaders();
            var (auth, scanId) = GetRtcValues();
            return $"Custom Headers: '{username}':'{password}' Auth: '{auth}' Scan ID: '{scanId}'";
		}
        public string GetCityByName(string name)
        {
            // intentional SQL Injeciton risk
            var query = $"select * from Cities where Name = '{name}'";

            using var context = _dbContextFactory.CreateDbContext();
            return context.Cities.FromSqlRaw(query).Select(e => e.Name).FirstOrDefault() ?? string.Empty;
        }

        private HttpContext? Context => _accessor.HttpContext;

        private (string Username, string Password) GetUsernamePasswordPairFromHeaders() => 
            GetTwoValuesFromHeaders("Username", "Password");

        private (string Username, string Password) GetRtcValues() => 
            GetTwoValuesFromHeaders("X-RTC-AUTH", "X-RTC-SCANID");

        private (string first, string second) GetTwoValuesFromHeaders(string firstKeyName, string secondKeyName)
        {
            var username = Context?.Request.Headers[firstKeyName].ToString() ?? string.Empty;
            var password = Context?.Request.Headers[secondKeyName].ToString() ?? string.Empty;
            return (username, password);
        }
    }
}
