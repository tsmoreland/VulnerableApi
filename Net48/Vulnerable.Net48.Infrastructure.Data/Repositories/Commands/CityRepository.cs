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
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Domain.Contracts.Commands;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Net48.Infrastructure.Data.Repositories.Commands
{
    public sealed class CityRepository : ICityRepository
    {
        private readonly AddressDbContext _dbContext;

        /// <summary>
        /// Instantiates a new instance of the <see cref="CityRepository"/> class.
        /// </summary>
        /// <param name="dbContext">database context used to interact with the database</param>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="dbContext"/> is null
        /// </exception>
        public CityRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public Task<City?> GetCityById(int id, CancellationToken cancellationToken) =>
            _dbContext.Cities.FindAsync(cancellationToken, id);

        /// <inheritdoc/>
        public Task<Province?> GetProvinceById(int id, CancellationToken cancellationToken) =>
            _dbContext.Provinces.FindAsync(cancellationToken, id);

        /// <inheritdoc/>
        public Task<Country?> GetCountryById(int id, CancellationToken cancellationToken) =>
            _dbContext.Countries.FindAsync(cancellationToken, id);

        /// <inheritdoc/>
        public Task<Continent?> GetContinentById(int id, CancellationToken cancellationToken) =>
            _dbContext.Continents.FindAsync(cancellationToken, id);

        /// <inheritdoc/>
        public Task<int> Add(City model, CancellationToken cancellationToken) 
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Task Commit(CancellationToken cancellationToken) 
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<City> Delete(int id, CancellationToken cancellationToken) 
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Task Update(City model, CancellationToken cancellationToken) 
        {
            throw new System.NotImplementedException();
        }

        #region IDisposable

        private bool _disposed;

        ~CityRepository() => 
            Dispose(false);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _dbContext.Dispose();
            _disposed = true;
        }

        #endregion
    }
}
