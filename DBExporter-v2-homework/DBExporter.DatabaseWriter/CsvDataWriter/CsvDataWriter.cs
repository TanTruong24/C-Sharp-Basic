using DBExporter.DatabaseObjects;
using System.Data.Common;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace DBExporter.DatabaseWriter.CsvDataWriter
{
    public class CsvDataWriter : IDataWriter
    {
        public CsvDataWriter()
        {
        }

        public void WriteData(ExportSource exportSource, Stream outputStream)
        {
            using var writer = new StreamWriter(outputStream, leaveOpen: true);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldQuote = _ => true
            };

            using var csv = new CsvWriter(writer, config);

            var columnSchema = exportSource.Reader.GetColumnSchema();
            WriteHeader(csv, columnSchema);
            WriteRows(csv, exportSource, columnSchema.Count);

            csv.Flush();
        }

        private static void WriteHeader(CsvWriter csv, IReadOnlyList<System.Data.Common.DbColumn> columnSchema)
        {
            foreach (var column in columnSchema)
            {
                csv.WriteField(column.ColumnName ?? string.Empty);
            }
            csv.NextRecord();
        }

        private static void WriteRows(CsvWriter csv, ExportSource exportSource, int columnCount)
        {
            while (exportSource.Reader.Read())
            {
                for (int i = 0; i < columnCount; i++)
                {
                    var value = exportSource.Reader.IsDBNull(i) ? string.Empty : exportSource.Reader[i]?.ToString();
                    csv.WriteField(value);
                }
                csv.NextRecord();
            }
        }
    }
}
