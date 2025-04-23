#define EXPERIMENTAL
#define THETAN

/**
 * Chỉ dẫn | Ý nghĩa
    #define | Định nghĩa một symbol

    #undef | Hủy định nghĩa một symbol

    #if, #elif, #else, #endif | Cấu trúc điều kiện cho đoạn mã

    #warning, #error | Sinh cảnh báo hoặc lỗi tại biên dịch

    #region, #endregion | Gom nhóm mã lại (hữu ích khi dùng IDE như Visual Studio)

    #pragma | Đưa ra hướng dẫn đặc biệt cho compiler
 */
namespace PreprocessorDirectives
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /**
             * Phần | Ý nghĩa
             * 
                #define DEBUG | Bật chế độ debug (thường set trong cấu hình project).

                #if DEBUG | In ra log hoặc thông tin debug khi được bật.

                #region | Gom nhóm phần Log() cho dễ quản lý.

                #warning | Cảnh báo khi dùng tính năng thử nghiệm.

                #error | Báo lỗi nếu chưa bật EXPERIMENTAL.

                #pragma warning disable/restore | Tắt cảnh báo biến chưa dùng.
             */

            Console.WriteLine("Hello, World!");

#if THETAN       
            Console.WriteLine("THETAN mode");
#elif DEBUG
            Console.WriteLine("DEBUG mode");
#else
            Console.WriteLine("RELEASE mode");
#endif

            #region Logging Feature
            Log("Program start with DEBUG mode....!");
            #endregion

            int unusedValue; // Biến khai báo mà không sử dụng -> có cảnh báo warning

#pragma warning disable CS0168 
            string debugString; // Biến khai báo mà không sử dụng -> không warning do đã disable
#pragma warning restore CS0168

#if EXPERIMENTAL
#warning "Experimental features"
            Console.WriteLine("Experimental features are running...");
#else
#error "Experimental feature is not enabled. Uncomment the line #define EXPERIMENTAL to use it."
#endif

            Console.WriteLine("Program end...");
        }
        static void Log(string message)
        {
#if DEBUG
            Console.WriteLine($"[LOG] {message}");
#endif
        }
    }
}
