// "//-----------------------------------------------------------------------".
// <copyright file="IDistributedCacheService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Caching.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for Cache Service.
    /// </summary>
    public interface IDistributedCacheService
    {
        /// <summary>
        /// Add or update cache entry of Type T asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of return parameter.</typeparam>
        /// <param name="cacheEntityKey">Key.</param>
        /// <param name="cacheEntity">Cache entity.</param>
        /// <param name="absoluteExpiration">Expiration.</param>
        /// <param name="cancellationToken">Token.</param>
        /// <returns>A <see cref="Task"/> Task.</returns>
        Task AddOrUpdateCacheAsync<T>(string cacheEntityKey, T cacheEntity, TimeSpan? absoluteExpiration = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add or update cache entry of string type asynchronously.
        /// </summary>
        /// <param name="cacheEntityKey">The key.</param>
        /// <param name="cacheEntity">The value.</param>
        /// <param name="absoluteExpiration">The expiration.</param>
        /// <param name="cancellationToken">The token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddOrUpdateCacheStringAsync(string cacheEntityKey, string cacheEntity, TimeSpan? absoluteExpiration = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets cache entry of Type T asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of return parameter.</typeparam>
        /// <param name="cacheEntityKey">The key.</param>
        /// <param name="cancellationToken">The token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> GetCacheAsync<T>(string cacheEntityKey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets cache entry of string type asynchronously.
        /// </summary>
        /// <param name="cacheEntityKey">The key.</param>
        /// <param name="cancellationToken">The token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<string> GetCacheStringAsync(string cacheEntityKey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Refresh cache entry asynchronously.
        /// </summary>
        /// <param name="cacheEntityKey">The key.</param>
        /// <param name="cancellationToken">The token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task RefreshCacheAsync(string cacheEntityKey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes cache entry asynchronously.
        /// </summary>
        /// <param name="cacheEntityKey">The key.</param>
        /// <param name="cancellationToken">The token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task RemoveCacheAsync(string cacheEntityKey, CancellationToken cancellationToken = default);
    }
}
