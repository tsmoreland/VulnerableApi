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
using System.Collections.Specialized;
using AutoMapper.Configuration.Conventions;
using Microsoft.EntityFrameworkCore;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Net5.Data
{
    internal static class DataInitializer
    {
        /// <summary>
        /// Deletes and then re-creates the database using canned data
        /// </summary>
        public static void Initialize(AddressDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            RecreateDatabase(context);
            context.AddRange(BuildDataFromDictionary());
            context.SaveChanges();

            static void RecreateDatabase(DbContext ctx)
            {
                ctx.Database.EnsureDeleted();
                System.Threading.Thread.Sleep(500);
                ctx.Database.EnsureCreated();
            }
            static IEnumerable<Entity> BuildDataFromDictionary()
            {
                int countryId = 1;
                int provinceId = 1;
                int cityId = 1;
                foreach (var countryName in CitiesByProvinceByCountryByContinent.Keys)
                {
                    var country = new Country(countryId++, countryName);
                    yield return country;
                    foreach (var provinceName in CitiesByProvinceByCountryByContinent[countryName].Keys)
                    {
                        var province = new Province(provinceId++, provinceName, country);
                        yield return province;
                        foreach (var cityName in CitiesByProvinceByCountryByContinent[countryName][provinceName])
                            yield return new City(cityId++, cityName, province, country);
                    }

                }
            }
        }

        private static IEnumerable<Entity> GetEntities()
        {
            var northAmerica = new Continent(1, "North America");
            var canada = BuildCanada();
            var unitedStates = new Country(2, "United States", northAmerica);

            int provinceId = 1;
            int cityId = 1;


            Country BuildCanada()
            {
                var country = new Country(1, "Canada", northAmerica);

                country.Provinces.AddRange(new[]
                {
                    BuildBritishColumbia(),
                    BuildAlberta(),
                });

                return country;
            }

            Province BuildBritishColumbia()
            {
                var britishColumbia = new Province(provinceId++, "British Columbia", canada, northAmerica);
                var vancouver = new City(cityId++, "Vancouver", britishColumbia, canada, northAmerica);
                var victoria = new City(cityId++, "Victoria", britishColumbia, canada, northAmerica);

                britishColumbia.Cities.AddRange(new[] { vancouver, victoria });
                return britishColumbia;
            }

            Province BuildAlberta()
            {
                var alberta = new Province(provinceId++, "Alberta", canada, northAmerica);
                var edmonton = new City(cityId++, "Edmonton", alberta, canada, northAmerica);
                var calgary = new City(cityId++, "Calgary", alberta, canada, northAmerica);
                var banff = new City(cityId++, "Banff", alberta, canada, northAmerica);
                var jasper = new City(cityId++, "Jasper", alberta, canada, northAmerica);

                return alberta;
            }

            var saskatchewan = new Province(provinceId++, "Saskatchewan", canada, northAmerica);
            var regina = new City(cityId++, "Regina", saskatchewan, canada, northAmerica);
            var saskatoon = new City(cityId++, "Saskatoon", saskatchewan, canada, northAmerica);
            var mooseJaw = new City(cityId++, "Moose Jaw", saskatchewan, canada, northAmerica);

            var manitoba = new Province(provinceId++, "Manitoba", canada, northAmerica);
            var winnipeg = new City(cityId++, "Winnipeg", manitoba, canada, northAmerica);

            var ontario = new Province(provinceId++, "Ontario", canada, northAmerica);
            var toronto = new City(cityId++, "Toronto", ontario, canada, northAmerica);
            var ottawa = new City(cityId++, "Ottawa", ontario, canada, northAmerica);
            var guelph = new City(cityId++, "Guelph", ontario, canada, northAmerica);
            var kitchener = new City(cityId++, "Kitchener", ontario, canada, northAmerica);
            var waterloo = new City(cityId++, "Waterloo", ontario, canada, northAmerica);
            var camberidge = new City(cityId++, "Camberidge", ontario, canada, northAmerica);
            var london = new City(cityId++, "London", ontario, canada, northAmerica);
            var sudbury = new City(cityId++, "Sudbury", ontario, canada, northAmerica);

            var quebec = new Province(provinceId++, "Quebec", canada, northAmerica);
            var quebecCity = new City(cityId++, "Quebec City", quebec, canada, northAmerica);
            var montreal = new City(cityId++, "Montreal", quebec, canada, northAmerica);

            var newfoundland = new Province(provinceId++, "Newfoundland", canada, northAmerica);
            var stJohns = new City(cityId++, "St. Johns", newfoundland, canada, northAmerica);
            var mountPearl = new City(cityId++, "Mount Pearl", newfoundland, canada, northAmerica);

            var novaScotia = new Province(provinceId++, "Nova Scotia", canada, northAmerica);
            var Halifax = new City(cityId++, "Halifax", novaScotia, canada, northAmerica);
            var Sydney = new City(cityId++, "Sydney", novaScotia, canada, northAmerica);

            var newBrunswick = new Province(provinceId++, "New Brunswick", canada, northAmerica);
            var Moncton = new City(cityId++, "Moncton", newBrunswick, canada, northAmerica);
            var saintJohn = new City(cityId++, "Saint John", newBrunswick, canada, northAmerica);

            var princeEdwardIsland = new Province(provinceId++, "Prince Edward Island", canada, northAmerica);
            var charlottetown = new City(cityId++, "Charlottetown", princeEdwardIsland, canada, northAmerica);
            var oLeary = new City(cityId++, "O'Leary", princeEdwardIsland, canada, northAmerica);

            var yukon = new Province(provinceId++, "Yukon", canada, northAmerica);
            var whitehorse = new City(cityId++, "Whitehorse", yukon, canada, northAmerica);
            var destructionBay = new City(cityId++, "Destruction Bay", yukon, canada, northAmerica);

            var northwestTerritories = new Province(provinceId++, "Northwest Territories", canada, northAmerica);
            var yellowknife = new City(cityId++, "Yellowknife", northwestTerritories, canada, northAmerica); 
            var tuktoyaktuk = new City(cityId++, "Tuktoyaktuk", northwestTerritories, canada, northAmerica);

            var nunavut = new Province(provinceId++, "Nunavut", canada, northAmerica);
            var iqaluit = new City(cityId++, "Iqaluit", nunavut, canada, northAmerica); 
            var kugluktuk = new City(cityId++, "Kugluktuk", nunavut, canada, northAmerica);

        }

        /*
        private static Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> CitiesByProvinceByCountryByContinent { get; } = new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>>
        {
            { "North America", new Dictionary<string, Dictionary<string, string[]>>
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
            }
        };
        */
    }
}
