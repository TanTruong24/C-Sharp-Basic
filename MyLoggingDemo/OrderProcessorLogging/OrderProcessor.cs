using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessorLogging
{
    public class OrderProcessor
    {
        private readonly ILogger<OrderProcessor> _logger;

        public OrderProcessor(ILogger<OrderProcessor> logger)
        {
            _logger = logger;
        }

        public async Task ProcessOrdersAsync()
        {
            _logger.LogInformation("Start order processing...");

            var tasks = new List<Task>();
            for (int i = 1; i <= 5; i++)
            {
                int orderId = i;
                tasks.Add(Task.Run(() => ProcessSingleOrder(orderId)));
            }

            await Task.WhenAll(tasks);
            _logger.LogInformation("Finished processing all orders.");
        }

        private async Task ProcessSingleOrder(int orderId)
        {
            try
            {
                _logger.LogDebug("Processing order {OrderId}", orderId);

                await Task.Delay(Random.Shared.Next(300, 1000));

                if (Random.Shared.Next(1,5) == 1)
                {
                    throw new InvalidOperationException($"Order {orderId} failed randomly");
                }

                _logger.LogInformation("Order {OrderId} processed successfully", orderId);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while processing order {OrderId}", orderId);
            }
        }

        /*
         * Ví dụ ouput log:
         * 
         * [15:55:26 INF] Start order processing...
            [15:55:26 DBG] Processing order 1
            [15:55:26 DBG] Processing order 5
            [15:55:26 DBG] Processing order 3
            [15:55:26 DBG] Processing order 4
            [15:55:26 DBG] Processing order 2
            [15:55:27 INF] Order 4 processed successfully
            [15:55:27 INF] Order 3 processed successfully
            [15:55:27 INF] Order 2 processed successfully
            [15:55:27 INF] Order 1 processed successfully
            [15:55:27 ERR] Error while processing order 5
            System.InvalidOperationException: Order 5 failed randomly
               at OrderProcessorLogging.OrderProcessor.ProcessSingleOrder(Int32 orderId) in E:\eng\csharp\MyLoggingDemo\OrderProcessorLogging\OrderProcessor.cs:line 44
            [15:55:27 INF] Finished processing all orders.
         * 
         * Giải thích:
         * 
         * 1. Các order không xử lý tuần tự
            Các order 1→5 không chạy lần lượt, mà chạy song song (Task.Run()).
            Tốc độ xong của mỗi đơn hàng phụ thuộc vào random delay (giả lập thời gian xử lý).
            ⮕ Nên kết quả thành công/thất bại cũng xảy ra không theo thứ tự.
         *
         * 3. Tất cả task đều await Task.WhenAll(tasks)
            Bạn đợi tất cả các task (xử lý đơn) hoàn thành (hoặc lỗi) trước khi log "Finished processing all orders.".
            Lỗi trong từng task đã được bắt riêng (try-catch trong mỗi Task.Run) nên Task.WhenAll không crash chương trình.
         *
         * Cách bạn đang làm chính là cách rất phổ biến trong các hệ thống production:
            Dispatch nhiều việc song song
            Handle lỗi riêng biệt từng công việc.
            Await toàn bộ để biết khi nào toàn bộ job xong.
         *
         */
    }
}
