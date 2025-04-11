using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;

using DBConnection;
using DBExportOptions;
using DBExportEngine;

namespace DBExporter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }

            Console.WriteLine("Parsing export options...");

            var options = FindOptions(args);
            if (options == null) return;

            try
            {
                using var connection = CreateDatabaseConnection(options);
                connection.Open();
                Console.WriteLine("Database connection established.");

                var data = DataUtils.QueryToList(connection, options.FinalQuery);
                Console.WriteLine($"Retrieved {data.Count} rows.");

                var exportService = new ExportService();
                var path = exportService.Export(data, options.Format, options.OutputFile, options.GetTableName());

                if (string.IsNullOrWhiteSpace(path))
                {
                    Console.WriteLine("[ERROR] Export failed or returned an empty path.");
                }
                else
                {
                    Console.WriteLine($"Export completed successfully. File saved at: {path}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.GetType().Name}: {ex.Message}");
            }
        }

        private static ExportOptions? FindOptions(string[] args)
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
                Console.WriteLine($"[ERROR] Failed to parse options: {ex.GetType().Name}: {ex.Message}");
                return null;
            }
        }

        private static DbConnection CreateDatabaseConnection(ExportOptions options)
        {
            var factoryProvider = new DbConnectionFactoryProvider();
            var builder = new DatabaseConnectionBuilder(factoryProvider);

            return builder
                .SetProvider("SqlServer")
                .SetConnectionString(options.ConnectionString)
                .Build();
        }

        private static void ShowUsage()
        {
            Console.WriteLine(@"
                Usage:
                  exporter --connStr=""<connection_string>"" --query=""<SELECT statement or table name>"" [--outputFile=""<filename>""] [--format=""csv|sql""] [--zip true|false] [--addTimestamp=true|false]

                Required:
                  --connStr       Database connection string.
                  --query         SELECT statement or table name to export.

                Optional:
                  --outputFile    Output filename (default: table name or 'export').
                  --format        Export format: 'csv' (default) or 'sql'.
                  --zip           Compress output into .zip (default: false).
                  --addTimestamp     Append current date/time to filename (default: false).

                Example:
                  exporter --connStr=""Server=.;Database=MyDB;Trusted_Connection=True;"" --query=""employeess"" --format=csv --zip=true --addTimestamp=true
                ");
        }

    }
}
