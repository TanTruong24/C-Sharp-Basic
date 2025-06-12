using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitTaskDemo.BreakfastSample
{
    /**
     * 
     * Pouring coffee
        coffee is ready
        Warming the egg pan...
        putting 3 slices of bacon in the pan
        cooking first side of bacon...
        Putting a slice of bread in the toaster
        Putting a slice of bread in the toaster
        Start toasting...
        flipping a slice of bacon
        flipping a slice of bacon
        flipping a slice of bacon
        cooking the second side of bacon...
        cracking 2 eggs
        cooking the eggs ...
        Remove toast from toaster
        Putting butter on the toast
        Putting jam on the toast
        toast is ready
        Put bacon on plate
        Put eggs on plate
        bacon is ready
        eggs are ready
        Pouring orange juice
        oj is ready
        Breakfast is ready!
     * 
     * Time → →

        Main():
        │
        ├─ PourCoffee()
        │     └── "coffee is ready"
        │
        ├─ FryEggsAsync() ─────────────┐
        │                              │
        ├─ FryBaconAsync() ────────────┤──▶ (Eggs & Bacon đang chạy ngầm)
        │                              │
        ├─ MakeToastWithButterAndJamAsync()
        │     └── ToastBreadAsync()
        │         └── [Toast xong sau 3s]
        │     └── ApplyButter()
        │     └── ApplyJam()
        │     └── return Toast
        │
        ├─ await Task.WhenAny(...) ⇢ xử lý từng task hoàn thành:
        │     ├─ if finishedTask == toastTask
        │     │     └── "toast is ready"
        │     ├─ if finishedTask == eggsTask
        │     │     └── "eggs are ready"
        │     ├─ if finishedTask == baconTask
        │           └── "bacon is ready"
        │
        ├─ PourOJ()
        │     └── "oj is ready"
        │
        └─ "Breakfast is ready!"
     * 
     */
    class FinalInstrucBreakfast
    {
        public static async Task RunAsync(string[] args)
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            /**
             * 
             * Trong thực tế, thứ tự hoàn thành (toast / eggs / bacon xong trước) không phụ thuộc vào việc bạn await trong MakeToastWithButterAndJamAsync(), 
             * mà phụ thuộc vào tổng thời gian thực thi của toàn bộ task đó so với các task khác.
             * 
             * ➡️ Nghĩa là MakeToastWithButterAndJamAsync là một Task, và nó chỉ hoàn thành khi:
                ToastBreadAsync hoàn tất
                và cả ApplyButter + ApplyJam chạy xong
             * 
             *  | Task                                | Thành phần                         | Tổng thời gian ước lượng |
                | ----------------------------------- | ---------------------------------- | ------------------------ |
                | `FryEggsAsync(2)`                   | Delay(3s) + Delay(3s)              | \~6s                     |
                | `FryBaconAsync(3)`                  | Delay(3s) + Delay(3s)              | \~6s                     |
                | `MakeToastWithButterAndJamAsync(2)` | Delay(3s) + ApplyButter + ApplyJam | \~3.5–4s                 |

             * 
             * ➡️ Vì task toast có thời gian ngắn hơn so với eggs và bacon, nên nó thường hoàn thành trước → nhưng không đảm bảo tuyệt đối.
             * 
             * var toast = await ToastBreadAsync(2);  // trực tiếp trong Main
             *  → Thì Main() sẽ bị dừng tại đây và chờ toast xong, các task khác chưa bắt đầu
                → Toast chắc chắn xong trước vì bạn đang chặn lại ở đó.
             *
             * var toastTask = MakeToastWithButterAndJamAsync(2);  // khởi động ngầm
             * → Task toast chạy song song với các task khác → chưa chắc đã xong trước.
             */
            var eggsTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                await finishedTask;
                breakfastTasks.Remove(finishedTask);
            }

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
        }

        static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }
}
