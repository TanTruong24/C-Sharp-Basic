using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace MyLoggingDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * một hệ thống logging luôn có 3 thành phần:
             * 
             * 1. factory chứa thông tin cấu hình logging, từ đó tạo các logger từ factory
             * 
             * 2. mỗi logger có 1 tên (category named), phân loại, nhóm dữ liệu, lọc log lại
             *      - Trong đa số trường hợp, tên log là tên lớp và namespace -> có thể truy dễ dàng, không bị trùng lặp
             *      
             * 3. logger provider -> nền bên dưới để ghi dữ liệu log (vd consoleProvider AddConsole) từ lúc tạo factory
             */
            using ILoggerFactory factory = LoggerFactory.Create(
                builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Debug);

                    // lọc chỉ hiển thị warning và information
                    // ghi đè lên lệnh setminumum level
                    builder.AddFilter((level) => level == LogLevel.Warning || level == LogLevel.Information);
                }
                );

            ILogger logger = factory.CreateLogger("Program");
            logger.LogInformation("Hello World! LogInformation is {Description}.", "fun");

            /*
             * Có nhiều log level trong 
             * - khi đặt cấu hình cho logging, mỗi logger có cấp độ riêng, chỉ những thông tin ở mức độ nghiêm trọng bằng hoặc hơn của logger đó mới in ra
             * - Trace < Debug < Information < Warning < Error < Critical < None
             * 
             * Log level:
             *  - 0 : Trace
             *      -> LogTrace() : Mức chi tiết nhất, thường bao gồm thông tin kỹ thuật rất sâu, biến nội bộ, callstack, không nên dùng ở production (nhạy cảm, quá tải)
             *      
             *  - 1 : Debug
             *      -> LogDebug() : Dành cho dev, follow logc thực thi, cẩn thận use production
             *      
             *  - 2 : Information
             *      -> LogInformation() : theo dõi business flow, dùng phổ biến production.  Ví dụ: “Order created successfully”
             *      
             *  - 3 : Warning
             *      -> LogWarning() : ghi nhận sự kiện bất thường, nhưng chua gây crash
             *      
             *  - 4 : Error
             *      -> LogError() : ghi khi có lỗi, exception xảy ra
             *      
             *  - 5 : Critical
             *      -> LogCritical() : Lỗi nghiêm trọng ảnh hưởng đến toàn bộ hệ thống hoặc yêu cầu phản ứng khẩn cấp. Ví dụ: "Không thể kết nối DB", "Disk full"
             *      
             *      
             * Dev: bật Debug trở xuống
             * Staging: Information trở xuống
             * Production: từ Information trở lên, và Trace/Debug nên tắt
             *      
             *      
             */

            /*
             * Nếu không config SetMinimumLevel || Filter ở factory thì những cấp độ > LogInfo mới được in ra console (debug và trace không in)
             * 
             * cấp độ ưu tiên Filter > SetMinimumLevel
             * 
             */

            logger.LogTrace("LogTrace");
            logger.LogDebug("LogDebug");
            logger.LogInformation("LogInformation");
            logger.LogWarning("LogWarning");
            logger.LogError("LogError");
            logger.LogCritical("LogCritical");

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("LogTrace:enabled");
            }

            string name = "Truong Tan";
            string role = "dev";

            // xác định dựa trên thứ tự truyền vào. Nên là tên gợi nhớ biên cần truyền vào
            logger.LogTrace("Hello World! LogTrace is {name} - {role}.", name, role);
        }
    }
}
