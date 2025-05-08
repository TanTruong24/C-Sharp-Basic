using Microsoft.Extensions.Configuration;

namespace ConfigurationDemo
{
    internal class Program
    {
        /**
         * 🔗 Các nguồn cấu hình kết hợp vào IConfiguration

            * appsettings.{Environment}.json

                - File cấu hình theo môi trường: appsettings.Development.json, appsettings.Production.json, v.v.

                - Chứa các key-value như connection string, setting cho dịch vụ.

                - Được load mặc định khi khởi tạo WebApplicationBuilder.

            * Environment Variables

                - Biến môi trường hệ thống (OS / Docker / Azure App Services...).

                - Cho phép ghi đè file JSON mà không sửa mã nguồn.

                - Ví dụ: ASPNETCORE_ENVIRONMENT=Production.

            * Command-line Arguments

                - Được truyền khi chạy ứng dụng qua dotnet run hoặc file thực thi .exe.

                * Ví dụ: dotnet run --AppName=TestApp.

            * Other sources

                - Bao gồm: XML, INI, Key-per-File, User Secrets, Azure Key Vault, App Configuration, In-Memory, Custom Provider...

                - Cho phép mở rộng và tích hợp với hệ thống bên ngoài.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            /**
             * Thứ tự ưu tiên thực tế chính là do thứ tự thêm các source vào ConfigurationBuilder
             * => nguồn nào được thêm sau cùng sẽ có quyền ghi đè cao hơn.
             * 
             * => JSON → ENV → CLI (CLI luôn nên cuối cùng để override)
             * 
             * .AddJsonFile(...).AddEnvironmentVariables().AddCommandLine(args), thì CommandLine là ưu tiên cao nhất vì được thêm cuối cùng.
             * 
             */
            var configurations = new ConfigurationBuilder()
                    .AddJsonFile(@"E:\eng\csharp\ConfigurationDemo\ConfigurationDemo\ConfigFiles\mysettings.json", optional: false) // bắt buộc
                    .AddJsonFile(@"E:\eng\csharp\ConfigurationDemo\ConfigurationDemo\ConfigFiles\mysettings.optional.json", optional: true) // tùy chọn
                    .AddXmlFile(@"E:\eng\csharp\ConfigurationDemo\ConfigurationDemo\ConfigFiles\mysettings.xml") // đọc file XML
                    .AddKeyPerFile(@"E:\eng\csharp\ConfigurationDemo\ConfigurationDemo\ConfigFiles\KeyPerFile\KeyPerFileConfigSample__StringValue", optional: true) // mỗi file = 1 key
                    .AddEnvironmentVariables() // load biến môi trường hệ thống
                    .AddCommandLine(args) // ghi đè cấu hình từ args dòng lệnh
                    .Build();

            PrintConfiguredProviders(configurations);
            Console.WriteLine();

            PrintConfigValues(configurations);
            Console.WriteLine();

            var config = configurations.GetRequiredSection("JsonConfigSample").Get<BindingConfig>();
            Print(config);
        }

        private static void Print(BindingConfig? config)
        {
            if (config == null)
            {
                Console.WriteLine("config == null");
            }
            else
            {
                Console.WriteLine($"StringConfig: {config.StringConfig}");
                Console.WriteLine($"IntegerConfig: {config.IntegerConfig}");
                Console.WriteLine($"BoolConfig: {config.BoolConfig}");
            }
        }

        private static void PrintConfiguredProviders(IConfigurationRoot configuration)
        {
            foreach (var p in configuration.Providers)
            {
                Console.WriteLine($"Provider: {p.GetType().Name}");
            }
        }

        private static void PrintConfigValues(IConfigurationRoot configuration)
        {
            foreach (var config in configuration.AsEnumerable())
            {
                Console.WriteLine($"{config.Key} = {config.Value}");
            }
        }
    }
}
