namespace LinQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            basicLinq();
        }


        static void basicLinq()
        {
            var dataSource = GetInNums();
            Print(dataSource);

            // cách 1: viết như sql -> query syntax
            // khi khai báo biểu thức linq, linq chưa thực thi
            // chỉ đến khi nào lấy kết quả thì biểu thức mới thực thi
            var ns = from n in dataSource
                     where GreaterThanZero(n) && n % 2 == 0 // có thể dùng toán tử c#
                     select n;

            // chỉ khi bấm enter thì mới thực thi tiếp print ns -> thực thi linq
            //Console.ReadLine();
            //Print(ns);

            // cách 2: method syntax
            var ns2 = dataSource.Where(n => GreaterThanZero(n) && n % 2 == 0);
            Print(ns2);
            Console.WriteLine(ns2.Sum());
        }

        static bool GreaterThanZero(int n)
        {
            Console.WriteLine($"{n} > 0 = {n > 0}");
            return n > 0;
        }

        static IEnumerable<int> GetInNums()
        {
            var ns = new int[] { -2, 4, -1, 543, -123, 54, 123, 54, 423, 23, 54, 23 };
            return ns;
        }
        
        static void Print(IEnumerable<int> ns)
        {
            Console.WriteLine("=============");
            foreach (var item in ns)
            {
                Console.WriteLine(item);
            }
        }
    }
}
