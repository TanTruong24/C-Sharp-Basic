using System.Security.Cryptography.X509Certificates;

namespace Static
{
    /**
       Static Classes and Static Class Members
        - static dùng để khai báo thành viên tĩnh - thuộc về class chứ không thuộc về instance (object).
        - truy cập bằng tên class, không dùng new. 
          Chỉ có một vùng nhớ được cấp phát cho một biến static (dù truy cập thay đổi giá trị ở đâu thì đều trỏ đến vùng nhớ đó -> thay đổi giá trị)
                + ví dụ có static class UtilityClass, static method MethodA -> call: UtilityClass.MethodA
                + có thể sử dụng biến static để lưu số lượng request trong hệ thống
        - sử dụng hàm static tối ưu bộ nhớ hơn vì không cần phải tạo 1 object với những thành phần không sử dụng
        - trong hàm static chỉ gọi tới các hàm static khác. Ngược lại non-static có thể gọi cả 2

        ** Đặc điểm chính static class
        *-> hạn chế dùng class static
        *
        1. Chỉ chứa các static members
            -> không thể khai báo biến hoặc phương thức non-static bên trong (nhưng non-static có thể khai báo static member)

        2. Không thể khởi tạo (instantiate)
            -> Không thể tạo đối tượng từ static class bằng new

        3. Được đánh dấu là sealed (niêm phong)
            -> không thể bị kế thừa, và không thể kế thừa từ class/interface khác, ngoại trừ Object
                - Trong C#, mọi class đều ngầm thừa kế từ System.Object, vì Object là class gốc của toàn bộ hệ thống kế thừa trong .NET
                Vì System.Object cung cấp một số phương thức cơ bản như:
                ToString()
                Equals()
                GetHashCode()
                GetType()
                Dù static class không gọi trực tiếp những hàm này như instance, nhưng về lý thuyết, mọi class trong .NET đều là Object.

        4. Không chứa constructor cho instance
            -> tực là không được có constructor thông thường (Instance Constructors), chỉ được có static constructor
            - static constructor không chứa tham số, không gồm public/private -> gọi tự động ngay khi sử dụng class. Vd static A() {}
            ```
            static class Config
            {
                public static string Path;

                static Config() {
                    Path = "config.json";
                }
            }
            ```
    */
    internal class Program
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine("Please select the convertor direction");
            Console.WriteLine("1. From Celsius to Fahrenheit.");
            Console.WriteLine("2. From Fahrenheit to Celsius.");

            string? selection = Console.ReadLine();

            switch (selection)
            {
                case "1":
                    Console.WriteLine("Input Celsius:");
                    double F = ConvertTemperature.CelsiusToFahrenheit(Double.Parse(Console.ReadLine() ?? "0"));
                    Console.WriteLine($"Temperature in Fahrenheit: {F:F2}");
                    break;
                case "2":
                    Console.WriteLine("Input Fahrenheit:");
                    double C = ConvertTemperature.FahrenhenheitToCelsius(Double.Parse(Console.ReadLine() ?? "0"));
                    Console.WriteLine($"Temperature in Celsius: {C:F2}");
                    break;
                default:
                    Console.WriteLine("Please select a convertor.");
                    break;
            }

            Console.WriteLine("==========Student=========");

            var s1 = Student.create("A01", "Truong", 9.8);
            var s2 = Student.create("A02", "The", 8.0);
            var s3 = Student.create("A03", "Tan", 10);

            Student.ShowTotalStudents();
            Student.ShowAllScore();

            var scores = Student.getAllScore();
            double avgScores = Helper.CalculateAverage(scores.Values.ToList());
            Console.WriteLine($"Average scores: {avgScores}");

            Console.WriteLine("==========PErson Extension=========");
            /**
             * Trong C#, extension class (lớp mở rộng) phải là lớp static, và tất cả các phương thức mở rộng (extension methods) bên trong nó cũng phải là static.
             * Extension class dùng để mở rộng chức năng cho một lớp có sẵn (như string, List<T>, DateTime, hoặc cả lớp tự định nghĩa) mà không cần sửa trực tiếp vào lớp đó.
             */
            Person person = new Person() { Id = 1213, Name = "Truong" };
            person.Print();

            Console.WriteLine("==========Singleton pattern=========");
            Console.WriteLine("Eager:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"counter: {AccessCounter.GetInstance().IncCounter()}");
            }

            Console.WriteLine("Lazy:");
            // Lazy connection -> khởi tạo khi dùng lần dầu
            Console.WriteLine("Start Lazy ConnectDB");

            // Gọi GetInstance lần đầu → tạo mới và gọi connect()
            var db1 = DatabaseConnection.GetInstance();
            db1.Query("SELECT * FROM users");

            // Gọi lần nữa → không tạo mới, dùng lại instance đã có
            var db2 = DatabaseConnection.GetInstance();
            db2.Query("SELECT * FROM products");

            // Kiểm tra 2 object có giống nhau không
            Console.WriteLine($"db1 and db2 same instance? {ReferenceEquals(db1, db2)}");
        }
    }
}
