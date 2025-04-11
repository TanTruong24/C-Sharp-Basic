using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportOptions
{
    public class CsvExporter : IDataExporter
    {
        public string Export(object data)
        {
            // Convert data to CSV format
            return "Data exported as CSV";
        }
    }

    public class SqlExporter : IDataExporter
    {
        public string Export(object data)
        {
            // Convert data to INSERT INTO ...
            return "Data exported as SQL";
        }
    }

    public class JsonExporter : IDataExporter
    {
        public string Export(object data)
        {
            // Convert data to JSON
            return "Data exported as JSON";
        }
    }

}
