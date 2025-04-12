using DBExporter.DatabaseObjects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.DatabaseBuilder.ConcreteServer
{
    public class SqlServer : IExportSourceBuilder
    {
        private readonly string _connectionString;

        public SqlServer(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));     
        }
        public ExportSource Build(string query, string tableNames)
        {
            try
            {
                DbConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();

                return new ExportSource(connection, reader)
                {
                    TableNames = tableNames
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to build ExportSource for SqlServer", ex);
            }
        }
    }
}
