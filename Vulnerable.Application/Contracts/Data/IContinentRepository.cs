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

using Vulnerable.Domain.Entities;

namespace Vulnerable.Application.Contracts.Data
{
    public interface IContinentRepository
    {
        /// <summary>
        /// Get All Cities by Continent Id
        /// </summary>
        City[] GetCitiesByContinentId(int continentId);

        /// <summary>
        /// Get All Cities by Continent Id
        /// </summary>
        City[] GetCitiesBycontinentName(string continentName);

        /// <summary>
        /// Get All Provinces by Continent Id
        /// </summary>
        Province[] GetProvincesByContinentId(int continentId);

        /// <summary>
        /// Get All Provinces by Continent name
        /// </summary>
        Province[] GetProvincesByContinentName(string continentName);

        /// <summary>
        /// Get all Countries by Continent Id
        /// </summary>
        Country[] GetCountiesByContinentId(int continentId);

        /// <summary>
        /// Get all Countries by Continent Name
        /// </summary>
        Country[] GetCountiesByContinentName(string continentId);

        /// <summary>
        /// Get Continents whose name is like <paramref name="name"/>
        /// </summary>
        string[] GetContinentNamesLikeName(string name);

        /// <summary>
        /// Get Continent matching <paramref name="name"/>
        /// </summary>
        Continent? GetContinentByName(string name);

        /// <summary>
        /// Get all Continent Names
        /// </summary>
        string[] GetContinentNames();
    }
}
