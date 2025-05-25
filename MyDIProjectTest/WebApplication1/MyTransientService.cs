using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    internal class MyTransientService : IMyTransientService
    {
        private static int Id = 0;
        private Guid _guid = Guid.NewGuid();

        public MyTransientService()
        {
            Console.WriteLine($"Transient Created: {++Id}, Guid = {_guid}");
        }

        public Guid GetGuid() => _guid;
    }
}
