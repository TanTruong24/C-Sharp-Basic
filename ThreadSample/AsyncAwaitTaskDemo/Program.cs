using System.Diagnostics;
using System.Threading.Tasks;

// https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/
namespace AsyncAwaitTaskDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            await Delay1();
            await Delay2();
            await Delay3();

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
        }

        private static async Task Delay1()
        {
            Console.WriteLine("Delay1...");
            await Task.Delay(1000);
        }

        private static async Task Delay2()
        {
            Console.WriteLine("Delay2...");
            await Task.Delay(2000);
        }

        private static async Task Delay3()
        {
            Console.WriteLine("Delay3...");
            await Task.Delay(3000);
        }
    }
}
