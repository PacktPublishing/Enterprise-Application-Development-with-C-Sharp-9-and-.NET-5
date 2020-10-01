// "//-----------------------------------------------------------------------".
// <copyright file="OrderRepository.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DataStore
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Options;
    using Packt.Ecommerce.Data.Models;
    using Packt.Ecommerce.DataStore.Contracts;

    /// <summary>
    /// The order repository.
    /// </summary>
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        /// <summary>
        /// The database settings.
        /// </summary>
        private readonly IOptions<DatabaseSettingsOptions> databaseSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="cosmosClient">The cosmos client.</param>
        /// <param name="databaseSettingsOption">The database settings option.</param>
        public OrderRepository(CosmosClient cosmosClient, IOptions<DatabaseSettingsOptions> databaseSettingsOption)
            : base(cosmosClient, databaseSettingsOption?.Value.DataBaseName, "Orders")
        {
            this.databaseSettings = databaseSettingsOption;
        }
    }
}
