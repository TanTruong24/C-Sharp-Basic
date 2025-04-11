using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    public class DatabaseConnectionBuilder : IDatabaseConnectionBuilder
    {
        private string _connectionString;
        private string _provider;
        private readonly DbConnectionFactoryProvider _factoryProvider;

        public DatabaseConnectionBuilder(DbConnectionFactoryProvider factoryProvider)
        {
            _factoryProvider = factoryProvider;
        }

        public DbConnection Build()
        {
            var factory = _factoryProvider.GetFactory(_provider);
            return factory.CreateConnection(_connectionString);
        }

        public IDatabaseConnectionBuilder SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public IDatabaseConnectionBuilder SetProvider(string provider)
        {
            _provider = provider;
            return this;
        }
    }
}
