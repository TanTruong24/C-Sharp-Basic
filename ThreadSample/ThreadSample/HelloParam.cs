using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSample
{
    class HelloParam
    {
        public required string Name { get; set; }

        public int Delay { get; set; } = 1000;

        public CancellationToken CancellationToken { get; set; }
    }
}
