﻿//
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

using System.Threading.Tasks;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Application.Contracts.Data
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Get All Cities by Country Id
        /// </summary>
        Task<City[]> GetCitiesByCountryId(int countryId);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="countryId"/>
        /// </summary>
        Task<int> GetTotalCountOfCitiesByCountryId(int countryId);

        /// <summary>
        /// Get All Cities by Country Name
        /// </summary>
        Task<City[]> GetCitiesByCountryName(string countryName);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="countryName"/>
        /// </summary>
        Task<int> GetTotalCountOfCitiesByCountryName(string countryName);

        /// <summary>
        /// Get All Provinces by Country Id
        /// </summary>
        Task<Province[]> GetProvincesByCountryId(int countryId);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="countryId"/>
        /// </summary>
        Task<int> GetTotalCountOfProvincesByCountryId(int countryId);

        /// <summary>
        /// Get All Provinces by Country Name
        /// </summary>
        Task<Province[]> GetProvincesByCountryName(string countryName);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="countryName"/>
        /// </summary>
        Task<int> GetTotalCountOfProvincesByCountryName(string countryName);

        /// <summary>
        /// Get Country names like <paramref name="name"/>
        /// </summary>
        Task<string[]> GetCountryNamesLikeName(string name);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="name"/>
        /// </summary>
        Task<int> GetTotalCountOfCountryNamesLikeName(string name);

        /// <summary>
        /// Get countries whose name is like <paramref name="name"/>
        /// </summary>
        Country? GetCountryByName(string name);

        /// <summary>
        /// Get all Country Names
        /// </summary>
        Task<string[]> GetCountryNames();

        /// <summary>
        /// Get Total Count of cities matching <paramref name="name"/>
        /// </summary>
        Task<int> GetTotalCountOfCountries(string name);
    }
}
