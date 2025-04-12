using DBExporter.DatabaseWriter.SqlDataWriter;

namespace DBExporter.DatabaseWriter.SqlDataWriter
{
    public class SqlDatabaseWriterFactory : IDataWriterFactory
    {
        public SqlDatabaseWriterFactory() { }
        public IDataWriter GetDataWriter()
        {
            return new SqlDataWriter();
        }
    }
}
