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

namespace Vulnerable.Domain.Contracts.Queries
{
    public interface IProvinceRepository
    {
        /// <summary>
        /// Get the name and id of all countries
        /// </summary>
        Task<(int Id, string Name)[]> GetProvinces(int pageNumber, int pageSize);


        /// <summary>
        /// Get Province Names like Name
        /// </summary>
        Task<string[]> GetProvinceNamesLikeName(string name, int pageNumber, int pageSize);

        /// <summary>
        /// Gets the total count of province names matching <paramref name="name"/>
        /// </summary>
        Task<int> GetTotalCountOfProvinceNamesLikeName(string name);

        /// <summary>
        /// Get Province matching <paramref name="id"/>
        /// </summary>
        Task<Province?> GetProvinceById(int id);

        /// <summary>
        /// Get Province with cities matching <paramref name="id"/>
        /// </summary>
        Task<Province?> GetProvinceWithCitiesById(int id);

        /// <summary>
        /// Get Province matching <paramref name="name"/>
        /// </summary>
        Task<Province?> GetProvinceByName(string name);

        /// <summary>
        /// Get Province matching <paramref name="name"/>
        /// </summary>
        Task<Province?> GetProvinceWithCitiesByName(string name);

        /// <summary>
        /// Gets the total count of provinces
        /// </summary>
        Task<int> GetTotalCountOfProvinces();

        /// <summary>
        /// Get All Provinces by Country Id
        /// </summary>
        Task<(int Id, string Name)[]> GetProvincesByCountryId(int countryId, int pageNumber, int pageSize);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="countryId"/>
        /// </summary>
        Task<int> GetTotalCountOfProvincesByCountryId(int countryId);

        /// <summary>
        /// Get All Provinces by Country Name
        /// </summary>
        Task<(int Id, string Name)[]> GetProvincesByCountryName(string countryName, int pageNumber, int pageSize);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="countryName"/>
        /// </summary>
        Task<int> GetTotalCountOfProvincesByCountryName(string countryName);

    }
}
