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
using System.Threading.Tasks;
using Vulnerable.Domain.Contracts.Queries;
using Vulnerable.Domain.Entities;
using Vulnerable.Shared;

namespace Vulnerable.Net48.Infrastructure.Data.Repositories.Queries
{
    public sealed class CountryRepository : ICountryRepository
    {
        private readonly AddressDbContext _dbContext;

        public CountryRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Get the name and id of all countries
        /// </summary>
        public Task<(int Id, string Name)[]> GetCountries(int pageNumber, int pageSize)
        {
            return _dbContext.Countries
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new {c.Id, c.Name})
                .ToArrayAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    return t.Result.Select(p => (p.Id, p.Name)).ToArray();
                });
        }

        public Task<Country?> GetCountryById(int id) =>
            _dbContext.Countries.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);

        public Task<Country?> GetCountryByName(string name)
        {
            var query = $"select * from Countries where name = '{name}'";

            return _dbContext.Countries
                .SqlQuery(query)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public Task<(int Id, string Name)[]> GetCountriesByContinentId(int continentId, int pageNumber, int pageSize) =>
            _dbContext.Countries
                .AsNoTracking()
                .Where(c => c.ContinentId == continentId)
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new {e.Id, e.Name})
                .ToArrayAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    return t.Result.Select(p => (p.Id, p.Name)).ToArray();
                });

        public Task<(int Id, string Name)[]> GetCountriesByContinentName(string continentName, int pageNumber, int pageSize) => 
            _dbContext.Countries
                .AsNoTracking()
                .Where(c => c.Continent != null && c.Continent.Name == continentName)
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new {e.Id, e.Name})
                .ToArrayAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    return t.Result.Select(p => (p.Id, p.Name)).ToArray();
                });

        public Task<string[]> GetCountryNamesLikeName(string name, int pageNumber, int pageSize)
        {
            var query = $"select * from Countries where name like '%{name}%'";

            return Task.FromResult(_dbContext.Countries
                .SqlQuery(query)
                .AsNoTracking()
                .Select(c => c.Name)
                .ToArray());
        }

        public Task<int> GetTotalCountOfCountries() =>
            _dbContext.Countries.AsNoTracking().CountAsync();

        public Task<int> GetTotalCountOfCountriesByContinentId(int continentId) =>
            _dbContext.Countries
                .AsNoTracking()
                .Where(c => c.ContinentId == continentId)
                .CountAsync();

        public Task<int> GetTotalCountOfCountriesByContinentName(string continentName) =>
            _dbContext.Countries
                .AsNoTracking()
                .Where(c => c.Continent != null && c.Continent.Name == continentName)
                .CountAsync();

        public Task<int> GetTotalCountOfCountryNamesLikeName(string name)
        {
            var query = $"select * from Countries where name like '%{name}%'";
            return _dbContext.Countries
                .SqlQuery(query)
                .AsNoTracking()
                .CountAsync();
        }
    }
}
