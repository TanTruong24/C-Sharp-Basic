using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBExportEngine.Formatters;

namespace DBExportEngine
{
    public class ExporterEngineFactory
    {
        private readonly Dictionary<string, Func<IDataExporter>> _register = new(StringComparer.OrdinalIgnoreCase)
        {
            {"csv", () => new CsvExporter()},
            {"sql", () => new SqlExporter()},
        };

        public IDataExporter GetExporter(string format)
        {
            if (!_register.TryGetValue(format, out var creator))
            {
                throw new NotSupportedException($"Format '{format}' not supported");
            }
            return creator();
        }

        public bool IsSupported(string format) => _register.ContainsKey(format);
    }
}
