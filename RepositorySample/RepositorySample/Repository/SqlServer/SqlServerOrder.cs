using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositorySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Repository.SqlServer
{
    public class SqlServerOrder : IOrderStorage
    {
        private readonly string _connectionString;

        Dictionary<string, string> Commands = new()
        {
            { "GetAll", "SELECT * FROM orders"},
            { "GetById", "SELECT * FROM orders WHERE id={orderId}" },
            { "Delete", "DELETE FROM orders WHERE id={orderId}"}
        };

        public SqlServerOrder (IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SqlServer");
        }

        public void Add(Order order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> Filter()
        {
            var orders = new List<Order>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(Commands["GetAll"], conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(new Order
                {
                    Id = (int) reader["id"],
                    CustomerId = (int)reader["cutomer_id"],
                    OrderReference = (string)reader["order_reference"]
                });
            }
            return orders;
        }
    }
}
