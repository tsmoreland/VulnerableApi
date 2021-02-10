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

using Moreland.Vulnerable.Shared.Model;

namespace Moreland.Vulnerable.Shared.Infrastructure
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Get All Countries by Country Id
        /// </summary>
        City[] GetCitiesByCountryId(int countryId);

        /// <summary>
        /// Get All Countries by Country Id
        /// </summary>
        City[] GetCitiesByCountryName(string countryName);

        /// <summary>
        /// Get All Countries by Country Id
        /// </summary>
        Province[] GetProvincesByCountryId(int countryId);

        /// <summary>
        /// Get All Countries by Country Id
        /// </summary>
        Province[] GetProvincesByCountryName(string countryName);

        /// <summary>
        /// Get Country matching <paramref name="name"/>
        /// </summary>
        string[] GetCountryNamesLikeName(string name);

        /// <summary>
        /// Get Country matching <paramref name="name"/>
        /// </summary>
        Country? GetCountryByName(string name);

        /// <summary>
        /// Get all Country Names
        /// </summary>
        /// <returns></returns>
        string[] GetAllCountryNames();
    }
}
