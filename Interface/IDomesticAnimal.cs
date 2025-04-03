using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    // Interface kế thừa từ IAnimal (Derived Interface)
    public interface IDomesticAnimal : IAnimal
    {
        void Feed(); // Phương thức mới chỉ có ở IDomesticAnimal
    }
}
