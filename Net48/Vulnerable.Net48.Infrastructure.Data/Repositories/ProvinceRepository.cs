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

using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Domain.Entities;
using Vulnerable.Shared;

namespace Vulnerable.Net48.Infrastructure.Data.Repositories
{
    public sealed class ProvinceRepository : IProvinceRepository
    {
        private readonly AddressDbContext _dbContext;

        public ProvinceRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Get the name and id of all countries
        /// </summary>
        public Task<(int Id, string Name)[]> GetProvinces(int pageNumber, int pageSize)
        {
            return _dbContext.Provinces
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new {p.Id, p.Name})
                .ToArrayAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    return t.Result.Select(p => (p.Id, p.Name)).ToArray();
                });
        }

        /// <inheritdoc/>
        public Task<Province?> GetProvinceById(int id) =>
            _dbContext.Provinces.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);

        /// <inheritdoc/>
        public Task<Province?> GetProvinceByName(string name)
        {
            var query = $"select * from Provinces where name='{name}'";

            return _dbContext.Provinces
                .SqlQuery(query)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<string[]> GetAllProvinceNames(int pageNumber, int pageSize) =>
            _dbContext.Provinces.AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => p.Name)
                .ToArrayAsync();

        /// <inheritdoc/>
        public Task<string[]> GetProvinceNamesLikeName(string name, int pageNumber, int pageSize)
        {
            var query = $"select * from Provinces where name like '%{name}%'";

            return Task.FromResult(_dbContext.Provinces
                .SqlQuery(query)
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArray());
        }

        /// <inheritdoc/>
        public Task<Province[]> GetProvincesByCountryId(int countryId, int pageNumber, int pageSize) =>
            _dbContext.Provinces
                .AsNoTracking()
                .Where(p => p.CountryId == countryId)
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();

        /// <inheritdoc/>
        public Task<Province[]> GetProvincesByCountryName(string countryName, int pageNumber, int pageSize) => 
            _dbContext.Provinces
                .AsNoTracking()
                .Where(p => p.Country != null && p.Country.Name == countryName)
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();


        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvinceNamesLikeName(string name)
        {
            var query = $"select * from Provinces where name like '%{name}%'";

            return Task.FromResult(_dbContext.Provinces
                .SqlQuery(query)
                .AsNoTracking()
                .Count());
        }

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvinces() =>
            _dbContext.Provinces.AsNoTracking().CountAsync();

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvincesByCountryId(int countryId) =>
            _dbContext.Provinces
                .AsNoTracking()
                .Where(p => p.CountryId == countryId)
                .CountAsync();

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvincesByCountryName(string countryName) => 
            _dbContext.Provinces
                .AsNoTracking()
                .Where(p => p.Country != null && p.Country.Name == countryName)
                .CountAsync();
    }
}
