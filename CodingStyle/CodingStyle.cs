using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*******************************************************
* C# Coding Style Summary (Chuẩn Visual Studio/.NET)
* 
* - Dấu ngoặc: Allman style (mỗi dấu { và } nằm trên dòng riêng)
  Ví dụ:
    if (x > 0)
    {
        DoSomething();
    }

* - Thụt lề: 4 dấu cách, không dùng tab

* - using:
    - Đặt ở đầu file, ngoài namespace
    - System.* luôn đặt trước
    - Sắp xếp alphabet theo sau
  Ví dụ:
    using System;
    using Project.Core;

* - Field:
    - _camelCase cho private/internal
    - s_ cho static
    - t_ cho thread-static
    - Dùng readonly nếu có thể
    - Thứ tự: static readonly, không phải readonly static
  Ví dụ:
    private readonly int _counter;
    private static readonly string s_config;
    [ThreadStatic] private static int t_userId;

* - Public field:
    - Sử dụng PascalCase
    - Không dùng prefix (_ hoặc s_)
  Ví dụ:
    public int MaxRetries;

* - Luôn khai báo rõ visibility (public, private, internal...)
  Ví dụ:
    private string _name;
    public void DoWork();

* - Tránh dùng this. trừ khi cần phân biệt với tham số
  Ví dụ:
    this._id = id;

* - Chỉ dùng var khi kiểu ở bên phải rõ ràng (new hoặc cast)
  Ví dụ:
    var stream = new FileStream("path");
    var user = (User)result;
  Không nên:
    var name = GetName(); // không rõ kiểu

* - Dùng new() rút gọn chỉ khi kiểu được ghi rõ ở bên trái
  Ví dụ:
    FileStream stream = new(...);

* - Dùng từ khóa C# thay vì BCL types
  Ví dụ:
    Dùng: int, string, bool
    Tránh: Int32, String, Boolean

* - Dùng PascalCase cho:
    - constant
    - method
    - local function
  Ví dụ:
    private const int MaxUsers = 100;
    public void CreateUser() { }

* - Ưu tiên dùng nameof(...) thay cho string literal nếu phù hợp
  Ví dụ:
    throw new ArgumentNullException(nameof(parameter));
  Không nên:
    throw new ArgumentNullException("parameter");

* - Không có nhiều hơn 1 dòng trắng liên tiếp trong class

* - Nếu dùng ký tự Unicode không phải ASCII, dùng mã \uXXXX
  Ví dụ:
    string s = "\u00F4ng \u0111\u1ED3ng"; // ông đồng

* - Label (goto): thụt lề ít hơn 1 cấp so với block
  Ví dụ:
  Start:
      Console.WriteLine("Jumped here");

* - if đơn dòng:
    - Có thể bỏ dấu ngoặc nếu tất cả các nhánh cùng là 1 dòng
    - Không viết if gộp lệnh trên một dòng
  Không nên:
    if (x == null) throw new ArgumentNullException(nameof(x));
  Nên viết:
    if (x == null)
        throw new ArgumentNullException(nameof(x));

*******************************************************/

namespace CodingStyle
{
    // Interface: PascalCase, tiền tố I
    public interface IUserService
    {
        void CreateUser(string name);
    }

    // Enum thường: PascalCase, danh từ số ít
    public enum UserStatus
    {
        Active,
        Inactive
    }

    // Enum flag: PascalCase, danh từ số nhiều, có [Flags]
    [Flags]
    public enum FilePermissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4
    }

    // Lớp chính: PascalCase
    public class UserService : IUserService
    {
        // Field nội bộ: _camelCase, readonly nếu có thể
        private readonly string _serviceName;

        // Static field: s_, static readonly
        private static readonly string s_defaultRole = "User";

        // Thread-static field: t_
        [ThreadStatic]
        private static int t_userId;

        // Public const: PascalCase
        public const int MaxUserCount = 100;

        // Constructor
        public UserService()
        {
            _serviceName = "UserService";
        }

        // Method: PascalCase
        public void CreateUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid name", nameof(name));
            }

            Console.WriteLine($"Create new User: {name}");
        }
    }
}
