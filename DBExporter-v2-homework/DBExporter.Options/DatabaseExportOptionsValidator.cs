using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.Options
{
    public class DatabaseExportOptionsValidator : IDatabaseExportOptionsValidator
    {
        public void Validate(DatabaseExportOptions options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            if (string.IsNullOrWhiteSpace(options.DatabaseOptions.Query))
            {
                throw new ArgumentException("A SELECT query or table name must be provided.");
            }
        }

        public static readonly DatabaseExportOptionsValidator Instance = new(); //normally we only need exactly 1 instance
    }
}
