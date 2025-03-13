namespace method
{
    internal class Program
    {
        // https://learn.microsoft.com/en-us/dotnet/csharp/methods
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/methods
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int x = 2;
            int y = 3;

            Console.WriteLine(AddTwoIntegerByValue(x, y));
            Console.WriteLine($"by value: {x}, {y}");
            Console.WriteLine(AddTwoIntegerByRefernece(ref x, ref y));
            Console.WriteLine($"by reference: {x}, {y}");

            //
            Console.WriteLine("================");
            var mc = new MyClass() { M = 1 }; // memory: mc->[1000] = MyClass->[9000] 
            Print(mc);

            // ham UpdateMyClass->[2000] = [1000] = [9000]
            UpdateMyClass(mc);

            Print(mc);
        }

        public static int AddTwoIntegerByValue(int x, int y)
        {
            return x + y;
        }

        public static int AddTwoIntegerByRefernece(ref int x, ref int y) 
        {
            x += 1;
            y += 2;
            return (x + y);
        }

        static void Print(MyClass mc)
        { 
            Console.WriteLine($"mc.M = {mc.M}");
        }

        static void UpdateMyClass(MyClass mc) //[2000]
        {
            // tạo vùng nhớ mới -> không ảnh hưởng mc ở ngoài
            //mc = new MyClass() { M = 222222 };

            // Thay đổi tạo ví trí vùng nhớ của mc M, 
            mc.M = 200;
            Print(mc);
        }
    }
}
