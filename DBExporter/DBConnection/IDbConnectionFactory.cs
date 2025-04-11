using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection(string connectionString);
    }
}
