using Microsoft.Extensions.Configuration;


namespace TestConfiguration.CustomConfiguration
{
    public class SqlConfigurationSource : IConfigurationSource
    {
        public string ConnectionString { get; set; }
        public string Query { get; set; }

        public SqlConfigurationSource(string connectionString, string query)
        {
            ConnectionString = connectionString;
            Query = query;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SqlConfigurationProvider(this);
        }
    }  
   
}
