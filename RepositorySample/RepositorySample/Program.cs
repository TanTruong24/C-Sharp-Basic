using Microsoft.Extensions.Configuration;
using RepositorySample.Params;
using RepositorySample.Entities;
using RepositorySample.Repository;
using RepositorySample.Repository.SqlServer;
using System;
using System.Collections.Generic;
using System.IO;

namespace RepositorySample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start...");

            // B1: Load cấu hình
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            // B2: Đọc type và khởi tạo storage phù hợp
            var repoType = configuration["RepositorySettings:Type"];
            IOrderStorage storage;

            if (repoType == "SqlServer")
            {
                storage = new SqlServerOrder(configuration);
                Console.WriteLine("Use SQL Server Storage.");
            }
            else if (repoType == "InMemory")
            {
                storage = new SqlServerOrder(configuration);
                Console.WriteLine("Use In-Memory Storage.");
            }
            else
            {
                throw new Exception("Config 'RepositorySettings:Type' invalid.");
            }

            // B3: Dùng qua Repository pattern
            var repository = new OrderRepository(storage);

            var newOrder = new CreateOrderParams
            {
                CustomerId = 1,
                OrderReference = "ORD-DEMO",
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = 301, Quantity = 1, Price = 10000 }
                }
            };

            repository.Create(newOrder);

            var allOrders = repository.Filter();
            foreach (var order in allOrders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Ref: {order.OrderReference}");
            }
        }
    }
}
