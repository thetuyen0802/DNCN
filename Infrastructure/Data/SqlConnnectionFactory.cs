using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SqlConnnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public  IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
