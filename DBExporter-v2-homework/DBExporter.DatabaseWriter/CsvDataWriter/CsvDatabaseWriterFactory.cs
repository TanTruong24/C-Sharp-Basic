using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.DatabaseWriter.CsvDataWriter
{
    public class CsvDatabaseWriterFactory : IDataWriterFactory
    {
        public CsvDatabaseWriterFactory()
        {
        }

        public IDataWriter GetDataWriter()
        {
            return new CsvDataWriter();
        }
    }
}
