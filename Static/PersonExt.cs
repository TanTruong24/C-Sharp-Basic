using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    internal static class PersonExt
    {
        public static void Print(this Person person)
        {
            Console.WriteLine($"ID: {person.Id}");
            Console.WriteLine($"Name: {person.Name}");
        }
    }
}
