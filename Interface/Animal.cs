using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    // base class triển khai IAnimal và cung cấp triển khai mặc định
    public class Animal : IAnimal
    {
        public virtual void Speak()
        {
            Console.WriteLine("Animal make a sound");
        }
    }
}
