using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    /**
     * Lớp DatabaseConnection sử dụng Singleton Pattern với Lazy Initialization và hỗ trợ đa luồng (thread-safe)
     * 
     * Lazy Initialization
     *      - Lazy init (khởi tạo lười biếng) là một kỹ thuật trong lập trình dùng để trì hoãn việc khởi tạo một đối tượng cho đến khi nó thực sự cần thiết
     * 
     */
    internal class DatabaseConnection
    {
        // Biến static chứa instance duy nhất của lớp, khởi tạo lần đầu khi cần (lazy)
        private static DatabaseConnection? instance = null;

        // Đối tượng dùng để khóa (lock) khi nhiều thread cùng truy cập
        private static object instanceLock = new object();

        // Constructor private để ngăn tạo từ bên ngoài
        private DatabaseConnection() { }

        // Phương thức public static để lấy instance duy nhất (singleton)
        public static DatabaseConnection GetInstance()
        {
            // Khóa để đảm bảo chỉ một thread được tạo instance tại một thời điểm
            lock (instanceLock) 
            {
                if (instance == null)
                {
                    instance = new DatabaseConnection();
                    instance.connect();
                }
            }
            return instance;
        }

        /**
         * Sử dụng Lazy có hỡ trọ trong C#
         * 
            // Singleton sử dụng Lazy<T>: chỉ khởi tạo khi truy cập .Value, thread-safe mặc định
            private static readonly Lazy<DatabaseConnection> instance =
                new(() =>
                {
                    var db = new DatabaseConnection();
                    db.connect(); // Gọi connect() khi tạo
                    return db;
                });

            // Thuộc tính public để truy cập instance duy nhất
            public static DatabaseConnection Instance => instance.Value;

            // Constructor private để ngăn tạo từ bên ngoài
            private DatabaseConnection() { }
         */

        private void connect()
        {
            Console.WriteLine("ConnectDB SQL");
        }

        public void Query(string sql)
        {
            Console.WriteLine($"Run SQL: {sql}");
        }
    }
}
