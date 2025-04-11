using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter
{
    public static class DataUtils
    {
        public static List<Dictionary<string, object>> QueryToList(DbConnection conn, string query)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = query;

            using var reader = cmd.ExecuteReader();

            var result = new List<Dictionary<string, object>>();
            var colCount = reader.FieldCount;
            var colNames = new string[colCount];

            for (int i = 0; i < colCount; i++)
            {
                colNames[i] = reader.GetName(i);
            }

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i=0; i<colCount; i++)
                {
                    row[colNames[i]] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                result.Add(row);
            }
            return result;
        }
    }
}
