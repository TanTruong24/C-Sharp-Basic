using DatabaseConnection;
using System.Data.Common;
using DBExporterOptions;
using Microsoft.Data.SqlClient;
using ExportEngine;


namespace DBExporter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var options = FindOptions(args);

            if (options == null) return;

            using var conn = DatabaseConnection(options);

            conn.Open();

            //Display(conn, options);

            var data = DataUtils.QueryToList(conn, options.FinalQuery);

            var service = new ExportService();

            var path = service.Export(data, options.Format, options.OutputFile, options.Zip);

            Console.WriteLine($"Exported to: {path}");


        }

        private static void Display(DbConnection conn, ExportOptions options)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = options.QueryOrTable ?? "";

            using var reader = cmd.ExecuteReader();

            var colCount = reader.FieldCount;
            while (reader.Read())
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"{reader.GetName(i)}: {reader[i]}  ");
                }
                Console.WriteLine();
            }
        }

        public static ExportOptions? FindOptions(string[] args)
        {
            try
            {
                var factory = new ExporterFactory();
                var parser = new ExportOptionsParser();
                var options = parser.Parse(args);
                options.Validate(factory);

                return options;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        static DbConnection DatabaseConnection(ExportOptions options)
        {
            var factoryProvider = new DbConnectionFactoryProvider();

            var builder = new DatabaseConnectionBuilder(factoryProvider);

            DbConnection connection = builder.SetProvider("SqlServer").SetConnectionString(options.ConnectionString).Build();

            return connection;
        }
    }
}
