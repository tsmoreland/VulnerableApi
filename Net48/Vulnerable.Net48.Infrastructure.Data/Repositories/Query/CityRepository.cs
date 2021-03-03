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

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vulnerable.Domain.Contracts.Query;
using Vulnerable.Domain.Entities;
using Vulnerable.Shared;

namespace Vulnerable.Net48.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Pretty much a copy of the NET5 version but with a few slight variations to
    /// work with EF6
    /// </summary>
    public sealed class CityRepository : ICityRepository
    {
        private readonly AddressDbContext _dbContext;

        public CityRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public Task<(int Id, string Name)[]> GetCities(int pageNumber, int pageSize)
        {
            return _dbContext.Cities
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new {c.Id, c.Name})
                .ToArrayAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    return t.Result.Select(p => (p.Id, p.Name)).ToArray();
                });
        }

        // <inheritdoc/>
        public Task<int> GetTotalCountOfCities()
        {
            return _dbContext.Cities
                .AsNoTracking()
                .CountAsync();
        }

        /// <inheritdoc/>
        public Task<City?> GetCityById(int id)
        {
            return _dbContext.Cities.AsNoTracking()
                .Include(c => c.Province)
                .Include(c => c.Country)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <inheritdoc/>
        public Task<City?> GetCityByName(string name)
        {
            // intentional SQL Injeciton risk
            var query = $"select * from Cities where Name = '{name}'";

            return _dbContext.Cities.SqlQuery(query).AsNoTracking().FirstOrDefaultAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);

                    var city = t.Result;
                    if (city == null)
                        return null;

                    var province = _dbContext.Provinces
                        .AsNoTracking()
                        .Include(p => p.Country)
                        .FirstOrDefault(p => p.Id == city.ProvinceId);
                    if (province != null)
                        city.SetCountryAndProvince(province);
                    return city;
                });
        }

        /// <inheritdoc/>
        public Task<string[]> GetCityNamesLikeName(string name, int pageNumber, int pageSize)
        {
            // intentional SQL Injeciton risk
            var query = $"select * from Cities where Name Like '%{name}%'";

            return Task.FromResult(_dbContext.Cities
                .SqlQuery(query)
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToArray());
        }

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfCityNamesLikeName(string name)
        {
            // intentional SQL Injeciton risk
            var query = $"select * from Cities where Name Like '%{name}%'";
            return _dbContext.Cities
                .SqlQuery(query)
                .AsNoTracking()
                .CountAsync();
        }


        public Task<int> GetTotalCountOfCitiesBy(Expression<Func<City, bool>> predicate)
        {
            return _dbContext.Cities
                .AsNoTracking()
                .Where(predicate)
                .CountAsync();
        }

        public Task<(int Id, string Name)[]> GetCitiesByProvinceId(int provinceId, int pageNumber, int pageSize) =>
            GetCitiesBy(e => e.ProvinceId == provinceId, pageNumber, pageSize);

        public Task<(int Id, string Name)[]> GetCitiesByProvinceName(string provinceName, int pageNumber, int pageSize) =>
            GetCitiesBy(e => e.Province != null && e.Province.Name == provinceName, pageNumber, pageSize);

        public Task<(int Id, string Name)[]> GetCitiesByCountryId(int countryId, int pageNumber, int pageSize) =>
            GetCitiesBy(e => e.CountryId == countryId, pageNumber, pageSize);

        public Task<(int Id, string Name)[]> GetCitiesByCountryName(string countryName, int pageNumber, int pageSize) =>
            GetCitiesBy(e => e.Country != null && e.Country.Name == countryName, pageNumber, pageSize);

        private Task<(int Id, string Name)[]> GetCitiesBy(Expression<Func<City, bool>> predicate, int pageNumber, int pageSize)
        {
            return _dbContext.Cities
                .AsNoTracking()
                .Include(c => c.Province)
                .Include(c => c.Country)
                .Where(predicate)
                .OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(e => new {e.Id, e.Name})
                .ToArrayAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    return t.Result.Select(p => (p.Id, p.Name)).ToArray();
                });
        }
    }
}
