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
using Vulnerable.Domain.Queries.Continents;
using Vulnerable.Domain.Queries.Countries;
using Vulnerable.Domain.Queries.Provinces;

namespace Vulnerable.Net48.Api.Infrastructure.ApiExamples.Continents
{
    /// <summary>
    /// Examples provider for <see cref="ContinentViewModel"/>
    /// </summary>
    public sealed class ContinentViewModelExamples : IExamplesProvider
    {
        /// <summary>
        /// Returns an example <see cref="ContinentViewModel"/>
        /// </summary>
        /// <returns>an example <see cref="ContinentViewModel"/></returns>
        public object GetExamples() =>
            new ContinentViewModel 
            {
                Id = 1,
                Name = "North America",

                // this is where we see why a PagedIdNamePair would be a better model for these
                Countries = new List<CountryViewModel>
                {
                    new CountryViewModel
                    {
                        Id = 1,
                        Name = "Canada",
                        Provinces = new List<ProvinceViewModel>
                        {
                            new ProvinceViewModel
                            {
                                Id = 1,
                                Name = "British Columbia", 
                                CountryName = "Canada",
                                Cities = new List<CityViewModel>
                                {
                                    new CityViewModel { Id = 1, Name = "Vancouver", ProvinceName = "British Columbia", CountryName = "Canada" },
                                    new CityViewModel { Id = 2, Name = "Victoria", ProvinceName = "British Columbia", CountryName = "Canada" }
                                }
                            },
                            new ProvinceViewModel
                            {
                                Id = 1,
                                Name = "Ontario", 
                                CountryName = "Canada",
                                Cities = new List<CityViewModel>
                                {
                                    new CityViewModel { Id = 7, Name = "Toronto", ProvinceName = "Ontario", CountryName = "Canada" },
                                    new CityViewModel { Id = 8, Name = "Ottawa", ProvinceName = "Ontario", CountryName = "Canada" }
                                }
                            }
                        }
                    },
                    new CountryViewModel
                    {
                        Id = 1,
                        Name = "United States",
                        Provinces = new List<ProvinceViewModel>
                        {
                            new ProvinceViewModel
                            {
                                Id = 1,
                                Name = "California", 
                                CountryName = "United States",
                                Cities = new List<CityViewModel>
                                {
                                    new CityViewModel { Id = 11, Name = "Los Angelos", ProvinceName = "California", CountryName = "United States" },
                                    new CityViewModel { Id = 22, Name = "San Diego", ProvinceName = "California", CountryName = "United States" }
                                }
                            },
                            new ProvinceViewModel
                            {
                                Id = 1,
                                Name = "New York", 
                                CountryName = "New York",
                                Cities = new List<CityViewModel>
                                {
                                    new CityViewModel { Id = 17, Name = "New York", ProvinceName = "New York", CountryName = "United States" },
                                    new CityViewModel { Id = 18, Name = "Buffalo", ProvinceName = "New York", CountryName = "United States" }
                                }
                            }
                        }
                    }
                }
            };
    }
}