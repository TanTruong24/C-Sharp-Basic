using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExportEngine.Formatters
{
    public class SqlExporter : IDataExporter
    {
        public string FileExtension => ".sql";

        public string Export(List<Dictionary<string, object>> data, string outputPath, string tableName)
        {
            if (data.Count == 0)
            {
                return outputPath;
            }

            var sb = new StringBuilder();

            foreach (var row in data)
            {
                var cols = string.Join(", ", row.Keys);
                var vals = string.Join(", ", row.Values.Select(v => $"'{v?.ToString()?.Replace("'", "''")}'"));
                sb.AppendLine($"INSERT INTO {tableName} ({cols}) VALUES ({vals});");
            }

            File.WriteAllText(outputPath, sb.ToString());
            return outputPath;
        }
    }
}
