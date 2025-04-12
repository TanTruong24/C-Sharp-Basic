using System.Data;
using System.Data.Common;

namespace DBExporter.DatabaseObjects
{
    public class ExportSource : IDisposable
    {
        private bool _disposed;

        public DbConnection Connection { get; } // must be a connected, open connection
        public DbDataReader Reader { get; } // must be a connected , ready-to-read reader

        public string TableNames { get; set; } = string.Empty;

        public ExportSource(DbConnection connection, DbDataReader reader)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));

            if (connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("The connection must be opened before passed to ExportSource.");
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    if (!Reader.IsClosed)
                        Reader.Close();
                }
                catch { /* log or ignore */ }

                try
                {
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
                }
                catch { /* log or ignore */ }
            }

            _disposed = true;
        }

        ~ExportSource()
        {
            Dispose(disposing: false);
        }
    }
}
