using Microsoft.Extensions.Configuration;
using RepositorySample.Params;
using RepositorySample.Entities;
using RepositorySample.Repository;
using RepositorySample.Repository.SqlServer;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Identity.Client.Extensions.Msal;

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

            var storage = GetStorageByConfig(repoType, configuration);

            CreateOrderExample(storage);
            FilterOrder(storage);

        }

        static IOrderStorage GetStorageByConfig(string? repoType, IConfiguration configuration)
        {
            IOrderStorage storage;
            IProductRepository productRepository = new ProductRepository();

            if (repoType == "SqlServer")
            {
                storage = new SqlServerOrder(configuration, productRepository);
                Console.WriteLine("Use SQL Server Storage.");
            }
            else if (repoType == "InMemory")
            {
                storage = new SqlServerOrder(configuration, productRepository);
                Console.WriteLine("Use In-Memory Storage.");
            }
            else
            {
                throw new Exception("Config 'RepositorySettings:Type' invalid.");
            }

            return storage;
        }

        static void CreateOrderExample(IOrderStorage storage) 
        {
            // B3: Dùng qua Repository pattern
            var repository = new OrderRepository(storage);

            var newOrder = new CreateOrderParams
            {
                CustomerId = 1,
                OrderReference = $"YAMAHA-{DateTime.Now.ToString("yyyyMMdd-HHmmss")}",
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = 2, Quantity = 5}
                }

            };

            repository.Create(newOrder);

            var allOrders = repository.Filter();
            foreach (var order in allOrders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Ref: {order.OrderReference}");
            }
        }

        static void FilterOrder(IOrderStorage storage)
        {
            Console.WriteLine("Input filter params: ");
            Console.WriteLine("orderBy: ");
            var orderBy = Console.ReadLine();

            Console.WriteLine("orderDirection: ");
            var orderDirection = Console.ReadLine();

            Console.WriteLine("Query order reference: ");
            var query = Console.ReadLine();
            
            var filterCriterias = new FilterOrderCriteria()
            {
                OrderBy = orderBy,
                OrderDirection = orderDirection,
                Query = query
            };
            var repository = new OrderRepository(storage);

            var allOrders = repository.Filter(filterCriterias);

            foreach (var order in allOrders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Ref: {order.OrderReference}");
            }
        }
    }
}
