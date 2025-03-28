using ClassLibrary1;
using ClassLibrary1.P;

namespace oop_v2
{
    internal class Program
    {
        /**
         abstraction  != interface
        - abstraction -> kế thừa
        - interface -> bản cam kết, implement
         */
        static void Main(string[] args)
        {
            //var printer = new Printer() { isRequired = false};
            //printer.Print("Hello world");

            //var printerName = new Printer("TAN", "Backend developer")
            //{
            //    age = 20,
            //    isRequired = true
            //};
            //printer.Print("Hello world");

            // nếu printer là abstract class tthif trong laserPrinter phải khai báo method để không lỗi
            var laserPrinter = new LaserPrinter("TRUONG") { age = 24, isRequired = true};

            var a = new A();
            a.A1();
            a.B1();
        }
    }

    /**
     internal cho phép sử dụng trong cùng .dll (cùng asembly) -> chỉ trong oop-v2
    nếu khác project mà cần sử dụng class thì -> add project reference
     */
    internal class AInternal { }

    /**
     * Lỗi: `protected` và `private` chỉ dùng cho thành viên trong class, 
        không thể áp dụng cho class độc lập trong namespace.
    */
    //protected class BError { }
    //private class CError { }


    // Hoặc dùng nested class nếu cần `protected` hoặc `private`
    internal class OuterClass
    {
        protected class B { }  // Chỉ truy cập từ class con
        private class C { }    // Chỉ truy cập trong OuterClass
    }

}

