using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExportOptions
{
    public class ExportOptionsParser
    {
        private readonly Dictionary<string, Action<string, ExportOptions>> _handlers;

        public ExportOptionsParser()
        {
            _handlers = new()
            {
                ["connStr"] = (val, opt) => opt.ConnectionString = val,
                ["query"] = (val, opt) => opt.QueryOrTable = val,
                ["outputFile"] = (val, opt) => opt.OutputFile = val,
                ["format"] = (val, opt) => opt.Format = val,
            };
        }

        public ExportOptions Parse(string[] args)
        {
            var options = new ExportOptions();

            foreach (var arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    var parts = arg.TrimStart('-').Split('=', 2);
                    var key = parts[0];
                    var hasValue = parts.Length > 1;

                    if (_handlers.TryGetValue(key, out var handler))
                    {
                        if (!hasValue)
                        {
                            throw new ArgumentException($"Missing value for argument: --{key}");
                        }
                        handler(parts[1], options);
                    }
                    else if (key == "zip")
                    {
                        options.Zip = true;
                    }
                    else if (key == "addTimestamp")
                    {
                        options.AddTimestamp = true;
                    }
                    else
                    {
                        throw new ArgumentException($"Unknown argument: --{key}");
                    }
                }
            }
            return options;
        }
    }
}
