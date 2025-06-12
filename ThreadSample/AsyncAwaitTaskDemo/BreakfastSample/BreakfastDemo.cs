using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitTaskDemo.BreakfastSample
{
    // These classes are intentionally empty for the purpose of this example. They are simply marker classes for the purpose of demonstration, contain no properties, and serve no other purpose.
    internal class Bacon { }
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }

    public class BreakfastSample
    {
        // Đặt Main thành async Task Main để có thể dùng await
        public static async Task RunAsync(string[] args)
        {
            // Gọi đúng RunAsync với args
            //await WrongLogicAsyncBreakfast.RunAsync(args);

            await FinalInstrucBreakfast.RunAsync(args);

            //SyncIntrucBreakfast.Run(args);
        }
    }
}
