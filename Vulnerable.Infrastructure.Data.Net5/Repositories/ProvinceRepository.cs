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
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Domain.Entities;
using Vulnerable.Shared;

namespace Vulnerable.Infrastructure.Data.Net5.Repositories
{
    public sealed class ProvinceRepository : IProvinceRepository
    {
        private readonly AddressDbContext _dbContext;

        public ProvinceRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        public Task<City[]> GetCitiesByProvinceId(int provinceId, int pageSize, int pageNumber)
        {
            return _dbContext.Cities
                .AsNoTracking()
                .Where(c => c.ProvinceId == provinceId)
                .Include(c => c.Province)
                .Include(c => c.Country)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
        }

        public Task<City[]> GetCitiesByProvinceName(string provinceName, int pageSize, int pageNumber)
        {
            return _dbContext.Cities
                .AsNoTracking()
                .Where(c => c.Province != null && c.Province.Name == provinceName)
                .Include(c => c.Province)
                .Include(c => c.Country)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
        }

        public Task<Province?> GetProvinceByName(string name)
        {
            var query = $"select * from Provinces where Name = '{name}'";

            return _dbContext.Provinces.FromSqlRaw(query).AsNoTracking().FirstOrDefaultAsync()
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    var province = t.Result;
                    if (province == null)
                        return null;

                    var country = _dbContext.Countries
                        .AsNoTracking()
                        .FirstOrDefault(c => c.Id == province.CountryId);
                    if (country != null)
                        province.SetCountry(country);

                    return province;
                });
        }

        public Task<string[]> GetProvinceNames(int pageSize, int pageNumber)
        {
            return _dbContext.Provinces.AsNoTracking()
                .Select(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
        }

        public Task<string[]> GetProvinceNamesLikeName(string name, int pageSize, int pageNumber)
        {
            var query = $"select * from Provinces where Name Like '%{name}%'";
            return _dbContext.Provinces
                .FromSqlRaw(query)
                .AsNoTracking()
                .Select(p => p.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToArrayAsync();

        }

        public Task<int> GetTotalCountOfCitiesByProvinceId(int provinceId)
        {
            return _dbContext.Cities
                .AsNoTracking()
                .Where(c => c.ProvinceId == provinceId)
                .CountAsync();
        }

        public Task<int> GetTotalCountOfCitiesByProvinceName(string provinceName)
        {
            return _dbContext.Cities
                .AsNoTracking()
                .Where(c => c.Province != null && c.Province.Name == provinceName)
                .CountAsync();
        }

        public Task<int> GetTotalCountOfProvinceNamesLikeName(string name)
        {
            var query = $"select * from Provinces where Name Like '%{name}%'";
            return _dbContext.Provinces
                .FromSqlRaw(query)
                .AsNoTracking()
                .CountAsync();
        }

        public Task<int> GetTotalCountOfProvinces()
        {
            return _dbContext.Provinces.AsNoTracking()
                .CountAsync();
        }
    }
}
