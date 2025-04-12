using DBExporter.DatabaseObjects;
using DBExporter.DatabaseBuilder.ConcreteServer;
using DBExporter.Options;

namespace DBExporter.DatabaseBuilder
{
    public static class DatabaseBuilderFactory
    {
        public static IExportSourceBuilder? Connect(ServerTypes serverTypes, string connectString)
        {
            return serverTypes switch
            {
                ServerTypes.SqlServer => new SqlServer(connectString),
                _ => null
            };
        }
    }
}
