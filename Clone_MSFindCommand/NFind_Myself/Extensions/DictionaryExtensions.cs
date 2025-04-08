using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Khai báo namespace chứa extension method.
// File sử dụng extension phải `using NFind_Myself.Extensions;` để truy cập được.
namespace NFind_Myself.Extensions
{
    public static class DictionaryExtensions
    {
        // Đây là cốt lõi của extension method:
        // - Từ khóa `this` đứng trước Dictionary<string, bool> báo cho C# biết:
        //   "Hàm này là mở rộng cho Dictionary<string, bool>"
        //
        // Khi đó, bạn có thể gọi opts.GetFlag(...) ở bất cứ đâu có using namespace phù hợp.
        //
        // Nội dung hàm: dùng TryGetValue để kiểm tra key tồn tại và lấy giá trị.
        // Nếu key không tồn tại, trả về false (mặc định).
        public static bool GetFlag(this Dictionary<string, bool> opts, string key)
        {
            return opts.TryGetValue(key, out var value) && value;
        }
    }
}