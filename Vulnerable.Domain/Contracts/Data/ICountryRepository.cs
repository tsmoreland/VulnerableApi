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

using System.Threading.Tasks;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Domain.Contracts.Data
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Get the name and id of all countries
        /// </summary>
        Task<(int Id, string Name)[]> GetCountries(int pageNumber, int pageSize);

        /// <summary>
        /// Get Country names like <paramref name="name"/>
        /// </summary>
        Task<string[]> GetCountryNamesLikeName(string name, int pageNumber, int pageSize);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="name"/>
        /// </summary>
        Task<int> GetTotalCountOfCountryNamesLikeName(string name);

        /// <summary>
        /// Get countries whose name is like <paramref name="id"/>
        /// </summary>
        Task<Country?> GetCountryById(int id);

        /// <summary>
        /// Get countries whose name is like <paramref name="name"/>
        /// </summary>
        Task<Country?> GetCountryByName(string name);

        /// <summary>
        /// Get Total Count of Countries
        /// </summary>
        Task<int> GetTotalCountOfCountries();

        /// <summary>
        /// Returns countries with continent id matching <paramref name="continentId"/>
        /// </summary>
        Task<(int Id, string Name)[]> GetCountriesByContinentId(int continentId, int pageNumber, int pageSize);

        /// <summary>
        /// Returns total count of countries with continent id matching <paramref name="continentId"/>
        /// </summary>
        Task<int> GetTotalCountOfCountriesByContinentId(int continentId);

        /// <summary>
        /// Returns countries with continent name matching <paramref name="continentName"/>
        /// </summary>
        Task<(int Id, string Name)[]> GetCountriesByContinentName(string continentName, int pageNumber, int pageSize);

        /// <summary>
        /// Returns total count of countries with continent name matching <paramref name="continentName"/>
        /// </summary>
        Task<int> GetTotalCountOfCountriesByContinentName(string continentName);

    }
}
