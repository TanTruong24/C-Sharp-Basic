using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributesDemoBasic
{
 
    class Class1
    {
        // custom atttribute
        [MyAttribute("The Truong")]
        public void MyAttributeFunction()
        {
            throw new NotImplementedException();
        }

        [MyAttribute("ABC XYZ")]
        public void PrintHelloWorld()
        {
            Console.WriteLine("Hello World!");
        }

        // Cảnh báo khi sử dụng method/class đã cũ hoặc không nên dùng nữa.
        [Obsolete("Method cũ, hay dùng cái mới")]
        public void OldMethod()
        {
            Console.WriteLine("Obsolete");
        }
    }
}
