using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportOptions
{
    public class ExporterFactory
    {
        private readonly Dictionary<string, Func<IDataExporter>> _registry;

        public ExporterFactory()
        {
            _registry = new Dictionary<string, Func<IDataExporter>>(StringComparer.OrdinalIgnoreCase)
        {
            { "csv", () => new CsvExporter() },
            { "sql", () => new SqlExporter() },
            { "json", () => new JsonExporter() }
            // muốn thêm XML, XLSX → chỉ thêm 1 dòng
        };
        }

        public IDataExporter GetExporter(string format)
        {
            if (!_registry.TryGetValue(format, out var exporterFactory))
                throw new NotSupportedException($"Format '{format}' is not supported.");
            return exporterFactory();
        }

        public bool IsSupported(string format) => _registry.ContainsKey(format);
    }

}
