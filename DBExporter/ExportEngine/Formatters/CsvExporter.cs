using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportEngine.Formatters
{
    public class CsvExporter : IDataExporter
    {
        public string FileExtension => ".csv";

        public string Export(List<Dictionary<string, object>> data, string outputPath)
        {
            var sb = new StringBuilder();

            if (data.Count > 0)
            {
                var header = data[0].Keys;
                sb.AppendLine(string.Join(",", header));

                foreach (var row in data)
                {
                    var line = string.Join(",", header.Select(k => row[k]?.ToString()));
                    sb.AppendLine(line);
                }
            }

            File.WriteAllText(outputPath, sb.ToString());
            return outputPath;
        }
    }
}
