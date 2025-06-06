namespace AsyncDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Main - 1 - Thread ID: {Environment.CurrentManagedThreadId}");

            Task t = Task.Run(AsyncFunc1);
            await AsyncFunc2();

            Console.WriteLine($"Main - 2 - Thread ID: {Environment.CurrentManagedThreadId}");

            t.Wait();
        }

        private static async Task<int> AsyncFunc2()
        {
            Console.WriteLine($"Async2 - 1 - Thread ID: {Environment.CurrentManagedThreadId}");

            var r = (await File.ReadAllTextAsync("../../../sample.txt")).Length;

            Console.WriteLine($"Async2 - 2 - Thread ID: {Environment.CurrentManagedThreadId}");
            return r;
        }

        private static int AsyncFunc1()
        {
            Console.WriteLine($"AsyncFunc1 - Thread ID: {Environment.CurrentManagedThreadId}");
            return 0;
        }
    }
}
