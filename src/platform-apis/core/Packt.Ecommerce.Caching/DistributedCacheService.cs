// "//-----------------------------------------------------------------------".
// <copyright file="DistributedCacheService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.Caching
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using Packt.Ecommerce.Caching.Interfaces;

    /// <inheritdoc/>
    public class DistributedCacheService : IDistributedCacheService
    {
        /// <summary>
        /// The default cache expiration minutes.
        /// </summary>
        private const long DefaultCacheAbsoluteExpirationMinutes = 60;

        /// <summary>
        /// The entity serializer.
        /// </summary>
        private readonly IEntitySerializer entitySerializer;

        /// <summary>
        /// The distributed cache.
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// The distributed cache entry options.
        /// </summary>
        private readonly DistributedCacheEntryOptions distributedCacheEntryOptions;

        /// <summary>
        /// The default cache entry expriation time.
        /// </summary>
        private readonly TimeSpan defaultCacheEntryAbsoluteExpriationTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedCacheService"/> class.
        /// </summary>
        /// <param name="distributedCache">The distributed cache.</param>
        /// <param name="entitySerializer">The entity serializer.</param>
        public DistributedCacheService(IDistributedCache distributedCache, IEntitySerializer entitySerializer)
        {
            this.distributedCache = distributedCache;
            this.entitySerializer = entitySerializer;
            this.defaultCacheEntryAbsoluteExpriationTime = TimeSpan.FromMinutes(DefaultCacheAbsoluteExpirationMinutes);
            this.distributedCacheEntryOptions = new DistributedCacheEntryOptions();
        }

        /// <inheritdoc/>
        public async Task AddOrUpdateCacheAsync<T>(string cacheEntityKey, T cacheEntity, TimeSpan? absoluteExpiration = default, CancellationToken cancellationToken = default)
        {
            var absoluteExpiryTime = absoluteExpiration == null
                                 ? this.defaultCacheEntryAbsoluteExpriationTime
                                 : TimeSpan.FromSeconds(absoluteExpiration.Value.TotalSeconds);
            var byteValue = await this.entitySerializer.SerializeEntityAsync<T>(cacheEntity).ConfigureAwait(false);
            await this.distributedCache.SetAsync(cacheEntityKey, byteValue, this.distributedCacheEntryOptions.SetAbsoluteExpiration(absoluteExpiryTime), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task AddOrUpdateCacheStringAsync(string cacheEntityKey, string cacheEntity, TimeSpan? absoluteExpiration = default, CancellationToken cancellationToken = default)
        {
            var absoluteExpiryTime = absoluteExpiration == null
                                 ? this.defaultCacheEntryAbsoluteExpriationTime
                                 : TimeSpan.FromSeconds(absoluteExpiration.Value.TotalSeconds);
            await this.distributedCache.SetStringAsync(cacheEntityKey, cacheEntity, this.distributedCacheEntryOptions.SetAbsoluteExpiration(absoluteExpiryTime), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<T> GetCacheAsync<T>(string cacheEntityKey, CancellationToken cancellationToken = default)
        {
            var obj = await this.distributedCache.GetAsync(cacheEntityKey, cancellationToken).ConfigureAwait(false);
            return obj != null ? await this.entitySerializer.DeserializeEntityAsync<T>(obj).ConfigureAwait(false) : default;
        }

        /// <inheritdoc/>
        public async Task<string> GetCacheStringAsync(string cacheEntityKey, CancellationToken cancellationToken = default)
        {
            return await this.distributedCache.GetStringAsync(cacheEntityKey, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task RefreshCacheAsync(string cacheEntityKey, CancellationToken cancellationToken = default)
        {
            await this.distributedCache.RefreshAsync(cacheEntityKey, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task RemoveCacheAsync(string cacheEntityKey, CancellationToken cancellationToken = default)
        {
            await this.distributedCache.RemoveAsync(cacheEntityKey, cancellationToken).ConfigureAwait(false);
        }
    }
}
