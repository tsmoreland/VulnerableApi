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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vulnerable.Domain.Contracts.Queries;
using Vulnerable.Domain.Entities;
using Vulnerable.Shared;

namespace Vulnerable.Net.Infrastructure.Data.Repositories.Queries
{
    public sealed class FactoryBuiltCityRepository : ICityRepository
    {
        private readonly IDbContextFactory<AddressDbContext> _dbContextFactory;

        public FactoryBuiltCityRepository(IDbContextFactory<AddressDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        /// <summary>
        /// Get the name and id of all cities
        /// </summary>
        public async Task<(int Id, string Name)[]> GetCities(int pageNumber, int pageSize)
        {
            await using AddressDbContext context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Cities
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


        /// <inheritdoc/>
        public async Task<int> GetTotalCountOfCities()
        {
            await using AddressDbContext context = _dbContextFactory.CreateDbContext();
            return await context.Cities
                .AsNoTracking()
                .CountAsync();
        }

        /// <inheritdoc/>
        public async Task<City?> GetCityById(int id)
        {
            await using AddressDbContext context = _dbContextFactory.CreateDbContext();
            return await context.Cities.AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <inheritdoc/>
        public async Task<City?> GetCityByName(string name)
        {
            // intentional SQL Injeciton risk
            string query = $"select * from Cities where Name = '{name}'";

            await using AddressDbContext context = _dbContextFactory.CreateDbContext();
            City? city = await context.Cities.FromSqlRaw(query).AsNoTracking().FirstOrDefaultAsync();

            if (city == null)
                return null;

            Province? province = await context.Provinces
                .AsNoTracking()
                .Include(p => p.Country)
                .FirstOrDefaultAsync(p => p.Id == city.ProvinceId);
            if (province != null)
                city.SetCountryAndProvince(province);
            return city;
        }

        /// <inheritdoc/>
        public async Task<string[]> GetCityNamesLikeName(string name, int pageNumber, int pageSize)
        {
            // intentional SQL Injeciton risk
            string query = $"select * from Cities where Name Like '%{name}%'";

            await using AddressDbContext context = _dbContextFactory.CreateDbContext();
            return await context.Cities
                .FromSqlRaw(query)
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToArrayAsync();
        }

        /// <inheritdoc/>
        public async Task<int> GetTotalCountOfCityNamesLikeName(string name)
        {
            // intentional SQL Injeciton risk
            string query = $"select * from Cities where Name Like '%{name}%'";

            await using AddressDbContext context = _dbContextFactory.CreateDbContext();
            return await context.Cities
                .FromSqlRaw(query)
                .AsNoTracking()
                .CountAsync();
        }

        /// <inheritdoc/>
        public Task<(int Id, string Name)[]> GetCitiesByProvinceId(int provinceId, int pageNumber, int pageSize) =>
            GetCitiesBy(e => e.ProvinceId == provinceId, pageNumber, pageSize);

        /// <inheritdoc/>
        public Task<(int Id, string Name)[]> GetCitiesByProvinceName(string provinceName, int pageNumber, int pageSize) =>
            GetCitiesBy(e => e.Province != null && e.Province.Name == provinceName, pageNumber, pageSize);

        /// <inheritdoc/>
        public Task<(int Id, string Name)[]> GetCitiesByCountryId(int countryId, int pageNumber, int pageSize) =>
            GetCitiesBy(e => e.CountryId == countryId, pageNumber, pageSize);

        /// <inheritdoc/>
        public Task<(int Id, string Name)[]> GetCitiesByCountryName(string countryName, int pageNumber, int pageSize) =>
            GetCitiesBy(e => e.Country != null && e.Country.Name == countryName, pageNumber, pageSize);

        /// <inheritdoc/>
        public async Task<int> GetTotalCountOfCitiesBy(Expression<Func<City, bool>> predicate)
        {
            await using AddressDbContext context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Cities
                .AsNoTracking()
                .Where(predicate)
                .CountAsync();
        }

        private async Task<(int Id, string Name)[]> GetCitiesBy(Expression<Func<City, bool>> predicate, int pageNumber, int pageSize)
        {
            await using AddressDbContext context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Cities
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
