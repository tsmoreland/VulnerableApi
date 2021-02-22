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

using System.Threading.Tasks;
using Vulnerable.Application.Contracts.Data;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Net48.Infrastructure.Data.Repositories
{
    public sealed class ProvinceRepository : IProvinceRepository
    {
        private readonly AddressDbContext _dbContext;

        public ProvinceRepository(AddressDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public Task<Province?> GetProvinceById(int id) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<Province?> GetProvinceByName(string name) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<string[]> GetProvinceNames(int pageSize, int pageNumber) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<string[]> GetProvinceNamesLikeName(string name, int pageSize, int pageNumber) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<Province[]> GetProvincesByCountryId(int countryId, int pageNumber, int pageSize) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<Province[]> GetProvincesByCountryName(string countryName, int pageNumber, int pageSize) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvinceNamesLikeName(string name) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvinces() => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvincesByCountryId(int countryId) => throw new System.NotImplementedException();

        /// <inheritdoc/>
        public Task<int> GetTotalCountOfProvincesByCountryName(string countryName) => throw new System.NotImplementedException();
    }
}
