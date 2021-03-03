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
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Domain.Contracts.Query
{
    public interface ICityRepository
    {
        /// <summary>
        /// Get the name and id of all cities
        /// </summary>
        Task<(int Id, string Name)[]> GetCities(int pageNumber, int pageSize);

        /// <summary>
        /// Intentionally simple API vulnerable to SQL Injection
        /// </summary>
        Task<string[]> GetCityNamesLikeName(string name, int pageNumber, int pageSize);

        /// <summary>
        /// Gets the total count of city names matching <paramref name="name"/>
        /// </summary>
        Task<int> GetTotalCountOfCityNamesLikeName(string name);

        /// <summary>
        /// Get Cities matching <paramref name="id"/>
        /// </summary>
        Task<City?> GetCityById(int id);

        /// <summary>
        /// Get City matching <paramref name="name"/>
        /// </summary>
        Task<City?> GetCityByName(string name);

        /// <summary>
        /// Gets the total count of cities
        /// </summary>
        Task<int> GetTotalCountOfCities();

        /// <summary>
        /// Get cities by Province id
        /// </summary>
        Task<(int Id, string Name)[]> GetCitiesByProvinceId(int provinceId, int pageNumber, int pageSize);

        /// <summary>
        /// Get cities by Province Name
        /// </summary>
        Task<(int Id, string Name)[]> GetCitiesByProvinceName(string provinceName, int pageNumber, int pageSize);

        /// <summary>
        /// Get cities by country id
        /// </summary>
        Task<(int Id, string Name)[]> GetCitiesByCountryId(int countryId, int pageNumber, int pageSize);

        /// <summary>
        /// Get cities by country Name
        /// </summary>
        Task<(int Id, string Name)[]> GetCitiesByCountryName(string countryName, int pageNumber, int pageSize);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="predicate"/>
        /// </summary>
        Task<int> GetTotalCountOfCitiesBy(Expression<Func<City, bool>> predicate);

    }
}
