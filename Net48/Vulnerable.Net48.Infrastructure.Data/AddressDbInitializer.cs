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
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Infrastructure.Data.Net48
{
    public sealed class AddressDbInitializer : DropCreateDatabaseAlways<AddressDbContext>
    {
        protected override void Seed(AddressDbContext context)
        {
            foreach (var entity in GetEntities())
                switch (entity)
                {
                    case City city:
                        context.Cities.AddOrUpdate(city);
                        break;
                    case Province province:
                        context.Provinces.AddOrUpdate(province);
                        break;
                    case Country country:
                        context.Countries.AddOrUpdate(country);
                        break;
                    case Continent continent:
                        context.Continents.AddOrUpdate(continent);
                        break;
                }

            base.Seed(context);
        }

        // TODO: shift this to an external SQL file or even inline string
        private static IEnumerable<Entity> GetEntities()
        {
            int countryId = 1;
            int provinceId = 1;
            int cityId = 1;

            var northAmerica = new Continent(1, "North America");
            yield return northAmerica;

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
                var britishColumbia = new Province(provinceId++, "British Columbia", country);
                var vancouver = new City(cityId++, "Vancouver", britishColumbia, country);
                var victoria = new City(cityId++, "Victoria", britishColumbia, country);

                britishColumbia.Cities.AddRange(new[] { vancouver, victoria });
                return britishColumbia;
            }
            static Province BuildAlberta(Country country, ref int provinceId, ref int cityId)
            {
                var alberta = new Province(provinceId++, "Alberta", country);
                var edmonton = new City(cityId++, "Edmonton", alberta, country);
                var calgary = new City(cityId++, "Calgary", alberta, country);
                var banff = new City(cityId++, "Banff", alberta, country);
                var jasper = new City(cityId++, "Jasper", alberta, country);

                alberta.Cities.AddRange(new[] {edmonton, calgary, banff, jasper});

                return alberta;
            }
            static Province BuildSaskatchwean(Country country, ref int provinceId, ref int cityId) {
                var saskatchewan = new Province(provinceId++, "Saskatchewan", country);
                var regina = new City(cityId++, "Regina", saskatchewan, country);
                var saskatoon = new City(cityId++, "Saskatoon", saskatchewan, country);
                var mooseJaw = new City(cityId++, "Moose Jaw", saskatchewan, country);
                saskatchewan.Cities.AddRange(new[] { regina, saskatoon, mooseJaw });
                return saskatchewan;
            }
            static Province BuildManitoba(Country country, ref int provinceId, ref int cityId)
            {
                var manitoba = new Province(provinceId++, "Manitoba", country);
                var winnipeg = new City(cityId++, "Winnipeg", manitoba, country);
                manitoba.Cities.Add(winnipeg);
                return manitoba;
            }
            static Province BuildOntario(Country country, ref int provinceId, ref int cityId)
            {
                var ontario = new Province(provinceId++, "Ontario", country);
                var toronto = new City(cityId++, "Toronto", ontario, country);
                var ottawa = new City(cityId++, "Ottawa", ontario, country);
                var guelph = new City(cityId++, "Guelph", ontario, country);
                var kitchener = new City(cityId++, "Kitchener", ontario, country);
                var waterloo = new City(cityId++, "Waterloo", ontario, country);
                var cambridge = new City(cityId++, "Cambridge", ontario, country);
                var london = new City(cityId++, "London", ontario, country);
                var sudbury = new City(cityId++, "Sudbury", ontario, country);

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
                var quebec = new Province(provinceId++, "Quebec", country);
                var quebecCity = new City(cityId++, "Quebec City", quebec, country);
                var montreal = new City(cityId++, "Montreal", quebec, country);

                quebec.Cities.AddRange(new[] {quebecCity, montreal});
                return quebec;
            }
            static Province BuildNewfoundland(Country country, ref int provinceId, ref int cityId)
            {
                var newfoundland = new Province(provinceId++, "Newfoundland", country);
                var stJohns = new City(cityId++, "St. Johns", newfoundland, country);
                var mountPearl = new City(cityId++, "Mount Pearl", newfoundland, country);
                newfoundland.Cities.AddRange(new[] {stJohns, mountPearl});
                return newfoundland;
            }
            static Province BuildNovaScotia(Country country, ref int provinceId, ref int cityId)
            {
                var novaScotia = new Province(provinceId++, "Nova Scotia", country);
                var halifax = new City(cityId++, "Halifax", novaScotia, country);
                var sydney = new City(cityId++, "Sydney", novaScotia, country);
                novaScotia.Cities.AddRange(new[] { halifax, sydney });
                return novaScotia;
            }
            static Province BuildNewBrunswick(Country country, ref int provinceId, ref int cityId)
            {
                var newBrunswick = new Province(provinceId++, "New Brunswick", country);
                var moncton = new City(cityId++, "Moncton", newBrunswick, country);
                var saintJohn = new City(cityId++, "Saint John", newBrunswick, country);

                newBrunswick.Cities.AddRange(new[] {moncton, saintJohn});
                return newBrunswick;
            }
            static Province BuildPrinceEdwardIsland(Country country, ref int provinceId, ref int cityId)
            {
                var princeEdwardIsland = new Province(provinceId++, "Prince Edward Island", country);
                var charlottetown = new City(cityId++, "Charlottetown", princeEdwardIsland, country);
                var oLeary = new City(cityId++, "O'Leary", princeEdwardIsland, country);

                princeEdwardIsland.Cities.AddRange(new[] { charlottetown, oLeary });
                return princeEdwardIsland;
            }
            static Province BuildYukon(Country country, ref int provinceId, ref int cityId)
            {
                var yukon = new Province(provinceId++, "Yukon", country);
                var whitehorse = new City(cityId++, "Whitehorse", yukon, country);
                var destructionBay = new City(cityId++, "Destruction Bay", yukon, country);

                yukon.Cities.AddRange(new[] {whitehorse, destructionBay});
                return yukon;
            }
            static Province BuildNorthwestTerritories(Country country, ref int provinceId, ref int cityId)
            {
                var northwestTerritories = new Province(provinceId++, "Northwest Territories", country);
                var yellowknife = new City(cityId++, "Yellowknife", northwestTerritories, country);
                var tuktoyaktuk = new City(cityId++, "Tuktoyaktuk", northwestTerritories, country);

                northwestTerritories.Cities.AddRange(new[] {yellowknife, tuktoyaktuk});
                return northwestTerritories;
            }
            static Province BuildNunavut(Country country, ref int provinceId, ref int cityId)
            {
                var nunavut = new Province(provinceId++, "Nunavut", country);
                var iqaluit = new City(cityId++, "Iqaluit", nunavut, country);
                var kugluktuk = new City(cityId++, "Kugluktuk", nunavut, country);

                nunavut.Cities.AddRange(new[] {iqaluit, kugluktuk});
                return nunavut;
            }

            static Country BuildUnitedStates(Continent continent, ref int countryId, ref int provinceId, ref int cityId)
            {
                var unitedStates = new Country(countryId++, "United States", continent);
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
                var california = new Province(provinceId++, "California", country);
                var losAngeles = new City(cityId++, "Los Angeles", california, country);
                var sanDiego = new City(cityId++, "San Diego", california, country);
                var sanFransisco = new City(cityId++, "San Fransisco", california, country);

                california.Cities.AddRange(new[] {losAngeles, sanDiego, sanFransisco});
                return california;
            }
            static Province BuildNewYork(Country country, ref int provinceId, ref int cityId)
            {
                var newYork = new Province(provinceId++, "California", country);
                var newYorkCity = new City(cityId++, "New York", newYork, country);
                var albany = new City(cityId++, "Albany", newYork, country);
                var buffalo = new City(cityId++, "Buffalo", newYork, country);

                newYork.Cities.AddRange(new[] {newYorkCity, albany, buffalo});
                return newYork;
            }
            static Province BuildMassachusetts(Country country, ref int provinceId, ref int cityId)
            {
                var massachusetts = new Province(provinceId++, "Massachusetts", country);
                var boston = new City(cityId++, "Boston", massachusetts, country);
                var cambridge = new City(cityId++, "Cambridge", massachusetts, country);
                var springfield = new City(cityId++, "Springfield", massachusetts, country);

                massachusetts.Cities.AddRange(new[] {boston, cambridge, springfield});
                return massachusetts;
            }
        }
    }
}
