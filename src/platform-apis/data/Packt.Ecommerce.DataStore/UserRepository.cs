namespace Packt.Ecommerce.DataStore
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Options;
    using Packt.Ecommerce.DataStore.Contracts;

    /// <summary>
    /// The User repository.
    /// </summary>
    public class UserRepository : BaseRepository<Data.Models.User>, IUserRepository
    {
        /// <summary>
        /// The database settings.
        /// </summary>
        private readonly IOptions<DatabaseSettingsOptions> databaseSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="cosmosClient">The cosmos client.</param>
        /// <param name="databaseSettingsOption">The database settings option.</param>
        public UserRepository(CosmosClient cosmosClient, IOptions<DatabaseSettingsOptions> databaseSettingsOption)
            : base(cosmosClient, databaseSettingsOption?.Value.DataBaseName, "Users")
        {
            this.databaseSettings = databaseSettingsOption;
        }
    }
}
