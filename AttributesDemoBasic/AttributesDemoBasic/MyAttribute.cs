using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributesDemoBasic
{
    //  quy định phạm vi áp dụng của một custom attribute trong C#.
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    class MyAttribute : Attribute
    {

        public  MyAttribute(string name)
        {
            fullname = name;
        }

        public string fullname { get; private set; }
    }
}
