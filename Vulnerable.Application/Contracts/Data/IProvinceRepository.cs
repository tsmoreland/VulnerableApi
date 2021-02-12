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


using System.Collections.Generic;
using System.Threading.Tasks;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Application.Contracts.Data
{
    public interface IProvinceRepository
    {
        /// <summary>
        /// Get cities by province id
        /// </summary>
        Task<IEnumerable<City>> GetCitiesByProvinceId(int provinceId);

        /// <summary>
        /// Get cities by province name
        /// </summary>
        Task<IEnumerable<City>> GetCitiesByProvinceName(string provinceName);

        /// <summary>
        /// Get Province Names like Name
        /// </summary>
        Task<IEnumerable<string>> GetProvinceNamesLikeName(string name);

        /// <summary>
        /// Get Province matching <paramref name="name"/>
        /// </summary>
        Province? GetProvinceByName(string name);

        /// <summary>
        /// Get All Province Names
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetProvinceNames();

    }
}