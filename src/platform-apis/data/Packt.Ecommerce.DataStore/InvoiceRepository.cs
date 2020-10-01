// "//-----------------------------------------------------------------------".
// <copyright file="InvoiceRepository.cs" company="Packt">
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
    /// The Invoice repository.
    /// </summary>
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        /// <summary>
        /// The database settings.
        /// </summary>
        private readonly IOptions<DatabaseSettingsOptions> databaseSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
        /// </summary>
        /// <param name="cosmosClient">The cosmos client.</param>
        /// <param name="databaseSettingsOption">The database settings option.</param>
        public InvoiceRepository(CosmosClient cosmosClient, IOptions<DatabaseSettingsOptions> databaseSettingsOption)
            : base(cosmosClient, databaseSettingsOption?.Value.DataBaseName, "Invoices")
        {
            this.databaseSettings = databaseSettingsOption;
        }
    }
}
