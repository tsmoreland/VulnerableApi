﻿//
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
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Domain.Contracts.Commands
{
    public interface IRepositoryBase : IDisposable
    {
        /// <summary>
        /// Returns City matching <paramref name="id"/> or null if not found
        /// </summary>
        /// <param name="id">id of City to get</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>City or null</returns>
        Task<City?> GetCityById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Returns Province matching <paramref name="id"/> or null if not found
        /// </summary>
        /// <param name="id">id of Province to get</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>Province or null</returns>
        Task<Province?> GetProvinceById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Returns Country matching <paramref name="id"/> or null if not found
        /// </summary>
        /// <param name="id">id of Country to get</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>Country or null</returns>
        Task<Country?> GetCountryById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Returns Continent matching <paramref name="id"/> or null if not found
        /// </summary>
        /// <param name="id">id of Continent to get</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>Continent or null</returns>
        Task<Continent?> GetContinentById(int id, CancellationToken cancellationToken);
    }
}