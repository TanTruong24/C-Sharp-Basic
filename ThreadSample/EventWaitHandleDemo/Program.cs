/**
 * EventWaitHandle là một primitive đồng bộ hóa (synchronization primitive) trong .NET, 
 * được dùng để đồng bộ hóa giữa các thread — nghĩa là giúp một thread chờ (wait) cho đến khi một tín hiệu (signal) từ thread khác được gửi đến.
 * 
 * Về bản chất, nó là một "công tắc", 
 * cho phép một hoặc nhiều thread dừng lại tại WaitOne() cho đến khi thread khác gọi Set() để bật công tắc cho phép tiếp tục.
 * 
 * Khi bạn cần giữ cho một thread chờ cho đến khi có tín hiệu từ thread khác.
 * Khi bạn cần điều khiển rõ ràng việc chờ và đánh thức giữa nhiều thread.
 * 
 */
namespace EventWaitHandleDemo
{
    internal class Program
    {
        static BlockingQueue<string> queue = new();

        static void Main(string[] args)
        {
            var t = new Thread(DeQueueThread) { IsBackground = true };
            t.Start();

            t = new Thread(DeQueueThread) { IsBackground = true };
            t.Start();

            string? s = null;

            // test thử trường hợp deque in ra 2 giá trị giống nhau
            //for (int i =0; i < 100000; i++)
            //{
            //    s = i.ToString();
            //    queue.EnQueue(s);
            //}

            do
            {
                Console.WriteLine("s: ");
                s = Console.ReadLine();

                if (!string.IsNullOrEmpty(s))
                {
                    //queue.EnQueueError(s);
                    queue.EnQueue(s);
                }
                Thread.Sleep(1000);
            } while (!string.IsNullOrEmpty(s));
        }

        static void DeQueueThreadError()
        {
            while(true)
            {
                //if (!queue.IsEmpty())
                //{
                //    var s = queue.DeQueue();
                //    Console.WriteLine($"Q: {s}");
                //}

                Console.WriteLine($"{Environment.CurrentManagedThreadId}");

                var s = queue.DeQueueError();
                Console.WriteLine($"Q: {s}");
            }

        }

        static void DeQueueThread()
        {
            while (true)
            {

                Console.WriteLine($"DeQueueThread: {Environment.CurrentManagedThreadId}");

                var s = queue.DeQueue();
                Console.WriteLine($"Q: {s}");
            }

        }
    }
}
