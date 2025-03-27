using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public abstract  class Printer
    {
        // nếu trong name không string? thì warning cảnh báo ở hàm contructor không tham số.
        // nếu sử dụng như description thì phải khởi tạo giá trị mặc định trong constructor
        private string? name;
        private string description;

        public int age { get; set; } = 24;

        public required bool isRequired { get; set; }

        /**
         * luôn có một constructor, nếu không thấy thì contructor đó ngầm định, luôn tự động thực hiện đầu tiên khi khởi tạo
         */
        public Printer()
        {
            description = string.Empty;
            Console.WriteLine("-------PRINTER-----");
        }

        public Printer(string name, string note)
        {
            // do name trùng tên nên cần this, ngoài ra có thể không dùng this nếu khác tên, ngầm định hiểu
            this.name = name;
            description = note;
            Console.WriteLine($"-------PRINTER: {name} {description} {age}-----");
        }

        public void Print(string mess)
        {
            Console.WriteLine(mess);
        }

        // tạo bộ khung trong cấu trúc thừa kế
        public abstract void MyAbstractMethod();
    }
}
