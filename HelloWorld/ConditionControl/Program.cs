namespace ConditionControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 3, 4, 5 };
            loop(nums);
        }

        static void loop(int[] args)
        {
            // for
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"for: {args[i]}");
            }

            // foreach
            foreach (var item in args)
            {
                Console.WriteLine($"foreach: {item}");
            }

            // while
            int m = 0;
            while (m < 4)
            {
                Console.WriteLine($"while m: {m}");
                m++;
            }

            // do-while
            int x = -1;
            do
            {
                Console.WriteLine($"do while: {x}");
                x += 1;
            }
            while (x < 3);
        }

        static void condition(string[] args)
        {
            // if, else if, else
            int x = 2;
            int y = 2;

            if (x == 3)
            {
                Console.WriteLine("x == y3");
            }
            else if (y == x)
            {
                Console.WriteLine($"x == y == {x}");
            }
            else
            {
                Console.WriteLine("x <> y");
            }

            // switch case
            switch (x)
            {
                case 1:
                    Console.WriteLine("x == 1");
                    break;
                case 2:
                    Console.WriteLine("x == 2");
                    break;
                default:
                    Console.WriteLine("not equal");
                    break;
            }
        }
    }
}
