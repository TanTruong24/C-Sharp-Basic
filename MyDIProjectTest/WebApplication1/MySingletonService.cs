using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    internal class MySingletonService : IMySingletonService
    {
        private static int Id = 0;
        private Guid _guid = Guid.NewGuid();

        public MySingletonService()
        {
            Console.WriteLine($"Singleton Created: {++Id}, Guid = {_guid}");
        }

        public Guid GetGuid() => _guid;
    }
}
