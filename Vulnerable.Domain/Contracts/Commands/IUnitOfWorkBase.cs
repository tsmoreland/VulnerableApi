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
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Domain.Contracts.Commands
{
#if NET48
    public interface IUnitOfWorkBase<TEntity> : IDisposable
        where TEntity : Entity
#else
    public interface IUnitOfWorkBase<TEntity> : IDisposable, IAsyncDisposable
        where TEntity : Entity
#endif
    {
        /// <summary>
        /// Returns City matching <paramref name="id"/> or null if not found
        /// </summary>
        /// <param name="id">id of City to get</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>City or null</returns>
        Task<City?> GetCityById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Returns Province matching <paramref name="id"/> or null if not found
        /// </summary>
        /// <param name="id">id of Province to get</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>Province or null</returns>
        Task<Province?> GetProvinceById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Returns Country matching <paramref name="id"/> or null if not found
        /// </summary>
        /// <param name="id">id of Country to get</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>Country or null</returns>
        Task<Country?> GetCountryById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Returns Continent matching <paramref name="id"/> or null if not found
        /// </summary>
        /// <param name="id">id of Continent to get</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>Continent or null</returns>
        Task<Continent?> GetContinentById(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new item to the repository
        /// </summary>
        /// <param name="model">item to add</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <returns>id of the newly added item</returns>
        /// <exception cref="ArgumentException">
        /// if item is not valid
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// if item or <paramref name="cancellationToken"/> are null
        /// </exception>
        Task<TEntity> Add(TEntity model, CancellationToken cancellationToken);

        /// <summary>
        /// associates the item with the repository, if already associated
        /// no action is taken 
        /// </summary>
        /// <param name="model">item to associate</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <exception cref="ArgumentException">
        /// if item is not valid
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// if item or <paramref name="cancellationToken"/> are null
        /// </exception>
        Task Update(TEntity model, CancellationToken cancellationToken);

        /// <summary>
        /// Marks an item for deletion, any dependent objects will also be deleted
        /// </summary>
        /// <param name="id">id of the item to delete</param>
        /// <param name="cancellationToken">used to cancel the operation</param>
        /// <exception cref="ArgumentException">
        /// if id does not match an existing object
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="cancellationToken"/> are null
        /// </exception>
        Task Delete(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Commits all pending changes
        /// </summary>
        /// <param name="cancellationToken">used to cancel the operation</param>
        Task Commit(CancellationToken cancellationToken);
    }
}
