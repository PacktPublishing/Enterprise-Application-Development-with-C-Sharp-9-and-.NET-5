using Microsoft.Extensions.Configuration;

namespace TestConfiguration.CustomConfiguration
{
    public static class SqlConfigurationExtensions
    {
        public static IConfigurationBuilder AddSql(this IConfigurationBuilder configuration, string connectionString, string query)
        {
            configuration.Add(new SqlConfigurationSource(connectionString, query));
            return configuration;
        }
    }
}
