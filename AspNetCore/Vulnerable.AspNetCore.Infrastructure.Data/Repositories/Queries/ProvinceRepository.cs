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

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vulnerable.Domain.Contracts.Queries;
using Vulnerable.Domain.Entities;
using Vulnerable.Shared;

namespace Vulnerable.Net.Infrastructure.Data.Repositories.Queries
{
    public sealed class ProvinceRepository : IProvinceRepository
    {
        private readonly AddressDbContext _dbContext;

        public ProvinceRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public Task<Province?> GetProvinceById(int id)
        {
            return _dbContext.Provinces.AsNoTracking()
                .Include(p => p.Country)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        /// <inheritdoc />
        public Task<Province?> GetProvinceWithCitiesById(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Province?> GetProvinceByName(string name)
        {
            string query = $"select * from Provinces where Name = '{name}'";

            return _dbContext.Provinces.FromSqlRaw(query).AsNoTracking().FirstOrDefaultAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    Province? province = t.Result;
                    if (province == null)
                        return null;

                    Country? country = _dbContext.Countries
                        .AsNoTracking()
                        .FirstOrDefault(c => c.Id == province.CountryId);
                    if (country != null)
                        province.SetCountry(country);

                    return province;
                });
        }

        /// <inheritdoc />
        public Task<Province?> GetProvinceWithCitiesByName(string name)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<(int Id, string Name)[]> GetProvinces(int pageNumber, int pageSize)
        {
            return _dbContext.Provinces.AsNoTracking()
                .Select(p => new { p.Id, p.Name })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsAsyncEnumerable()
                .Select(p => (p.Id, p.Name))
                .ToArrayAsync()
                .AsTask();
        }

        /// <inheritdoc/>
        public Task<string[]> GetProvinceNamesLikeName(string name, int pageSize, int pageNumber)
        {
            string query = $"select * from Provinces where Name Like '%{name}%'";
            return _dbContext.Provinces
                .FromSqlRaw(query)
                .AsNoTracking()
                .Select(p => p.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToArrayAsync();

        }

        public Task<int> GetTotalCountOfProvinceNamesLikeName(string name)
        {
            string query = $"select * from Provinces where Name Like '%{name}%'";
            return _dbContext.Provinces
                .FromSqlRaw(query)
                .AsNoTracking()
                .CountAsync();
        }

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvinces()
        {
            return _dbContext.Provinces.AsNoTracking()
                .CountAsync();
        }

        /// <inheritdoc />
        public Task<(int Id, string Name)[]> GetProvincesByCountryId(int countryId, int pageNumber, int pageSize)
        {
            return _dbContext.Provinces.AsNoTracking()
                .Where(p => p.CountryId == countryId)
                .Select(p => new { p.Id, p.Name })
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .AsAsyncEnumerable()
                .Select(p => (p.Id, p.Name))
                .ToArrayAsync()
                .AsTask();
        }

        /// <inheritdoc />
        public Task<int> GetTotalCountOfProvincesByCountryId(int countryId)
        {
            return _dbContext.Provinces.AsNoTracking()
                .Where(p => p.CountryId == countryId)
                .CountAsync();
        }

        /// <inheritdoc />
        public Task<(int Id, string Name)[]> GetProvincesByCountryName(string countryName, int pageNumber, int pageSize)
        {
            return _dbContext.Provinces.AsNoTracking()
                .Include(p => p.Country)
                .Where(p => p.Country!.Name == countryName)
                .Select(p => new { p.Id, p.Name })
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .AsAsyncEnumerable()
                .Select(p => (p.Id, p.Name))
                .ToArrayAsync()
                .AsTask();
        }

        /// <inheritdoc />
        public Task<int> GetTotalCountOfProvincesByCountryName(string countryName)
        {
            return _dbContext.Provinces.AsNoTracking()
                .Include(p => p.Country)
                .Where(p => p.Country!.Name == countryName)
                .CountAsync();
        }
    }
}
