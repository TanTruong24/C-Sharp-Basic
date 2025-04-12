using System.IO.Compression;
using DBExporter.DatabaseBuilder;
using DBExporter.DatabaseObjects;
using DBExporter.Options;
using DBExporter.DatabaseWriter.CsvDataWriter;
using DBExporter.DatabaseWriter.SqlDataWriter;
using DBExporter.DatabaseWriter;

namespace DBExporter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseExportOptions? options = ParseOptions(args);
            if (options == null) return;

            var databaseBuilder = DatabaseBuilderFactory.Connect(options.DatabaseOptions.ServerType, options.DatabaseOptions.ConnectionString);
            if (databaseBuilder == null)
            {
                Console.WriteLine("Unsupported server type.");
                return;
            }

            try
            {
                using var database = databaseBuilder.Build(
                    options.DatabaseOptions.Query,
                    options.DatabaseOptions.TableNames
                );

                if (options.ExportOptions.ZipCompressed)
                {
                    ExportToZipFile(database, options.ExportOptions);
                }
                else
                {
                    using var stream = File.Create(options.ExportOptions.FileName);
                    Export(database, stream, options.ExportOptions);
                }

                Console.WriteLine($"Export completed: {options.ExportOptions.FileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Export failed: {ex.Message}");
            }
        }

        private static DatabaseExportOptions? ParseOptions(string[] args)
        {
            try
            {
                var optionBuilder = new DatabaseExportOptionsBuilder(args, [DatabaseExportOptionsValidator.Instance]);
                return optionBuilder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid options: {ex.Message}");
                return null;
            }
        }

        private static void ExportToZipFile(ExportSource database, ExportOptions exportOptions)
        {
            using var zipFile = File.Create(exportOptions.FileName);
            using var archive = new ZipArchive(zipFile, ZipArchiveMode.Create);

            var entryName = GetInnerEntryName(exportOptions.FileName, exportOptions.ExportFormats);

            var zipEntry = archive.CreateEntry(entryName);
            using var stream = zipEntry.Open();

            Export(database, stream, exportOptions);
        }

        private static string GetInnerEntryName(string fileName, ExportFormats format)
        {
            var baseName = Path.GetFileNameWithoutExtension(fileName); // remove .zip
            var ext = format switch
            {
                ExportFormats.Csv => ".csv",
                ExportFormats.Sql => ".sql",
                _ => ".txt"
            };

            return baseName + ext;
        }

        private static void Export(ExportSource database, Stream stream, ExportOptions exportOptions)
        {
            IDataWriterFactory writerFactory = exportOptions.ExportFormats switch
            {
                ExportFormats.Csv => new CsvDatabaseWriterFactory(),
                ExportFormats.Sql => new SqlDatabaseWriterFactory(),
                _ => throw new Exception($"Export format {exportOptions.ExportFormats} not supported"),
            };

            var dataWriter = writerFactory.GetDataWriter();
            dataWriter.WriteData(database, stream);
        }
    }
}
