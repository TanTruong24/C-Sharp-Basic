using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    // Lớp kế thừa từ Animal, triển khai IDomesticAnimal và ghi đè phương thức Speak
    class Dog : Animal, IDomesticAnimal
    {
        // Explicit implementation của Speak() từ IAnimal
        void IAnimal.Speak()
        {
            Console.WriteLine("Dog barks");
        }

        // Ghi đè phương thức Speak từ lớp cơ sở Animal
        public override void Speak()
        {
            Console.WriteLine("Dog make a soud");
        }

        // Implement phương thức Feed từ IDomesticAnimal
        public void Feed()
        {
            Console.WriteLine("Dog is being fed");
        }
    }
}
