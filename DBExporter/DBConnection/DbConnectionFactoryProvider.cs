using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    public class DbConnectionFactoryProvider
    {
        private readonly Dictionary<string, IDbConnectionFactory> _factories = new();

        public DbConnectionFactoryProvider()
        {
            _factories["SqlServer"] = new SqlServerConnectionFactory();
        }

        public IDbConnectionFactory GetFactory(string provider)
        {
            if (!_factories.TryGetValue(provider, out var factory))
            {
                throw new NotSupportedException($"Provider {provider} not supported.");
            }
            return factory;
        }
    }
}
