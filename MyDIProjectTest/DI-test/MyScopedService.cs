using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_test
{
    internal class MyScopedService : IMyScopedService
    {
        private static int Id = 0;
        private Guid _guid = Guid.NewGuid();

        public MyScopedService()
        {
            Console.WriteLine($"Scoped Created: {++Id}, Guid = {_guid}");
        }

        public Guid GetGuid() => _guid;
    }
}
