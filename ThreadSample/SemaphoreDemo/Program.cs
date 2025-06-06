namespace SemaphoreDemo
{
    internal class Program
    {
        private static Random r = new();
        private static int ItemsInBox = 0;
        private const int MAX = 10;

        private static Semaphore semaphore = new(MAX, MAX);
        private static AutoResetEvent fullEvent = new(false);

        static void Main(string[] args)
        {
            for (int i = 0; i < 8; i++)
            {
                var t = new Thread(new ParameterizedThreadStart(MoveItemThread));
                t.IsBackground = true;

                t.Start(i.ToString());
            }

            var tt = new Thread(ReplaceBox)
            {
                IsBackground = true
            };
            tt.Start();

            Console.ReadLine();
        }

        private static void MoveItemThread(object? o)
        {
            var armNumber = o?.ToString() ?? "";

            while (true)
            {
                //if (ItemsInBox < MAX)
                //{
                //    //Thread.Sleep(r.Next(100, 400));

                //    Console.WriteLine($"{armNumber} moving item...");

                //    MoveItem();

                //    Thread.Sleep(r.Next(1000, 2000));

                //    Console.WriteLine($"{armNumber} - Done");
                //}


                semaphore.WaitOne();
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId} - {armNumber} moving item...");
                MoveItem();
                //Thread.Sleep(r.Next(1000, 2000));
                Console.WriteLine($"{armNumber} - Done");

            }

        }

        private static void MoveItem()
        {
            ItemsInBox++;
            Console.WriteLine($"Current quantity: {ItemsInBox}");
            if (ItemsInBox == MAX)
            {
                fullEvent.Set();
            }
        }

        private static void ReplaceBox()
        {
            while (true)
            {
                //if (ItemsInBox == MAX)
                //{
                //    Console.WriteLine("Replace with a new box");

                //    ItemsInBox = 0;
                //    semaphore.Release(MAX);
                //}

                fullEvent.WaitOne();
                Console.WriteLine("Replace with a new box");

                ItemsInBox = 0;
                semaphore.Release(MAX);
            }
        }
    }
}
