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

namespace Vulnerable.Net.Infrastructure.Data
{
    public static class DataInitializer
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

            Continent northAmerica = new Continent(1, "North America");
            Country canada = BuildCanada(northAmerica, ref countryId, ref provinceId, ref cityId);
            foreach (Province province in canada.Provinces)
            {
                foreach (City city in province.Cities)
                    yield return city;
                yield return province;
            }
            yield return canada;
            
            Country unitedStates = BuildUnitedStates(northAmerica, ref countryId, ref provinceId, ref cityId);
            foreach (Province province in unitedStates.Provinces)
            {
                foreach (City city in province.Cities)
                    yield return city;
                yield return province;
            }
            yield return unitedStates;

            static Country BuildCanada(Continent continent, ref int countryId, ref int provinceId, ref int cityId)
            {
                Country canada = new Country(countryId++, "Canada", continent);

                canada.Provinces.AddRange(new[]
                {
                    BuildBritishColumbia(canada, ref provinceId, ref cityId),
                    BuildAlberta(canada, ref provinceId, ref cityId),
                    BuildSaskatchwean(canada, ref provinceId, ref cityId),
                    BuildManitoba(canada, ref provinceId, ref cityId),
                    BuildOntario(canada, ref provinceId, ref cityId),
                    BuildQuebec(canada, ref provinceId, ref cityId),
                    BuildNewfoundland(canada, ref provinceId, ref cityId),
                    BuildNewBrunswick(canada, ref provinceId, ref cityId),
                    BuildNovaScotia(canada, ref provinceId, ref cityId),
                    BuildPrinceEdwardIsland(canada, ref provinceId, ref cityId),
                    BuildYukon(canada, ref provinceId, ref cityId),
                    BuildNorthwestTerritories(canada, ref provinceId, ref cityId),
                    BuildNunavut(canada, ref provinceId, ref cityId)
                });

                return canada;
            }
            static Province BuildBritishColumbia(Country country, ref int provinceId, ref int cityId)
            {
                Province britishColumbia = new Province(provinceId++, "British Columbia", country);
                City vancouver = new City(cityId++, "Vancouver", britishColumbia, country);
                City victoria = new City(cityId++, "Victoria", britishColumbia, country);

                britishColumbia.Cities.AddRange(new[] { vancouver, victoria });
                return britishColumbia;
            }
            static Province BuildAlberta(Country country, ref int provinceId, ref int cityId)
            {
                Province alberta = new Province(provinceId++, "Alberta", country);
                City edmonton = new City(cityId++, "Edmonton", alberta, country);
                City calgary = new City(cityId++, "Calgary", alberta, country);
                City banff = new City(cityId++, "Banff", alberta, country);
                City jasper = new City(cityId++, "Jasper", alberta, country);

                alberta.Cities.AddRange(new[] {edmonton, calgary, banff, jasper});

                return alberta;
            }
            static Province BuildSaskatchwean(Country country, ref int provinceId, ref int cityId) {
                Province saskatchewan = new Province(provinceId++, "Saskatchewan", country);
                City regina = new City(cityId++, "Regina", saskatchewan, country);
                City saskatoon = new City(cityId++, "Saskatoon", saskatchewan, country);
                City mooseJaw = new City(cityId++, "Moose Jaw", saskatchewan, country);
                saskatchewan.Cities.AddRange(new[] { regina, saskatoon, mooseJaw });
                return saskatchewan;
            }
            static Province BuildManitoba(Country country, ref int provinceId, ref int cityId)
            {
                Province manitoba = new Province(provinceId++, "Manitoba", country);
                City winnipeg = new City(cityId++, "Winnipeg", manitoba, country);
                manitoba.Cities.Add(winnipeg);
                return manitoba;
            }
            static Province BuildOntario(Country country, ref int provinceId, ref int cityId)
            {
                Province ontario = new Province(provinceId++, "Ontario", country);
                City toronto = new City(cityId++, "Toronto", ontario, country);
                City ottawa = new City(cityId++, "Ottawa", ontario, country);
                City guelph = new City(cityId++, "Guelph", ontario, country);
                City kitchener = new City(cityId++, "Kitchener", ontario, country);
                City waterloo = new City(cityId++, "Waterloo", ontario, country);
                City cambridge = new City(cityId++, "Cambridge", ontario, country);
                City london = new City(cityId++, "London", ontario, country);
                City sudbury = new City(cityId++, "Sudbury", ontario, country);

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
            static Province BuildQuebec(Country country, ref int provinceId, ref int cityId)
            {
                Province quebec = new Province(provinceId++, "Quebec", country);
                City quebecCity = new City(cityId++, "Quebec City", quebec, country);
                City montreal = new City(cityId++, "Montreal", quebec, country);

                quebec.Cities.AddRange(new[] {quebecCity, montreal});
                return quebec;
            }
            static Province BuildNewfoundland(Country country, ref int provinceId, ref int cityId)
            {
                Province newfoundland = new Province(provinceId++, "Newfoundland", country);
                City stJohns = new City(cityId++, "St. Johns", newfoundland, country);
                City mountPearl = new City(cityId++, "Mount Pearl", newfoundland, country);
                newfoundland.Cities.AddRange(new[] {stJohns, mountPearl});
                return newfoundland;
            }
            static Province BuildNovaScotia(Country country, ref int provinceId, ref int cityId)
            {
                Province novaScotia = new Province(provinceId++, "Nova Scotia", country);
                City halifax = new City(cityId++, "Halifax", novaScotia, country);
                City sydney = new City(cityId++, "Sydney", novaScotia, country);
                novaScotia.Cities.AddRange(new[] { halifax, sydney });
                return novaScotia;
            }
            static Province BuildNewBrunswick(Country country, ref int provinceId, ref int cityId)
            {
                Province newBrunswick = new Province(provinceId++, "New Brunswick", country);
                City moncton = new City(cityId++, "Moncton", newBrunswick, country);
                City saintJohn = new City(cityId++, "Saint John", newBrunswick, country);

                newBrunswick.Cities.AddRange(new[] {moncton, saintJohn});
                return newBrunswick;
            }
            static Province BuildPrinceEdwardIsland(Country country, ref int provinceId, ref int cityId)
            {
                Province princeEdwardIsland = new Province(provinceId++, "Prince Edward Island", country);
                City charlottetown = new City(cityId++, "Charlottetown", princeEdwardIsland, country);
                City oLeary = new City(cityId++, "O'Leary", princeEdwardIsland, country);

                princeEdwardIsland.Cities.AddRange(new[] { charlottetown, oLeary });
                return princeEdwardIsland;
            }
            static Province BuildYukon(Country country, ref int provinceId, ref int cityId)
            {
                Province yukon = new Province(provinceId++, "Yukon", country);
                City whitehorse = new City(cityId++, "Whitehorse", yukon, country);
                City destructionBay = new City(cityId++, "Destruction Bay", yukon, country);

                yukon.Cities.AddRange(new[] {whitehorse, destructionBay});
                return yukon;
            }
            static Province BuildNorthwestTerritories(Country country, ref int provinceId, ref int cityId)
            {
                Province northwestTerritories = new Province(provinceId++, "Northwest Territories", country);
                City yellowknife = new City(cityId++, "Yellowknife", northwestTerritories, country);
                City tuktoyaktuk = new City(cityId++, "Tuktoyaktuk", northwestTerritories, country);

                northwestTerritories.Cities.AddRange(new[] {yellowknife, tuktoyaktuk});
                return northwestTerritories;
            }
            static Province BuildNunavut(Country country, ref int provinceId, ref int cityId)
            {
                Province nunavut = new Province(provinceId++, "Nunavut", country);
                City iqaluit = new City(cityId++, "Iqaluit", nunavut, country);
                City kugluktuk = new City(cityId++, "Kugluktuk", nunavut, country);

                nunavut.Cities.AddRange(new[] {iqaluit, kugluktuk});
                return nunavut;
            }

            static Country BuildUnitedStates(Continent continent, ref int countryId, ref int provinceId, ref int cityId)
            {
                Country unitedStates = new Country(countryId++, "United States", continent);
                unitedStates.Provinces.AddRange(new[]
                {
                    BuildCalifornia(unitedStates, ref provinceId, ref cityId),
                    BuildNewYork(unitedStates, ref provinceId, ref cityId),
                    BuildMassachusetts(unitedStates, ref provinceId, ref cityId),
                });

                return unitedStates;
            }
            static Province BuildCalifornia(Country country, ref int provinceId, ref int cityId)
            {
                Province california = new Province(provinceId++, "California", country);
                City losAngeles = new City(cityId++, "Los Angeles", california, country);
                City sanDiego = new City(cityId++, "San Diego", california, country);
                City sanFransisco = new City(cityId++, "San Fransisco", california, country);

                california.Cities.AddRange(new[] {losAngeles, sanDiego, sanFransisco});
                return california;
            }
            static Province BuildNewYork(Country country, ref int provinceId, ref int cityId)
            {
                Province newYork = new Province(provinceId++, "California", country);
                City newYorkCity = new City(cityId++, "New York", newYork, country);
                City albany = new City(cityId++, "Albany", newYork, country);
                City buffalo = new City(cityId++, "Buffalo", newYork, country);

                newYork.Cities.AddRange(new[] {newYorkCity, albany, buffalo});
                return newYork;
            }
            static Province BuildMassachusetts(Country country, ref int provinceId, ref int cityId)
            {
                Province massachusetts = new Province(provinceId++, "Massachusetts", country);
                City boston = new City(cityId++, "Boston", massachusetts, country);
                City cambridge = new City(cityId++, "Cambridge", massachusetts, country);
                City springfield = new City(cityId++, "Springfield", massachusetts, country);

                massachusetts.Cities.AddRange(new[] {boston, cambridge, springfield});
                return massachusetts;
            }
        }
    }
}
