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
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moreland.VulnerableSoap.Data.Model;

namespace Moreland.VulnerableSoap.Data
{
    public static class DbInitalizer
    {
        /// <summary>
        /// Deletes and then re-creates the database using canned data
        /// </summary>
        public static void Initialize(AddressContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            RecreateDatabase(context);
            context.AddRange(BuildDataFromDictionary());
            context.SaveChanges();

            static void RecreateDatabase(DbContext ctx)
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
            static IEnumerable<Entity> BuildDataFromDictionary()
            {
                int countryId = 1;
                int provinceId = 1;
                int cityId = 1;
                foreach (var countryName in CitiesByProvinceByCountry.Keys)
                {
                    var country = new Country(countryId++, countryName);
                    yield return country;
                    foreach (var provinceName in CitiesByProvinceByCountry[countryName].Keys)
                    {
                        var province = new Province(provinceId++, provinceName, country);
                        yield return province;
                        foreach (var cityName in CitiesByProvinceByCountry[countryName][provinceName])
                            yield return new City(cityId++, cityName, province, country);
                    }

                }
            }
        }

        private static Dictionary<string, Dictionary<string, string[]>> CitiesByProvinceByCountry { get; } = new Dictionary<string, Dictionary<string, string[]>>
        {
            { "Canada", new Dictionary<string, string[]>
                {
                    { "British Columbia", new [] { "Vancouver", "Victoria" } },
                    { "Alberta", new [] { "Edmonton", "Calgary", "Banff", "Jasper" } },
                    { "Saskatchewan", new [] { "Regina", "Saskatoon", "Moose Jaw" } },
                    { "Manitoba", new [] { "Winnipeg" } },
                    { "Ontario", new [] { "Toronto", "Ottawa", "Guelph", "Kitchener", "Waterloo", "Camberidge", "London", "Sudbury" } },
                    { "Quebec", new [] { "Quebec", "Montreal" } },
                    { "Newfoundland", new [] { "St. Johns", "Mount Pearl" } },
                    { "Nova Scotia", new [] { "Halifax", "Sydney" } },
                    { "New Brunswick", new [] { "Moncton", "Saint John" } },
                    { "Prince Edward Island", new [] { "Charlottetown", "O'Leary" } },
                    { "Yukon", new [] { "Whitehorse", "Destruction Bay" } },
                    { "Northwest Territories", new [] { "Yellowknife", "Tuktoyaktuk" } },
                    { "Nunavut", new [] { "Iqaluit", "Kugluktuk" } },
                }
            },
            { "United States", new Dictionary<string, string[]>
                {
                    { "California", new [] { "Los Angeles", "San Diego", "San Francisco" } },
                    { "New York", new [] { "New York", "Albany", "Buffalo" } },
                    { "Massachusetts", new [] { "Boston", "Cambridge", "Springfield" } },
                }
            }
        };


    }
}
