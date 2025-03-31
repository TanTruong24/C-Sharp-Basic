namespace oop_polymorphism
{
    /**
    * 3 phương pháp triểu khai tính đa hình trong oop .net, liên quan đến virtual method
    * 
    * - 1 Ghi đè (Override) - đa hình thời điểm chạy (runtime)
    *   + "The derived class may override virtual members in the base class, defining new behavior."
    *   + sử dụng từ khóa override trên một virtual method
    *   => lớp con cung cấp cách triển khai mới cho một phương thức ảo đã được định nghĩa trong lớp cha. KHi gọi phương thưc qua lớp cha,
    *   hệ thóng sẽ quyết định tại thời điểm chạy lớp con nào thực sự thực hiện hành vi
    *   
    * - 2 kế thừa hành vi gốc mà không ghi đè - vẫn cho lớp con sau override
    *   + "The derived class may inherit the closest base class method without overriding it, preserving the existing behavior but enabling further derived classes to override the method."
    *   => "Lớp dẫn xuất có thể kế thừa phương thức từ lớp cơ sở gần nhất mà không ghi đè, tức là giữ nguyên hành vi hiện tại nhưng vẫn cho phép các lớp con tiếp theo có thể ghi đè phương thức đó sau này."
    *   
    *   + kế thừa đơn thuần - không override method. Runtime đa hình tiềm ẩn, có thể mở rộng sau này.
    *   => Lớp con không ghi đè method virtual  từ lớp cha, nên giữ nguyên hành vi. Nhưng do method vẫn là virtual, các lớp kế tiếp vấn có thể ghi đè. Đây là cách giữ tính mở rộng của hệ thống
    *   
    * - 3 Định nghĩa lại mehod (hide) - đa hình thời điểm biên dịch (compile-time) - sử dụng từ khóa NEW
    *   + "The derived class may define new non-virtual implementation of those members that hide the base class implementations."
    *   + Hiding -> định nghĩ phương thức cùng tên mà không dùng override
    *   + đa hình thời điểm biên dịch, đôi khi còn gọi là shadowing/hiding
    *   => lớp con định nghĩa một method mới trung tên với lớp cha nhưng không ghi đè, khi gọi thông qua kiểu của lớp cha,
    *   phương thức lớp cha vân chạy. Điều này dựa vào kiểu biến tại thời điêm biên dịch
    *   
    *       * thời gian biên dich (compile time)
    *           - là giai đoạn chương trình được biên dịch (build) - tức là dịch từ mã nguồn (code) sang mã máy
    *           - xảy ra trước khi chương trình được chạy
    *           - những gì xảy ra trong thời gian biên dịch: trình biên dịch kiểm tra lỗi cú pháp, kiểu dữ liệu, từ khóa. Xác định kiểu dữ liệu của biến. Tạo file .exe hoặc .dll
    *           
    *       * thời gian chạy (runtime):
    *           - là giai đoạn chương trình đang chạy, khi người dùng thực sự sử dụng phần mềm
    *           - xảy ra sau khi biên dịch xong, khi chương trình thực thi
    *           - những gì xảy ra: Xử lý logic chương trình, vòng lặp, điều kiện. Gọi hàm ảo (virtual), đa hình. Xử lý dữ liệu thật, nhập xuất.
    *           
    *      ??? nếu thời gian chạy sau thời gian biên dịch, thì lúc biên dịch tại sao không gọi hàm luôn mà phải tới lúc thời gian chạy mới xác định gọi
    *      - vì sao phải đợi thời gian chạy mới xác định gọi hàm?
    *      -> bởi lúc biên dịch, trình biên dịch KHÔNG BIẾT CHẮC kiểu thật sự của đối tượng sẽ là gì
    *           + kiểu khai báo thì biết từ trước (compile time biết)
    *           + nhưng kiểu thực tế thì chỉ biết khi chương trình thực sự chạy
    *           
    *           ```
    *           using System;

                public class Animal
                {
                    public virtual void Speak()
                    {
                        Console.WriteLine("Animal speaks");
                    }
                }

                public class Dog : Animal
                {
                    public override void Speak()
                    {
                        Console.WriteLine("🐶 Dog barks!");
                    }
                }

                public class Cat : Animal
                {
                    public override void Speak()
                    {
                        Console.WriteLine("🐱 Cat meows!");
                    }
                }

                public class Program
                {
                    public static void Main()
                    {
                        Console.WriteLine("Choose an animal (dog/cat): ");
                        string input = Console.ReadLine();

                        Animal a;

                        if (input == "dog")
                        {
                            a = new Dog();  // Lúc chạy mới tạo
                        }
                        else if (input == "cat")
                        {
                            a = new Cat();  // Lúc chạy mới tạo
                        }
                        else
                        {
                            a = new Animal();  // fallback
                        }

                        a.Speak();  // Không biết gọi Dog/Cat/Animal tới khi chạy
                    }
                }
            ```
     + Khi biên dịch:
    Trình biên dịch chỉ thấy a là Animal.
    Không thể biết người dùng sẽ nhập "dog" hay "cat".
    Chỉ chuẩn bị gọi hàm Speak() với cơ chế ảo (virtual).

    + Khi chạy chương trình:
    Người dùng nhập "dog" → tạo ra new Dog().
    Gán cho biến a, dù a là kiểu Animal
    Nhờ có virtual + override, lệnh a.Speak() sẽ gọi đúng Dog.Speak().

    */
    internal class Program
    {
        static void Main(string[] args)
        {
            //var rand = new Random();

            //Animal animal = GetAnimal(rand.Next(0, 2));

            //animal.Move();

            Logger log = new FileLogger();
            log.LogInfo();     //  "Log info from base Logger"       ← bị ẩn, không override
            log.LogWarning();  //  "Log warning from FileLogger"     ← override đúng
            log.LogError();    //  "Log error from base Logger"      ← không ghi đè

            FileLogger fLog = new FileLogger();
            fLog.LogInfo();    // "Log info from FileLogger"        ← đúng kiểu mới
        }

        static Animal GetAnimal(int id)
        {
            switch(id)
            {
                case 0:
                    return new Dog();
                case 1:
                    return new Bird();
                default:
                    return new Fish();

            }
        }

        /**
         * Ví dụ sử dụng từ khóa new và override
         * 
         * Khi nào dùng từ khóa new
         *      - Khi lớp cha không có virtual, bạn không thể dùng override.
         *      - Khi bạn muốn định nghĩa lại method hoàn toàn riêng cho lớp con.
         *      - Khi bạn muốn ẩn (hide) method lớp cha, không ghi đè.
         */
        public class Logger
        {
            public void LogInfo()
            {
                Console.WriteLine("Log info from base Logger");
            }

            public virtual void LogWarning()
            {
                Console.WriteLine("Log warning from base Logger");
            }

            public virtual void LogError()
            {
                Console.WriteLine("Log error from base Logger");
            }
        }

        public class FileLogger : Logger
        {
            // Ẩn LogInfo – compile-time binding
            public new void LogInfo()
            {
                Console.WriteLine("Log info from FileLogger (new)");
            }

            // Ghi đè LogWarning – run-time binding
            public override void LogWarning()
            {
                Console.WriteLine("Log warning from FileLogger (override)");
            }

            // Không override LogError → dùng y chang từ base
        }

        /**
         * ???? tạo hành vi riêng nhưng không gọi được method của mình mà lại gọi method của lớp cha
         * 
         * => “Tạo hành vi riêng (dùng new) nhưng không gọi được method của mình mà lại gọi method của lớp cha” – là vì bạn đang gọi thông qua kiểu của lớp cha.
         * => Khi bạn ẩn (new) một phương thức, bạn chỉ tạo một phiên bản mới của method đó trong lớp con.
         * Nhưng nếu bạn gọi thông qua kiểu của lớp cha, thì method của lớp cha vẫn được dùng, vì hàm không phải virtual nên không có đa hình động.
         */

    }
}
