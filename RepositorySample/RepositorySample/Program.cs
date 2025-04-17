using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RepositorySample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);

            IConfiguration configuration = builder.Build();

            using var conn = new SqlConnection(configuration.GetConnectionString("ShopDB"));
        }
    }
}
