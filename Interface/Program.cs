namespace Interface
{
    internal class Program
    {
        /**
         * - Một interface chứa các định nghĩa cho một nhóm chức năng liên quan mà một non-abstract và struct phải triển khai
         *      => interface là một bản hợp đồng (contract) mô tả những gì class cần thực hiện, chứ không phải mô tả cách thực hiện (phần cài đặt)
         *      Vì vậy:
         *          + Các thành viên public (mặc định modifier nếu không khai báo gì), protected, internal trong interface mặc định là không có thân hàm. 
         *          + private không thể triển khai từ bên ngoài interface -> chỉ dùng trong nội bộ nên phải có hiện thực (defaul implementation)
         *      
         * - C# không đa kế thừa, nhưng class có thể implement nhiều interface. Mọi method trong interface đều mặc định "Có thể override" (khác virtual như ghi đè class)
         * 
         * - Interface có thể định nghĩa các phương thức static, và các phương thức này bắt buộc phải có phần hiện thực.
         *      + từ C# 8+ có static method
         *      + lý do: Interface không có state (instance field) nên static method trong interfac không thể được override.
         *              Do đó, nó không thể "chỉ khai báo" mà không cài đặt – vì không ai sẽ "implement" nó, nó cần "tự implement" 
         * 
         * - interface cũng có thể định nghĩa một phần hiện thực mặc định (default implementation) cho các thành viên
         *      + default implementation có từ C# 8+. Cho phép viết logic mặc định trong interface, thay vì buộc all các implement phải tự định nghĩa lại
         *      ```
         *      interface IMyInterface
                {
                    void SayHi()
                    {
                        Console.WriteLine("Hi from interface!");
                    }
                }
                ```
         *      
         * - Interface không được phép khai báo dữ liệu instance như các fields, thuộc tính tự động thực thi, hay các sự kiện dạng thuộc tính
         *      + trong C# (.net nói chung) một interface là:
         *          - Một tập hợp các method signatures (giống "hợp đồng")
         *          - Khi biên dịch, interface sẽ trở thành metadata (dạng abstract table)
         *          - không có bộ nhớ riêng, không lưu trữ dữ liệu gì cả
         *          -> interface chỉ mô trả, chứ không giữ bất kỳ dữ liệu nào
         *      + So sánh với class (có state)
         *          -> Khi tạo instance của Person, một vùng nhớ sẽ được cấp phát để chứa field Name. Bộ nhớ này của từng object, tức instance-level state
         *          ```
         *          class Person
                    {
                        public string Name;  // ⬅️ Đây là state (field)
                    }
                    ```

                    -> IPerson said: "Ai triển khai tôi thì phải có Name" nhưng bản thân interface không chứa gì cả. Không có vùng nhớ nào để chứa dữ liệu (state)
                    ```
                    interface IPerson
                    {
                        string Name { get; set; }  // ⬅️ chỉ là khai báo – không cấp phát bộ nhớ
                    }
                    ```
         *  - interface có thể kế thừa từ một hoặc nhiều interface khác. Một interface có thể kế thừa các thành viên từ các interface cơ sở mà nó kế thừa. 
         *  Lớp triển khai interface đó phải cung cấp triển khai cho tất cả các thành viên của interface đó và các interface cơ sở.
         *  
         *  - Nâng cao:
         *      + Indexers trong Interface, điều này cho phép các lớp triển khai có thể sử dụng cú pháp chỉ mục để truy xuất phần tử
         *      ```
         *      public interface IKeyValueStore
                {
                    string this[string key] { get; set; }
                }
                ```
         * 
         *      + abstract class vs interface
         *          # abstract class: 
         *              là class cha trong một cây object, tạo bộ khung, tạo các lớp con, dùng lại các chức năng trong abstract, trong đa số case bao gồm cả method đã implement và chưa implement
         *              
         *          
         *          # interface
         *              là bản cam kết những gì mà một đối tượng cần phải có
         */
        static void Main(string[] args)
        {
            Console.WriteLine("========Indexer=========");

            // Khởi tạo một ma trận 3x3
            Matrix matrix = new Matrix(3, 3);

            // Gán giá trị cho các phần tử trong ma trận qua indexer
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 4;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            matrix[2, 0] = 7;
            matrix[2, 1] = 8;
            matrix[2, 2] = 9;

            // Hiển thị ma trận
            Console.WriteLine("Matrix:");
            matrix.DisplayMatrix();

            // Truy xuất một phần tử cụ thể từ ma trận
            Console.WriteLine("\nElement at position (1,1): " + matrix[1, 1]);  // Output: 5
        }
    }
}
