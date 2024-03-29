﻿//
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
    public sealed class ContinentRepository : IContinentRepository
    {
        private readonly AddressDbContext _dbContext;

        public ContinentRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<Continent?> GetContinentById(int id)
        {
            return _dbContext.Continents
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task<Continent?> GetContinentByName(string name)
        {
            var query = $"select * from Continents where name='{name}'";
            return _dbContext.Continents
                .SqlQuery(query)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public Task<string[]> GetContinentNames(int pageNumber, int pageSize)
        {
            return _dbContext.Continents
                .AsNoTracking()
                .Select(c => c.Name)
                .Skip((pageNumber -1)*pageSize)
                .Take(pageSize)
                .ToArrayAsync();
        }

        public Task<string[]> GetContinentNamesLikeName(string name, int pageNumber, int pageSize)
        {
            var query = $"select * from Continents where name like '%{name}%'";
            return Task.FromResult(_dbContext.Continents
                .SqlQuery(query)
                .AsNoTracking()
                .Select(c => c.Name)
                .Skip((pageNumber -1)*pageSize)
                .Take(pageSize)
                .ToArray());
        }

        public Task<(int Id, string Name)[]> GetContinents(int pageNumber, int pageSize)
        {
            return _dbContext.Continents
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

        public Task<int> GetTotalCountOfContinentNamesLikeName(string name)
        {
            var query = $"select * from Continents where name like '%{name}%'";
            return _dbContext.Continents
                .SqlQuery(query)
                .AsNoTracking()
                .CountAsync();
        }

        public Task<int> GetTotalCountOfContinents()
        {
            return _dbContext.Continents.CountAsync();
        }

    }
}
