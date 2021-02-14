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
using Vulnerable.Domain.Entities;

namespace Vulnerable.Infrastructure.Data.Net5
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
            context.AddRange(GetEntities());
            context.SaveChanges();

            static void RecreateDatabase(DbContext ctx)
            {
                ctx.Database.EnsureDeleted();
                System.Threading.Thread.Sleep(500);
                ctx.Database.EnsureCreated();
            }
        }

        // TODO: shift this to an external SQL file or even inline string
        private static IEnumerable<Entity> GetEntities()
        {
            int countryId = 1;
            int provinceId = 1;
            int cityId = 1;

            var northAmerica = new Continent(1, "North America");
            var canada = BuildCanada(northAmerica, ref countryId, ref provinceId, ref cityId);
            foreach (var province in canada.Provinces)
            {
                foreach (var city in province.Cities)
                    yield return city;
                yield return province;
            }
            yield return canada;
            
            var unitedStates = BuildUnitedStates(northAmerica, ref countryId, ref provinceId, ref cityId);
            foreach (var province in unitedStates.Provinces)
            {
                foreach (var city in province.Cities)
                    yield return city;
                yield return province;
            }
            yield return unitedStates;

            static Country BuildCanada(Continent continent, ref int countryId, ref int provinceId, ref int cityId)
            {
                var canada = new Country(countryId++, "Canada", continent);

                canada.Provinces.AddRange(new[]
                {
                    BuildBritishColumbia(continent, canada, ref provinceId, ref cityId),
                    BuildAlberta(continent, canada, ref provinceId, ref cityId),
                    BuildSaskatchwean(continent, canada, ref provinceId, ref cityId),
                    BuildManitoba(continent, canada, ref provinceId, ref cityId),
                    BuildOntario(continent, canada, ref provinceId, ref cityId),
                    BuildQuebec(continent, canada, ref provinceId, ref cityId),
                    BuildNewfoundland(continent, canada, ref provinceId, ref cityId),
                    BuildNewBrunswick(continent, canada, ref provinceId, ref cityId),
                    BuildNovaScotia(continent, canada, ref provinceId, ref cityId),
                    BuildPrinceEdwardIsland(continent, canada, ref provinceId, ref cityId),
                    BuildYukon(continent, canada, ref provinceId, ref cityId),
                    BuildNorthwestTerritories(continent, canada, ref provinceId, ref cityId),
                    BuildNunavut(continent, canada, ref provinceId, ref cityId)
                });

                return canada;
            }
            static Province BuildBritishColumbia(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var britishColumbia = new Province(provinceId++, "British Columbia", country, continent);
                var vancouver = new City(cityId++, "Vancouver", britishColumbia, country, continent);
                var victoria = new City(cityId++, "Victoria", britishColumbia, country, continent);

                britishColumbia.Cities.AddRange(new[] { vancouver, victoria });
                return britishColumbia;
            }
            static Province BuildAlberta(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var alberta = new Province(provinceId++, "Alberta", country, continent);
                var edmonton = new City(cityId++, "Edmonton", alberta, country, continent);
                var calgary = new City(cityId++, "Calgary", alberta, country, continent);
                var banff = new City(cityId++, "Banff", alberta, country, continent);
                var jasper = new City(cityId++, "Jasper", alberta, country, continent);

                alberta.Cities.AddRange(new[] {edmonton, calgary, banff, jasper});

                return alberta;
            }
            static Province BuildSaskatchwean(Continent continent, Country country, ref int provinceId, ref int cityId) {
                var saskatchewan = new Province(provinceId++, "Saskatchewan", country, continent);
                var regina = new City(cityId++, "Regina", saskatchewan, country, continent);
                var saskatoon = new City(cityId++, "Saskatoon", saskatchewan, country, continent);
                var mooseJaw = new City(cityId++, "Moose Jaw", saskatchewan, country, continent);
                saskatchewan.Cities.AddRange(new[] { regina, saskatoon, mooseJaw });
                return saskatchewan;
            }
            static Province BuildManitoba(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var manitoba = new Province(provinceId++, "Manitoba", country, continent);
                var winnipeg = new City(cityId++, "Winnipeg", manitoba, country, continent);
                manitoba.Cities.Add(winnipeg);
                return manitoba;
            }
            static Province BuildOntario(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var ontario = new Province(provinceId++, "Ontario", country, continent);
                var toronto = new City(cityId++, "Toronto", ontario, country, continent);
                var ottawa = new City(cityId++, "Ottawa", ontario, country, continent);
                var guelph = new City(cityId++, "Guelph", ontario, country, continent);
                var kitchener = new City(cityId++, "Kitchener", ontario, country, continent);
                var waterloo = new City(cityId++, "Waterloo", ontario, country, continent);
                var cambridge = new City(cityId++, "Cambridge", ontario, country, continent);
                var london = new City(cityId++, "London", ontario, country, continent);
                var sudbury = new City(cityId++, "Sudbury", ontario, country, continent);

                ontario.Cities.AddRange(new[]
                {
                    toronto, 
                    ottawa, 
                    guelph, 
                    kitchener, 
                    waterloo, 
                    cambridge, 
                    london, 
                    sudbury
                });
                return ontario;
            }
            static Province BuildQuebec(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var quebec = new Province(provinceId++, "Quebec", country, continent);
                var quebecCity = new City(cityId++, "Quebec City", quebec, country, continent);
                var montreal = new City(cityId++, "Montreal", quebec, country, continent);

                quebec.Cities.AddRange(new[] {quebecCity, montreal});
                return quebec;
            }
            static Province BuildNewfoundland(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var newfoundland = new Province(provinceId++, "Newfoundland", country, continent);
                var stJohns = new City(cityId++, "St. Johns", newfoundland, country, continent);
                var mountPearl = new City(cityId++, "Mount Pearl", newfoundland, country, continent);
                newfoundland.Cities.AddRange(new[] {stJohns, mountPearl});
                return newfoundland;
            }
            static Province BuildNovaScotia(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var novaScotia = new Province(provinceId++, "Nova Scotia", country, continent);
                var halifax = new City(cityId++, "Halifax", novaScotia, country, continent);
                var sydney = new City(cityId++, "Sydney", novaScotia, country, continent);
                novaScotia.Cities.AddRange(new[] { halifax, sydney });
                return novaScotia;
            }
            static Province BuildNewBrunswick(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var newBrunswick = new Province(provinceId++, "New Brunswick", country, continent);
                var moncton = new City(cityId++, "Moncton", newBrunswick, country, continent);
                var saintJohn = new City(cityId++, "Saint John", newBrunswick, country, continent);

                newBrunswick.Cities.AddRange(new[] {moncton, saintJohn});
                return newBrunswick;
            }
            static Province BuildPrinceEdwardIsland(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var princeEdwardIsland = new Province(provinceId++, "Prince Edward Island", country, continent);
                var charlottetown = new City(cityId++, "Charlottetown", princeEdwardIsland, country, continent);
                var oLeary = new City(cityId++, "O'Leary", princeEdwardIsland, country, continent);

                princeEdwardIsland.Cities.AddRange(new[] { charlottetown, oLeary });
                return princeEdwardIsland;
            }
            static Province BuildYukon(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var yukon = new Province(provinceId++, "Yukon", country, continent);
                var whitehorse = new City(cityId++, "Whitehorse", yukon, country, continent);
                var destructionBay = new City(cityId++, "Destruction Bay", yukon, country, continent);

                yukon.Cities.AddRange(new[] {whitehorse, destructionBay});
                return yukon;
            }
            static Province BuildNorthwestTerritories(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var northwestTerritories = new Province(provinceId++, "Northwest Territories", country, continent);
                var yellowknife = new City(cityId++, "Yellowknife", northwestTerritories, country, continent);
                var tuktoyaktuk = new City(cityId++, "Tuktoyaktuk", northwestTerritories, country, continent);

                northwestTerritories.Cities.AddRange(new[] {yellowknife, tuktoyaktuk});
                return northwestTerritories;
            }
            static Province BuildNunavut(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var nunavut = new Province(provinceId++, "Nunavut", country, continent);
                var iqaluit = new City(cityId++, "Iqaluit", nunavut, country, continent);
                var kugluktuk = new City(cityId++, "Kugluktuk", nunavut, country, continent);

                nunavut.Cities.AddRange(new[] {iqaluit, kugluktuk});
                return nunavut;
            }

            static Country BuildUnitedStates(Continent continent, ref int countryId, ref int provinceId, ref int cityId)
            {
                var unitedStates = new Country(countryId++, "United States", continent);
                unitedStates.Provinces.AddRange(new[]
                {
                    BuildCalifornia(continent, unitedStates, ref provinceId, ref cityId),
                    BuildNewYork(continent, unitedStates, ref provinceId, ref cityId),
                    BuildMassachusetts(continent, unitedStates, ref provinceId, ref cityId),
                });

                return unitedStates;
            }
            static Province BuildCalifornia(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var california = new Province(provinceId++, "California", country, continent);
                var losAngeles = new City(cityId++, "Los Angeles", california, country, continent);
                var sanDiego = new City(cityId++, "San Diego", california, country, continent);
                var sanFransisco = new City(cityId++, "San Fransisco", california, country, continent);

                california.Cities.AddRange(new[] {losAngeles, sanDiego, sanFransisco});
                return california;
            }
            static Province BuildNewYork(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var newYork = new Province(provinceId++, "California", country, continent);
                var newYorkCity = new City(cityId++, "New York", newYork, country, continent);
                var albany = new City(cityId++, "Albany", newYork, country, continent);
                var buffalo = new City(cityId++, "Buffalo", newYork, country, continent);

                newYork.Cities.AddRange(new[] {newYorkCity, albany, buffalo});
                return newYork;
            }
            static Province BuildMassachusetts(Continent continent, Country country, ref int provinceId, ref int cityId)
            {
                var massachusetts = new Province(provinceId++, "Massachusetts", country, continent);
                var boston = new City(cityId++, "Boston", massachusetts, country, continent);
                var cambridge = new City(cityId++, "Cambridge", massachusetts, country, continent);
                var springfield = new City(cityId++, "Springfield", massachusetts, country, continent);

                massachusetts.Cities.AddRange(new[] {boston, cambridge, springfield});
                return massachusetts;
            }
        }
    }
}
