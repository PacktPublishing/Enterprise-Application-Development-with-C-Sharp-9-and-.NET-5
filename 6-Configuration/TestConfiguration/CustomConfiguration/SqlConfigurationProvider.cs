using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace TestConfiguration.CustomConfiguration
{
    public class SqlConfigurationProvider : ConfigurationProvider
    {
        public SqlConfigurationSource Source { get; }

        public SqlConfigurationProvider(SqlConfigurationSource source)
        {
            Source = source;
        }

        public override void Load()
        {
            // create a connection object  
            SqlConnection sqlConnection = new SqlConnection(Source.ConnectionString);

            // Create a command object  
            SqlCommand sqlCommand = new SqlCommand(Source.Query, sqlConnection);
            sqlConnection.Open();

            // Call ExecuteReader to return a DataReader  
            SqlDataReader salDataReader = sqlCommand.ExecuteReader();

            while (salDataReader.Read())
            {
                Data.Add(salDataReader.GetString(0), salDataReader.GetString(1));
            }
            salDataReader.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();
        }
    }
}
