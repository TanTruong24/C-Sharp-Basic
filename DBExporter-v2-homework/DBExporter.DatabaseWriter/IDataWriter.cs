using DBExporter.DatabaseObjects;

namespace DBExporter.DatabaseWriter
{
    public interface IDataWriter
    {
        void WriteData(ExportSource exportSource, Stream outputStream);
    }
}
