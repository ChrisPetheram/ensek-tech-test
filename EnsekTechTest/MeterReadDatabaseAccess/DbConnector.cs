using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadDatabaseAccess
{
    public class DbConnector : IDbConnector
    {

        public string ConnectionString { get; init; }

        public DbConnector(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);

            connection.Open();
            return connection;
        }
    }
}
