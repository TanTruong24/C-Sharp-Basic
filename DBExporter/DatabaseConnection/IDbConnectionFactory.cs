using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection(string connectionString);
    }
}
