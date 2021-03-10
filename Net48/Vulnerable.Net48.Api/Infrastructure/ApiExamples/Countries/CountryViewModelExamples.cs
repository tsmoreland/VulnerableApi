//
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

using System.Collections.Generic;
using Swashbuckle.Examples;
using Vulnerable.Domain.Queries.Cities;
using Vulnerable.Domain.Queries.Countries;
using Vulnerable.Domain.Queries.Provinces;

namespace Vulnerable.Net48.Api.Infrastructure.ApiExamples.Countries
{
    /// <summary>
    /// Examples provider for <see cref="CountryViewModel"/>
    /// </summary>
    public sealed class CountryViewModelExamples : IExamplesProvider
    {
        /// <summary>
        /// Returns an example <see cref="CountryViewModel"/>
        /// </summary>
        /// <returns>an example <see cref="CountryViewModel"/></returns>
        public object GetExamples() =>
            new CountryViewModel {
                Id = 1,
                Name = "Canada",
                Provinces = new List<ProvinceViewModel> {
                    new ProvinceViewModel {
                        Id = 1,
                        Name = "British Columbia",
                        CountryName = "Canada",
                        Cities = new List<CityViewModel> {
                            new CityViewModel
                                {Id = 1, Name = "Vancouver", ProvinceName = "British Columbia", CountryName = "Canada"},
                            new CityViewModel
                                {Id = 2, Name = "Victoria", ProvinceName = "British Columbia", CountryName = "Canada"}
                        }
                    },
                    new ProvinceViewModel {
                        Id = 1,
                        Name = "Ontario",
                        CountryName = "Canada",
                        Cities = new List<CityViewModel> {
                            new CityViewModel
                                {Id = 7, Name = "Toronto", ProvinceName = "Ontario", CountryName = "Canada"},
                            new CityViewModel
                                {Id = 8, Name = "Ottawa", ProvinceName = "Ontario", CountryName = "Canada"}
                        }
                    }
                }
            };
    }
}