using AsyncAwaitTaskDemo.BreakfastSample;
using System.Diagnostics;
using System.Threading.Tasks;

// https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/

/*
 * Synchronous – Đồng bộ (Tuần tự, chặn luồng)
 * Time → →
    Thread 1: ─── Task A ────► Task B ────► Task C ────► Done
               (chờ xong mới làm tiếp)
 * 
 * 
 *  2. Asynchronous – Bất đồng bộ (Không chặn, chờ ngầm)
 *  Time → →
    Thread 1: ─── Task A ─ await ──► (CPU làm việc khác) ──► tiếp tục Task A
                             ↑
                 (Task A đang đợi I/O hoặc tác vụ ngầm)
 * 
 * 
 *  3. Concurrency – Đồng thời (Xen kẽ nhiều tác vụ)
 *  Time → →
    Thread 1: ─ Task A ─┐       ┌── Task A tiếp
                        └─ Task B ─┘
               (Luân phiên xen kẽ trên 1 luồng)

 *  4. Parallelism – Song song (Chạy cùng lúc thật sự)
 *  Time → →
    Thread 1: ───────── Task A ───────────►
    Thread 2: ───────── Task B ───────────►
           (Chạy cùng lúc nhờ nhiều CPU core)

 *  
 */
namespace AsyncAwaitTaskDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Example();

            //await BreakfastSample.BreakfastSample.RunAsync(args);
        }

        static async Task Example()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            //await Delay1();
            //await Delay2();
            //await Delay3();

            var t1 = Delay1Async();
            var t2 = Delay2ASync();
            var t3 = Delay3Async();

            var tr = Task.Run(RunMethod);

            //await t1;
            //await t2;
            //await t3;

            // thoát ra khi tất cả các task hoàn thành
            //Task.WaitAll(t1, t2, t3);

            // chạy câu lệnh sau khi một trong các task hoàn thành
            //Task.WaitAny(t1, t2, t3);

            // tạo một task mới đại diện cho t1, t2, t3
            var t123 = Task.WhenAll(t1, t2, t3, tr);
            await t123;

            var t = CalculateResult(10);
            await t;


            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
        }

        private static async Task Delay1Async()
        {
            Console.WriteLine("Delay1...");
            await Task.Delay(1000);
            Console.WriteLine("Delay1 demo...");
        }

        private static async Task Delay2ASync()
        {
            Console.WriteLine("Delay2...");
            await Task.Delay(2000);
            Console.WriteLine("Delay2 demo...");
        }

        private static async Task Delay3Async()
        {
            Console.WriteLine("Delay3...");
            await Task.Delay(3000);
            Console.WriteLine("Delay3 demo...");
        }

        // cpu bound 
        static void RunMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"i: {i}");
                Task.Delay(500).Wait();
            }
        }

        // do trong hàm không có await nên hàm không cần async
        static Task<double> CalculateResult(int n)
        {
            if (n == 0) return Task.FromResult(0.0);

            return Task.FromResult(Math.Pow(n, 2));
        }
    }
}
