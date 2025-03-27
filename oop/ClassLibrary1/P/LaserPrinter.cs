using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.P
{
    public class LaserPrinter : Printer
    {
        public LaserPrinter(string name) : base(name, "xyz") { }
        public int resolution {  get; set; }

        public override void MyAbstractMethod()
        {
        }
    }
}
