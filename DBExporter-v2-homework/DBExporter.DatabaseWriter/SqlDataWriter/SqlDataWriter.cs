using DBExporter.DatabaseObjects;
using System.Data.Common;
using System.Globalization;
using System.Text;
using DBExporter.Options;

namespace DBExporter.DatabaseWriter.SqlDataWriter
{
    public class SqlDataWriter : IDataWriter
    {
        public void WriteData(ExportSource database, Stream stream)
        {
            using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);

            var tableName = GetTableName(database.TableNames);

            var columns = database.Reader.GetColumnSchema();
            var columnNames = columns.Select(c => $"[{c.ColumnName}]").ToArray();

            while (database.Reader.Read())
            {
                var values = new List<string>();

                for (int i = 0; i < columns.Count; i++)
                {
                    var value = database.Reader.IsDBNull(i) ? "NULL" : EscapeValue(database.Reader[i]);
                    values.Add(value);
                }

                string insert = $"INSERT INTO {tableName} ({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", values)});";
                writer.WriteLine(insert);
            }

            writer.Flush();
        }

        private static string GetTableName(string? tableNames)
        {
            return !string.IsNullOrWhiteSpace(tableNames)
                ? tableNames
                : "UNKNOWN";
        }


        private static string EscapeValue(object value)
        {
            if (value is string str)
            {
                return $"N'{str.Replace("'", "''")}'";
            }

            if (value is DateTime dt)
            {
                return $"'{dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'";
            }

            if (value is bool b)
            {
                return b ? "1" : "0";
            }

            if (value is null)
            {
                return "NULL";
            }

            return $"'{Convert.ToString(value, CultureInfo.InvariantCulture)}'";
        }
    }
}
