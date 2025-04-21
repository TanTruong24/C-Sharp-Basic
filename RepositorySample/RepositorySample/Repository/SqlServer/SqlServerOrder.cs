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
        private readonly IProductRepository _productRepository;

        private readonly string _connectionString;

        Dictionary<string, string> Commands = new()
        {
            { "filter", "SELECT * FROM orders"},
        };

        public SqlServerOrder (IConfiguration config, IProductRepository productRepository)
        {
            _connectionString = config.GetConnectionString("ShopDB");
            _productRepository = productRepository;
        }

        public IEnumerable<Order> Filter()
        {
            var orders = new List<Order>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(Commands["filter"], conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(new Order
                {
                    Id = (int) reader["id"],
                    CustomerId = (int)reader["customer_id"],
                    OrderReference = (string)reader["order_reference"]
                });
            }
            return orders;
        }

        public void Create(Order order)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                var insertOrderCmd = new SqlCommand(@"
                INSERT INTO orders (customer_id, order_reference)
                VALUES (@CustomerId, @OrderReference);
                SELECT CAST(SCOPE_IDENTITY() as int);",
                conn, transaction);

                insertOrderCmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                insertOrderCmd.Parameters.AddWithValue("@OrderReference", order.OrderReference);

                // Lấy ID mới từ DB
                order.Id = (int)insertOrderCmd.ExecuteScalar();

                AddItems(order.Items, order.Id, conn, transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void AddItems(List<OrderItem> items, int orderId, SqlConnection conn, SqlTransaction transaction)
        {
            foreach (var item in items)
            {

                var unitPrice = _productRepository.GetPriceById(item.ProductId, conn, transaction);
                var totalPrice = unitPrice * item.Quantity;


                var cmd = new SqlCommand(@"
                INSERT INTO orders_items (order_id, product_id, quantity, price)
                VALUES (@OrderId, @ProductId, @Quantity, @Price)", conn, transaction);

                cmd.Parameters.AddWithValue("@OrderId", orderId);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@Price", totalPrice);
                cmd.ExecuteNonQuery();

                _productRepository.ReduceQuantity(item.ProductId, item.Quantity, conn, transaction);
            }
        }
    }
}
