// "//-----------------------------------------------------------------------".
// <copyright file="BaseRepository.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.DataStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Packt.Ecommerce.DataStore.Contracts;

    /// <summary>
    /// The generic repository.
    /// </summary>
    /// <typeparam name="TEntity">Generic entity if type class.</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
where TEntity : class
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly Container container;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="cosmosClient">cosmos client.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="containerName">Name of the container.</param>
        public BaseRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            this.container = cosmosClient?.GetContainer(databaseName, containerName);
        }

        /// <inheritdoc/>
        public async Task<ItemResponse<TEntity>> AddAsync(TEntity entity, string partitionKey)
        {
            ItemResponse<TEntity> response = await this.container.CreateItemAsync<TEntity>(entity, new PartitionKey(partitionKey)).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAsync(string filterCriteria)
        {
            if (string.IsNullOrWhiteSpace(filterCriteria))
            {
                filterCriteria = "select * from e";
            }
            else
            {
                filterCriteria = $"select * from e where {filterCriteria}";
            }

            var iterator = this.container.GetItemQueryIterator<TEntity>(new QueryDefinition(filterCriteria));
            List<TEntity> results = new List<TEntity>();
            while (iterator.HasMoreResults)
            {
                var result = await iterator.ReadNextAsync().ConfigureAwait(false);

                results.AddRange(result.ToList());
            }

            return results;
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<TEntity> response = await this.container.ReadItemAsync<TEntity>(id, new PartitionKey(partitionKey)).ConfigureAwait(false);
                return response;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ModifyAsync(TEntity entity, string etag, string partitionKey)
        {
            try
            {
                ItemResponse<TEntity> response = await this.container.UpsertItemAsync<TEntity>(entity, new PartitionKey(partitionKey), new ItemRequestOptions { IfMatchEtag = etag }).ConfigureAwait(false);
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound || ex.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<TEntity> response = await this.container.DeleteItemAsync<TEntity>(id, new PartitionKey(partitionKey)).ConfigureAwait(false);
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }
    }
}
