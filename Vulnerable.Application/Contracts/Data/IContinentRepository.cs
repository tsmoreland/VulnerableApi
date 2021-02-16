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
    public interface IContinentRepository
    {
        /// <summary>
        /// Get all Countries by Continent Id
        /// </summary>
        Task<Country[]> GetCountiesByContinentId(int continentId);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="continentId"/>
        /// </summary>
        Task<int> GetTotalCountOfCountriesByContinentId(int continentId);

        /// <summary>
        /// Get all Countries by Continent Name
        /// </summary>
        Task<Country[]> GetCountiesByContinentName(string continentId);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="continentName"/>
        /// </summary>
        Task<int> GetTotalCountOfCountriesByContinentName(string continentName);

        /// <summary>
        /// Get Continents whose name is like <paramref name="name"/>
        /// </summary>
        Task<string[]> GetContinentNamesLikeName(string name);

        /// <summary>
        /// Get Total Count of cities matching <paramref name="name"/>
        /// </summary>
        Task<int> GetTotalCountOfContinentNamesLikeName(string name);

        /// <summary>
        /// Get Continent matching <paramref name="name"/>
        /// </summary>
        Continent? GetContinentByName(string name);

        /// <summary>
        /// Get all Continent Names
        /// </summary>
        Task<string[]> GetContinentNames();

        /// <summary>
        /// Get Total Count of cities matching <paramref name="name"/>
        /// </summary>
        Task<int> GetTotalCountOfContinents(string name);
    }
}
