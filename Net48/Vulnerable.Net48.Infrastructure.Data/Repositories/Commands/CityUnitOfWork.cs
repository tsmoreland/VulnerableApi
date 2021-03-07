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
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Domain.Contracts.Commands;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Net48.Infrastructure.Data.Repositories.Commands
{
    public sealed class CityUnitOfWork : UnitOfWorkBase<City>, ICityUnitOfWork
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="CityUnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">database context used to interact with the database</param>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="dbContext"/> is null
        /// </exception>
        public CityUnitOfWork(AddressDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Task<City> Add(City model, CancellationToken cancellationToken)
        {
            return Task.FromResult(DbContext.Cities.Add(model));
        }

        public override Task Delete(int id, CancellationToken cancellationToken)
        {
            // TODO: add stored procedure to handle the delete

            throw new NotImplementedException();
        }

        public override Task Update(City model, CancellationToken cancellationToken)
        {
            DbContext.Cities.Attach(model);
            DbContext.Entry(model).State = EntityState.Modified;
            return Task.CompletedTask;
        }

    }
}
