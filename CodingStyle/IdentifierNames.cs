using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingStyle
{
    /**
     * 1 identifier (định danh) là tên gán cho các kiểu dữ liệu (class, interface, struct, delegate, enum), hoặc biến, hàm, namespace trong c#
     * 
     * Naming rule
     *      - Phải bắt đầu bằng một chữ cái hoặc _
     *      - có thể chứa: các ký tự chữ cái, định dạng, kết hợp, kết nối Unicode, chữ số thập phân
     *      - Dùng từ khóa làm định danh bằng cách thêm @ trước từ khóa
     *          + VD: @if
     *          => được gọi là verbatim identifiers - chủ yếu được dùng khi cần tương tác với định danh đến từ các ngôn ngữ lập trình khác.
     *          
     * Naming Conversation - quy ước đặt tên
     *      - PascalCase -> namespace, class, method, constant
     *      - camelCase -> cho biến, tham số, biến cục bộ
     *      + I + interface : IService
     *      + ...Atrribute : các class là Attribute nên kết thúc bằng "Attribute". VD: SerializableAttribute
     *      + Enum danh từ số ít hoặc số nhiều: Enum (non-flag) dùng N số ít (Color); Enum (flag) dùng N số nhiều (FileAttributes)
     *          * non-flag enum (enum thường) -> mỗi giá trị đại diện cho một lựa chọn duy nhất trong thể kết hợp. 
     *          ```
     *          enum Color
                {
                    Red,
                    Green,
                    Blue
                }
                ```
                * flag enum (dùng bit để kết hợp nhiều giá trị): 
                    - Dùng khi một biến cần chứa nhiều giá trị cùng lúc
                    - Các giá trị là lũy thừa của 2 để hỗ trợ bitwise OR (|).
                    - Phải đánh dấu [Flags] để sử dụng đúng cách.
                ```
                [Flags]
                enum FileAccess
                {
                    Read = 1,       // 0001
                    Write = 2,      // 0010
                    Execute = 4     // 0100
                }

                FileAccess access = FileAccess.Read | FileAccess.Write;
                ```

     *          
     *          
     *      + không dùng __ (2 dấu gạch chân liên tiếp) -> tên dành cho hệ thống
     *      + tên nên rõ nghĩa
     *      + ưu tiên rõ ràng hơn ngắn gọn
     *      + biến private dùng _ + camelCase: private int _totalCount
     *      + biến static nên bắt đầu bằng s_ : private static int s_totalCount
     *      + namespace nên có ý nghĩa và theo domain ngược: VD Com.MyCompany.Project.Module
     *          * là cách viết domain từ phải sang trái, tránh trùng lặp tên namespace giữa các tổ chức khác nhau.
     *          * Domain ngược = viết domain từ đuôi sang đầu, sau đó dùng làm prefix cho namespace.
     *          VD:
     *          Namespace theo domain ngược
     *          ```
     *          namespace Com.MyCompany.MyApp.Services.User
                {
                    public class UserService
                    {
                        // logic xử lý user
                    }
                }
                ```
                So sánh với cách viết không theo domain
                ```
                namespace Services.User
                {
                    public class UserService
                    {
                        // dễ trùng tên khi dùng chung code từ bên khác
                        // Dễ bị xung đột tên nếu bạn dùng thư viện từ bên thứ ba cũng có Services.User.
                    }
                }
                ```
     *      
     *      + tên assembly nên phản ánh mục địch chính : VD MyApp.DataAccess cho thư viện truy xuất dữ liệu
     *          * Là đơn vị triển khai của .NET, thường là một file .dll hoặc .exe. Mỗi project (library hoặc executable) khi build sẽ tạo ra 1 assembly.
     *          Ví dụ:
                    . MyApp.Core.dll → phần logic chính
                    . MyApp.Data.dll → phần truy cập dữ liệu
                    . MyApp.Web.dll → phần giao tiếp web hoặc API
                * Đặt tên assembly hợp lý để:
                    . Dễ nhận biết vai trò từng phần.
                    . Tự động map tên assembly với namespace.
                    . Dễ quản lý version và dependency giữa các module.
     * 
     */
    class IdentifierNames
    {
    }
}
