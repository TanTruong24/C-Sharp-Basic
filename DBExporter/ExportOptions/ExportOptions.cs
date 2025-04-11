using System;
using System.Collections.Generic;
using System.IO;

namespace ExportOptions
{
    public class ExportOptions
    {
        public string ConnectionString { get; set; }
        public string QueryOrTable { get; set; }
        public string OutputFile { get; set; }
        public string Format { get; set; } = "csv";
        public bool Zip { get; set; } = false;
        public bool AddTimestamp { get; set; } = false;

        public string FinalQuery { get; private set; }

        public void Validate(ExporterFactory factory)
        {
            ValidateRequiredFields();
            NormalizeFormat(factory);
            BuildFinalQuery();
            ResolveOutputFilePath();
        }

        private void ValidateRequiredFields()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentException("Connection string is required (--connStr=...)");

            if (string.IsNullOrWhiteSpace(QueryOrTable))
                throw new ArgumentException("Query or Table is required (--query=...)");
        }

        private void NormalizeFormat(ExporterFactory factory)
        {
            Format = Format?.Trim().ToLower() ?? "csv";

            if (!factory.IsSupported(Format))
                throw new ArgumentException($"Unsupported format: {Format}");
        }

        private void BuildFinalQuery()
        {
            var trimmed = QueryOrTable.Trim();
            FinalQuery = trimmed.StartsWith("select", StringComparison.OrdinalIgnoreCase)
                ? trimmed
                : $"SELECT * FROM {trimmed}";
        }

        private void ResolveOutputFilePath()
        {
            var baseName = FinalQuery.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase) ? "export" : QueryOrTable.Trim();
            var extension = $".{Format}";

            if (string.IsNullOrWhiteSpace(OutputFile))
            {
                OutputFile = $"{baseName}{extension}";
            }
            else
            {
                var outputPath = OutputFile.Trim();
                bool isDirectory = string.IsNullOrEmpty(Path.GetExtension(outputPath));

                OutputFile = isDirectory
                    ? Path.Combine(outputPath, $"{baseName}{extension}")
                    : outputPath;
            }

            // chuẩn hóa đường dẫn tuyệt đối
            OutputFile = Path.GetFullPath(OutputFile);

            // append timestamp nếu cần
            if (AddTimestamp)
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var ext = Path.GetExtension(OutputFile);
                var name = Path.GetFileNameWithoutExtension(OutputFile);
                var dir = Path.GetDirectoryName(OutputFile) ?? "";
                OutputFile = Path.Combine(dir, $"{name}_{timestamp}{ext}");
            }

            // append zip nếu cần
            if (Zip && !OutputFile.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                OutputFile += ".zip";
            }
        }
    }
}
