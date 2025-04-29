using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Threading.Tasks;

namespace OrderProcessorLogging
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //Sử dụng Serilog để log ra file
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Debug()                         //Log từ Debug trở lên
                .WriteTo.File("../../../logs/myapp.log", rollingInterval: RollingInterval.Day)  // Ghi ra file, mỗi ngày 1 file
                .CreateLogger();

            var host = Host.CreateDefaultBuilder(args)
                .UseSerilog(logger)    // Sử dụng Serilog thay cho mặc định
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<OrderProcessor>();
                })
                .Build();

            // Sử dụng log trong .net

//            using var host = Host.CreateDefaultBuilder(args)
//                .ConfigureLogging(logging =>
//                {
//                    logging.ClearProviders();
//                    logging.AddConsole();

//                    // Set log level thấp nhất chương trình muốn trong env debug
//#if DEBUG
//                    logging.SetMinimumLevel(LogLevel.Debug);
//#endif
//                })
//                .ConfigureServices((context, services) =>
//                {
//                    // DI
//                    services.AddTransient<OrderProcessor>();
//                })
//                .Build();


            // // Resolve service & run logic
            var processor = host.Services.GetRequiredService<OrderProcessor>();
            await processor.ProcessOrdersAsync();
        }
    }
}
