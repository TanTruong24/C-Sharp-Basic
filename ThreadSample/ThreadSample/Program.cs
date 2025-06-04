namespace ThreadSample
{
    internal class Program
    {
        /**
         * trong ngữ cảnh của .net
         * - parallel 
         *      -> Thực thi nhiều tác vụ thực sự cùng lúc trên nhiều lõi CPU.
         *      -> Tăng hiệu suất cho các tác vụ độc lập nhau, thường áp dụng cho tính toán song song
         *      
         *      + để xử lý song song một cách thực sự -> số tác vụ = số cpu
         *      -> Parallel Programming không đồng nghĩa với "mọi task chạy cùng lúc", 
         *      mà là "thiết kế để nhiều task có thể chạy đồng thời nếu có đủ tài nguyên".
         * 
         * - concurrent (đồng thời) 
         *      -> nhiều việc xen kẽ để phản hồi nhanh (responsive)
         *      -> nhiều tác vụ cùng được xử lý xen kẽ trong một khoảng thời gian
         *      
         * - để xử lý song song một cách thực sự -> số tác vụ = số cpu
         */

        static bool b = false;

        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            //var t1 = new Thread(() => {
            //    while (!b)
            //    {
            //        Console.WriteLine("Hello T1, World!");

            //        Thread.Sleep(1000);
            //    }
            //});

            //var t2 = new Thread(() => {
            //    while (!b)
            //    {
            //        Console.WriteLine("Hello T2, World!");

            //        Thread.Sleep(1000);
            //    }
            //});

            var t1 = new Thread(new ParameterizedThreadStart(Print));
            var t2 = new Thread(new ParameterizedThreadStart(Print));
            var t3 = new Thread(new ParameterizedThreadStart(Print));

            /**
             * Foreground: 
             *      -> Thread chính, có vai trò quan trọng trong ứng dụng
             *      -> Ứng dụng chờ thread chạy xong mới thoát, Giữ cho process còn sống
             * 
             * Background: 
             *      -> Thread phụ, không quan trọng, thường làm tác vụ nền
             *      -> Ứng dụng dừng ngay không chờ thread chạy xong, Không giữ process – process có thể thoát bất kỳ lúc nào
             */

            //t1.IsBackground = true;
            //t2.IsBackground = true;


            t1.Start(new HelloParam() { Name = "1", CancellationToken = cts.Token});
            t2.Start(new HelloParam() { Name = "2" , Delay = 2000, CancellationToken = cts.Token });
            t3.Start(new HelloParam() { Name = "3", Delay = 3000 , CancellationToken = cts.Token });

            //Console.ReadLine();

            //b = true;
            //cts.Cancel();
            cts.CancelAfter(10000);
        }

        public static void Print(object? p)
        {
            // nếu p không phải là helloparam thì sẽ ném exception
            // var hp = (HelloParam)p;

            // nếu p không phải là HelloParam thì sẽ trả về null
            var hp = p as HelloParam;

            
            //while (!b)
            if (hp != null)
            {
                while (!hp.CancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine($"Hello, World! {hp?.Name ?? "NAME"} ");

                    Thread.Sleep(hp?.Delay ?? 1000);
                }
            }

            
        }
    }
}
