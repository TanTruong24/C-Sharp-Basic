using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.Options
{
    /*
    * Usage: dbexport --conntStr=<connectionstring> --query=<select query> [--filename:filename] [--server:<SqlServer>] [-format:<csv|tsql>] [--compress] [-adt]
    */
    public class DatabaseExportOptionsBuilder
    {
        private readonly string[] args;

        private readonly IEnumerable<IDatabaseExportOptionsValidator> validators;

        public Func<DateTime> CurrentTimeFunc { get; set; } = () => DateTime.Now;

        public string FileDateTimeFormat { get; set; } = "yyyyddMM-HHmmss";

        public DatabaseExportOptionsBuilder(
            string[] args,
            IEnumerable<IDatabaseExportOptionsValidator> validators
            )
        {
            this.args = args;
            this.validators = validators ?? [];
        }

        public DatabaseExportOptions Build()
        {
            var options = ParseArguments(args);
            return Validate(options);
        }

        private DatabaseExportOptions ParseArguments(string[] args)
        {
            if (args == null || args.Length < 2)
                throw new ArgumentException("Missing required parameters");

            var options = new DatabaseExportOptions();

            foreach (var arg in args)
            {
                ParseArgument(arg, options);
            }

            NormalizeQueryFromTableNames(options);

            FinalizeFileName(options.ExportOptions);

            return options;
        }

        private void NormalizeQueryFromTableNames(DatabaseExportOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.DatabaseOptions.Query))
                return;

            var query = options.DatabaseOptions.Query.Trim();

            // Nếu query bắt đầu bằng "select", thì giữ nguyên
            if (query.StartsWith("select", StringComparison.OrdinalIgnoreCase))
                return;

            // Tách danh sách bảng
            var tableList = query.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                 .Select(t => t.Trim())
                                 .Where(t => !string.IsNullOrWhiteSpace(t))
                                 .ToList();

            if (tableList.Count == 0)
            {
                throw new ArgumentException("No valid table name found in --query.");
            }

            if (tableList.Count > 1)
            {
                throw new ArgumentException("Only one table name is allowed in --query. Please remove commas and try again.");
            }

            var table = tableList[0];

            options.DatabaseOptions.TableNames = table;
            options.DatabaseOptions.Query = $"SELECT * FROM {table}";
        }


        private void ParseArgument(string arg, DatabaseExportOptions options)
        {
            const string ConnStrPrefix = "--connStr=";
            const string QueryPrefix = "--query=";
            const string FileNamePrefix = "--filename=";
            const string ServerPrefix = "--server=";
            const string FormatPrefix = "--format=";
            const string FilePathPrefix = "--filePath=";

            if (arg.StartsWith(ConnStrPrefix))
            {
                options.DatabaseOptions.ConnectionString = arg[ConnStrPrefix.Length..];
            }
            else if (arg.StartsWith(QueryPrefix))
            {
                options.DatabaseOptions.Query = arg[QueryPrefix.Length..];
            }
            else if (arg.StartsWith(FileNamePrefix))
            {
                options.ExportOptions.FileName = arg[FileNamePrefix.Length..];
            }
            else if (arg.StartsWith(ServerPrefix))
            {
                var serverName = arg[ServerPrefix.Length..];
                if (Enum.TryParse<ServerTypes>(serverName, true, out var serverType))
                {
                    options.DatabaseOptions.ServerType = serverType;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Unknown server type: {serverName}");
                }
            }
            else if (arg.StartsWith(FormatPrefix))
            {
                var formatName = arg[FormatPrefix.Length..];
                if (Enum.TryParse<ExportFormats>(formatName, true, out var format))
                {
                    options.ExportOptions.ExportFormats = format;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Unknown export format: {formatName}");
                }
            }
            else if (arg.StartsWith(FilePathPrefix))
            {
                options.ExportOptions.FilePath = arg[FilePathPrefix.Length..];
            }
            else if ("--compress".Equals(arg, StringComparison.OrdinalIgnoreCase))
            {
                options.ExportOptions.ZipCompressed = true;
            }
            else if ("--adt".Equals(arg, StringComparison.OrdinalIgnoreCase))
            {
                options.ExportOptions.AppendExportTimeToFileName = true;
            }
            else
            {
                throw new ArgumentException($"Unknown option: {arg}");
            }
        }

        private void FinalizeFileName(ExportOptions exportOptions)
        {
            if (string.IsNullOrWhiteSpace(exportOptions.FileName))
            {
                exportOptions.FileName = "export";
            }

            // Append timestamp nếu được yêu cầu
            if (exportOptions.AppendExportTimeToFileName)
            {
                exportOptions.FileName += $"-{CurrentTimeFunc().ToString(FileDateTimeFormat)}";
            }

            // Thêm phần mở rộng file (.csv, .sql)
            exportOptions.FileName += GetFileExtension(exportOptions.ExportFormats);

            // Nếu cần nén, thêm .zip
            if (exportOptions.ZipCompressed)
            {
                exportOptions.FileName += ".zip";
            }

            // Nếu có file path → ghép path và đảm bảo thư mục tồn tại
            if (!string.IsNullOrWhiteSpace(exportOptions.FilePath))
            {
                // Tạo thư mục nếu chưa tồn tại
                Directory.CreateDirectory(exportOptions.FilePath);

                if (!Path.IsPathRooted(exportOptions.FileName))
                {
                    exportOptions.FileName = Path.Combine(exportOptions.FilePath, exportOptions.FileName);
                }
            }
        }


        private static string GetFileExtension(ExportFormats format)
        {
            return format switch
            {
                ExportFormats.Csv => ".csv",
                ExportFormats.Sql => ".sql",
                _ => throw new NotSupportedException($"Unsupported export format: {format}")
            };
        }

        private DatabaseExportOptions Validate(DatabaseExportOptions options)
        {
            foreach (var validator in validators)
            {
                validator.Validate(options);
            }

            return options;
        }
    }
}
