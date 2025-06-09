
namespace CriticalSectionDemo
{
    internal class Program
    {

        static int x = 10;
        static int y = 20;

        static object lockObject = new object();

        static void Main(string[] args)
        {
            var t = new Thread(new ThreadStart(P)) { IsBackground = true };
            t.Start();

            PrintXY();
            Swap();
            PrintXY();
        }

        private static void Swap()
        {
            /**
             * dịch ra
             * https://sharplab.io/
             *             
             *  object obj = lockObject;
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    int num = x;
                    Thread.Sleep(100);
                    x = y;
                    Thread.Sleep(200);
                    y = num;
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            */

            lock (lockObject)
            {
                // critical section
                int t = x;
                Thread.Sleep(100);
                x = y;
                Thread.Sleep(200);
                y = t;
            }

        }

        private static void PrintXY()
        {
            lock (lockObject)
            {
                Console.WriteLine($"x = {x}, y = {y}");
            }
        }

        private static void P()
        {
            while (true)
            {
                PrintXY();

            }
        }
    }
}
