using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    public interface IDatabaseConnectionBuilder
    {
        IDatabaseConnectionBuilder SetConnectionString(string connectionString);

        IDatabaseConnectionBuilder SetProvider(string provider);

        DbConnection Build();
    }
}
