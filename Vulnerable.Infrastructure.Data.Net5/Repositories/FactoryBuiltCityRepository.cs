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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Infrastructure.Data.Net5.Repositories
{
    public sealed class FactoryBuiltCityRepository : ICityRepository
    {
        private readonly IDbContextFactory<AddressDbContext> _dbContextFactory;

        public FactoryBuiltCityRepository(IDbContextFactory<AddressDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        /// <inheritdoc/>
        public async Task<string[]> GetAllCityNames(int pageNumber, int pageSize)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Cities
                .AsNoTracking()
                .Select(c => c.Name)
                .Skip(pageSize*(pageNumber-1))
                .Take(pageSize)
                .ToArrayAsync();
        }

        /// <summary>
        /// Gets the total count of cities
        /// </summary>
        public async Task<int> GetTotalCountOfCities()
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Cities
                .AsNoTracking()
                .CountAsync();
        }

        /// <inheritdoc/>
        public async Task<City?> GetCityByName(string name)
        {
            // intentional SQL Injeciton risk
            var query = $"select * from Cities where Name = '{name}'";

            await using var context = _dbContextFactory.CreateDbContext();
            var city = await context.Cities.FromSqlRaw(query).AsNoTracking().FirstOrDefaultAsync();

            if (city == null)
                return null;

            var province = await context.Provinces
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
            var query = $"select * from Cities where Name Like '%{name}%'";

            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Cities
                .FromSqlRaw(query)
                .AsNoTracking()
                .Select(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToArrayAsync();
        }

        /// <summary>
        /// Gets the total count of city names matching <paramref name="name"/>
        /// </summary>
        public async Task<int> GetTotalCountOfCityNamesLikeName(string name)
        {
            // intentional SQL Injeciton risk
            var query = $"select * from Cities where Name Like '%{name}%'";

            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Cities
                .FromSqlRaw(query)
                .AsNoTracking()
                .CountAsync();

        }
    }
}
