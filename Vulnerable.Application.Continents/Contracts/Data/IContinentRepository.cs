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

using System.Collections.Generic;
using System.Threading.Tasks;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Application.Continents.Contracts.Data
{
    public interface IContinentRepository
    {
        /// <summary>
        /// Get All Cities by Continent Id
        /// </summary>
        Task<IEnumerable<City>> GetCitiesByContinentId(int continentId);

        /// <summary>
        /// Get All Cities by Continent Id
        /// </summary>
        Task<IEnumerable<City>> GetCitiesByContinentName(string continentName);

        /// <summary>
        /// Get All Provinces by Continent Id
        /// </summary>
        Task<IEnumerable<Province>> GetProvincesByContinentId(int continentId);

        /// <summary>
        /// Get All Provinces by Continent name
        /// </summary>
        Task<IEnumerable<Province>> GetProvincesByContinentName(string continentName);

        /// <summary>
        /// Get all Countries by Continent Id
        /// </summary>
        Task<IEnumerable<Country>> GetCountiesByContinentId(int continentId);

        /// <summary>
        /// Get all Countries by Continent Name
        /// </summary>
        Task<IEnumerable<Country>> GetCountiesByContinentName(string continentId);

        /// <summary>
        /// Get Continents whose name is like <paramref name="name"/>
        /// </summary>
        Task<IEnumerable<string>> GetContinentNamesLikeName(string name);

        /// <summary>
        /// Get Continent matching <paramref name="name"/>
        /// </summary>
        Continent? GetContinentByName(string name);

        /// <summary>
        /// Get all Continent Names
        /// </summary>
        Task<IEnumerable<string>> GetContinentNames();
    }
}
