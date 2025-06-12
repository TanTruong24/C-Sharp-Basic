using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitTaskDemo.BreakfastSample
{
    /**
     *  Tóm tắt:
        - Khởi động các task bất đồng bộ sớm
        - Nhưng lại chờ (await) chúng quá muộn
        - Hệ quả: các task đã hoàn thành, nhưng không xử lý ngay khi hoàn tất
        - Trong ví dụ nấu ăn: điều này giống như để trứng và bacon nằm trên chảo sau khi đã chín, dẫn đến bị cháy
     * 
     * Time → →
        Main(): 
        │
        ├─ PourCoffee()
        │     └─ [Done ngay] --> "Coffee is ready"
        │
        ├─ FryEggsAsync()  ──────────────┐
        │                                │
        ├─ FryBaconAsync() ──────────────┐
        │                                │
        ├─ ToastBreadAsync() ── await ──▶│  (Main() tạm dừng ở đây đợi toast)
        │                                │
        │    → toast xong                │
        │    → ApplyButter & ApplyJam    │
        │    → "Toast is ready"          │
        │
        ├─ PourOJ()
        │    └─ "Juice is ready"
        │
        ├─ await eggsTask ─────────────▶ Trứng đã nấu xong từ trước → giờ mới xử lý
        │     └─ "Eggs are ready (but might be burnt!)"
        │
        ├─ await baconTask ────────────▶ Bacon cũng đã chín → giờ mới lấy ra
        │     └─ "Bacon is ready (but might be burnt!)"
        │
        └─ Done → "Breakfast is ready!"
     * 
     */
    public class WrongLogicAsyncBreakfast
    {
        public static async Task RunAsync(string[] args)
        {
            var sw = Stopwatch.StartNew();

            Coffee cup = PourCoffee();
            Console.WriteLine("Coffee is ready");

            // Khởi động các task cùng lúc
            // Đây là bước khởi động task bất đồng bộ → task chạy ngay lập tức nhưng chưa bị await
            // Không có await, nên phương thức Main() tiếp tục chạy tiếp mà không chờ
            var eggsTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = ToastBreadAsync(2);

            // Không chờ trứng và bacon, xử lý toast trước
            // Ở đây await sẽ tạm dừng Main() cho đến khi toastTask hoàn thành.
            var toast = await toastTask;
            // Khi xong, kết quả được gán vào toast, rồi bạn phết bơ, phết mứt, in Toast is ready
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("Toast is ready");

            Juice oj = PourOJ();
            Console.WriteLine("Juice is ready");

            // Bây giờ mới chờ egg và bacon (chúng đã xong từ trước)
            // Lúc này, eggsTask đã chạy xong từ lâu, nhưng bạn vẫn chưa xử lý
            var eggs = await eggsTask;
            Console.WriteLine("Eggs are ready (but might be burnt!)");

            var bacon = await baconTask;
            Console.WriteLine("Bacon is ready (but might be burnt!)");

            sw.Stop();
            Console.WriteLine($"Breakfast is ready in {sw.Elapsed.TotalSeconds} seconds!");
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee...");
            return new Coffee();
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice...");
            return new Juice();
        }

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int i = 0; i < slices; i++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(2000); // Toast nhanh
            Console.WriteLine("Remove toast from toaster");
            return new Toast();
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"Putting {slices} slices of bacon in the pan");
            Console.WriteLine("Cooking first side of bacon...");
            await Task.Delay(3000);
            for (int i = 0; i < slices; i++)
            {
                Console.WriteLine("Flipping a slice of bacon");
            }
            Console.WriteLine("Cooking second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");
            return new Bacon();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(2000);
            Console.WriteLine($"Cracking {howMany} eggs");
            Console.WriteLine("Cooking the eggs...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");
            return new Egg();
        }
    }

}
