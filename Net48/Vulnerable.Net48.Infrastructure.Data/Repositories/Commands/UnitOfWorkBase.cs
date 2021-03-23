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
using System.Data;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Domain.Contracts.Commands;
using Vulnerable.Domain.Entities;
using Vulnerable.Shared;

namespace Vulnerable.Net48.Infrastructure.Data.Repositories.Commands
{
    public abstract class UnitOfWorkBase<TEntity> : IUnitOfWorkBase<TEntity>
        where TEntity : Entity
    {
        private readonly DbContextTransaction _transaction;

        public UnitOfWorkBase(AddressDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _transaction = DbContext.Database.BeginTransaction(IsolationLevel.RepeatableRead);
        }

        /// <inheritdoc/>
        public Task<City?> GetCityById(int id, CancellationToken cancellationToken) =>
            DbContext.Cities.FindAsync(cancellationToken, id);

        /// <inheritdoc/>
        public Task<Province?> GetProvinceById(int id, CancellationToken cancellationToken) =>
            DbContext.Provinces.FindAsync(cancellationToken, id);

        /// <inheritdoc/>
        public Task<Country?> GetCountryById(int id, CancellationToken cancellationToken) =>
            DbContext.Countries.FindAsync(cancellationToken, id);

        /// <inheritdoc/>
        public Task<Continent?> GetContinentById(int id, CancellationToken cancellationToken) =>
            DbContext.Continents.FindAsync(cancellationToken, id);

        /// <inheritdoc/>
        public Task Commit(CancellationToken cancellationToken) =>
            DbContext
                .SaveChangesAsync(cancellationToken)
                .ContinueWith(response =>
                {
                    GuardAgainst.FaultedOrCancelled(response);
                    var result = response.Result;
                    _transaction.Commit();

                    if (result == 0)
                    {
                    }
                }, cancellationToken);

        /// <inheritdoc/>
        public abstract Task<TEntity> Add(TEntity model, CancellationToken cancellationToken);

        /// <inheritdoc/>
        public abstract Task Update(TEntity model, CancellationToken cancellationToken);

        /// <inheritdoc/>
        public abstract Task Delete(int id, CancellationToken cancellationToken);

        protected AddressDbContext DbContext { get; }

        #region IDisposable

        private bool _disposed;

        ~UnitOfWorkBase() =>
            Dispose(false);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
                return;

            DbContext.Dispose();
            _disposed = true;
        }

        #endregion

    }
}
