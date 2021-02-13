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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Cities.Core.Contracts.Data;
using Vulnerable.Domain.Entities;
using Vulnerable.Domain.Projections;

namespace Vulnerable.Net5.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly IDbContextFactory<AddressDbContext>? _dbContextFactory;
        private readonly AddressDbContext? _dbContext;

        public CityRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        public CityRepository(IDbContextFactory<AddressDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory ?? throw new System.ArgumentNullException(nameof(dbContextFactory));
        }

        /// <inheritdoc/>
        public async Task<PagedCityNames> GetAllCityNames(int pageNumber, int pageSize)
        {
            await using var context = GetDbContext();
            var namesTask = context.Value.Cities
                .AsNoTracking()
                .Select(c => c.Name)
                .Skip(pageNumber*(pageNumber-1))
                .Take(pageNumber)
                .ToArrayAsync();
            var countTask = context.Value.Cities.CountAsync();
            await Task.WhenAll(namesTask, countTask);
            return new PagedCityNames
            {
                Count = countTask.Result,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Names = namesTask.Result
            };
        }

        /// <inheritdoc/>
        public Task<City?> GetCityByName(string name)
        {
            // intentional SQL Injeciton risk
            var query = $"select * from Cities where Name = '{name}'";

            using var context = GetDbContext();

            var city = context.Value.Cities.FromSqlRaw(query).AsNoTracking().FirstOrDefault();

            if (city == null)
                return Task.FromResult((City?)null);

            var province = context.Value.Provinces
                .AsNoTracking()
                .Include(p => p.Country)
                .FirstOrDefault(p => p.Id == city.ProvinceId);
            if (province != null)
                city.SetCountryAndProvince(province);
            return Task.FromResult<City?>(city);
        }

        /// <inheritdoc/>
        public async Task<PagedCityNames> GetCityNamesLikeName(string name, int pageNumber, int pageSize)
        {
            // intentional SQL Injeciton risk
            var query = $"select * from Cities where Name Like '%{name}%'";

            await using var context = GetDbContext();
            var namesTask = context.Value.Cities
                .FromSqlRaw(query)
                .AsNoTracking()
                .Select(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToArrayAsync();
            var countTask = context.Value.Cities.FromSqlRaw(query).CountAsync();
            await Task.WhenAll(namesTask, countTask);

            return new PagedCityNames
            {
                Count = countTask.Result,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Names = namesTask.Result
            };
        }

        private OptionalDisposal<AddressDbContext> GetDbContext()
        {
            return _dbContext != null
                ? new OptionalDisposal<AddressDbContext>(_dbContext, false)
                : new OptionalDisposal<AddressDbContext>(_dbContextFactory!.CreateDbContext(), true);
        }
    }
}
