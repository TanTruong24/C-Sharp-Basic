using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_polymorphism
{
    internal class Bird: Animal
    {
        public override void Move()
        {
            Console.WriteLine("Bird fly");
        }
    }
}
