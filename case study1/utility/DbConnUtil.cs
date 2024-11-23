using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace case_study1.utility
{
    internal class DbConnUtil
    {
        private static IConfiguration _configuration;

        static DbConnUtil()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();
        }

        public static string GetConnectionString()
        {
            return _configuration.GetConnectionString("LocalConnectionString");
        }

        public static SqlConnection GetConnection()
        {
            string connectionString = GetConnectionString();
            return new SqlConnection(connectionString);
        }
    }
}
