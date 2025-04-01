using System.Security.Cryptography.X509Certificates;

namespace Static
{
    /**
       Static Classes and Static Class Members
        - static dùng để khai báo thành viên tĩnh - thuộc về class chứ không thuộc về instance (object).
        - truy cập bằng tên class, không dùng new
            ví dụ có static class UtilityClass, static method MethodA -> call: UtilityClass.MethodA

        ** Đặc điểm chính static class
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
        }
    }
}
