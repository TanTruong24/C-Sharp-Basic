using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    /**
     *  Lớp AccessCounter sử dụng Singleton Pattern theo kiểu Eager Initialization
     *  => Instance được khởi tạo ngay khi chương trình load class
     */
    internal class AccessCounter
    {
        private int counter = 0;

        // Instance duy nhất của lớp AccessCounter, được khởi tạo NGAY LẬP TỨC khi chương trình chạy
        // Ưu:      - Gọn gàng, dễ hiểu, không cần xử lý đồng bộ (thread-safe mặc định với static)
        // Nhược:   - Luôn tạo instance dù không dùng → có thể lãng phí tài nguyên nếu không sử dụng đến
        private static AccessCounter accessCounter = new();

        public int Counter => counter;

        public int IncCounter()
        {
            counter++;
            return counter;
        }

        // Phương thức static trả về instance duy nhất (singleton) của lớp AccessCounter
        public static AccessCounter GetInstance()
        {
            return accessCounter;
        }
    }
}
