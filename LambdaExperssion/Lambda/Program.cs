namespace Lambda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====Input Calculation:");

            string op = Console.ReadLine();
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());

            try
            {
                Console.WriteLine($"Calculation: {CalculatorBasic(op, a, b)}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // ================

            Console.WriteLine("========Print List name: ");

            string str = Console.ReadLine();
            List<string> names = str.Split(" ").ToList();

            Dictionary<string, string> dic = new Dictionary<string, string>() { { "a", "A" }, { "b", "B" } };
            Print(names, dic);

            // ================

            Console.WriteLine("========Even numbers: ");
            string strNums = Console.ReadLine();
            List<int> nums = strNums.Split(" ").Select(int.Parse).ToList();
            FilterEvenNumber(nums);
        }

        static int CalculatorBasic(string op, int a, int b)
        {
            Dictionary<string, Func<int, int, int>> opertations = new Dictionary<string, Func<int, int, int>>();

            opertations.Add("+", (a, b) => a + b);
            opertations.Add("-", (a, b) => a - b);
            opertations.Add("/", (a, b) => b != 0 ? a/b : throw new DivideByZeroException());
            opertations.Add("*", (a, b) => a * b);

            return opertations[op](a, b);
        }

        static void Print(List<string> input, Dictionary<string, string> dic)
        {
            Action<string> printList = item => Console.WriteLine($"Hello: {item}");

            Action<string, string> printDic = (a, b) => Console.WriteLine($"Hello: {a} - {b}");

            input.ForEach(printList);

            foreach (var item in dic)
            {
                printDic(item.Key, item.Value);
            }
        }

        static void FilterEvenNumber(List<int> nums)
        {

            Func<int, bool> evenNumFunc = (a) => a % 2 == 0;

            List<int> evenNum1 = new List<int>();

            nums.ForEach(num =>
            {
                if (evenNumFunc(num)) evenNum1.Add(num);
            });

            Console.WriteLine($"even number 1 using Func: {string.Join(" ", evenNum1)}");


            Predicate<int> evenNum2 = (a) => a % 2 == 0;

            List<int> eventNum2 = nums.FindAll(evenNum2);

            Console.WriteLine($"even number 2 using Perdicate, FindAll: {string.Join(" ", eventNum2)}");
        }
    }
}
