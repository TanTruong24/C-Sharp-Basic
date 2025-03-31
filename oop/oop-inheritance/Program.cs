/**
"The class whose members are inherited is called the base class. The class that inherits the members of the base class is called the derived class."
=> "Lớp có các thành phần (members) được kế thừa gọi là lớp cơ sở (base class). Lớp kế thừa các thành phần từ lớp cơ sở được gọi là lớp dẫn xuất (derived class)."

- C# và .NET chỉ hỗ trợ đơn kế thừa (single inheritance). Tuy nhiên, kế thừa là chuyển tiếp (transitive)

=============
Mức độ truy cập của một thành phần trong lớp cha có thể ảnh hưởng đối với các lớp dẫn xuất:
- private: các thành phần được truy cập trong các lớp con ĐƯỢC LỒNG trong lớp cha
    public class A
    {
        private int _value = 10;

        public class B : A
        {
            public int GetValue()
            {
                return _value;
            }
        }
    }
- protected: tp chỉ được truy cập trong lớp con
- internal: tp được chỉ được truy cập trong lớp con được đặt ở cùng project (cùng file .dll, assembly) của lớp cha
- public: tp được truy cập từ bất kì đâu
=================

Lớp con có thể override thành phần trong lớp cha với điều kiện method được đánh dấu là virtual (có thể override) hoặc và abstract (bắt buộc override)
Ví dụ về lỗi:
    public class A
    {
        public void Method1()
        {
            // Do something.
        }
    }

    public class B : A
    {
        public override void Method1() // Generates CS0506:  "<member> cannot override inherited member <member> because it is not marked virtual, abstract, or override."
        {
            // Do something else.
        }
    }
    --------------
    public abstract class A
    {
        public abstract void Method1();
    }

    public class B : A // Generates CS0534: "<class> does not implement inherited abstract member <member>", because class B provides no implementation for A.Method1.
    {
        public void Method3()
        {
            // Do something.
        }
    }
===============
Mặc định, bất kỳ lớp nào cũng có thể được kế thừa. 
Tuy nhiên, nếu không muốn lớp con kế thừa tiếp dùng từ khóa sealed.
Nếu cố gắng kế thừa từ một lớp sealed, trình biên dịch sẽ báo lỗi CS0509: "cannot derive from sealed type <typeName>."

==============
Abstract class và interface
    Abstract class:
    - Không thể khởi tạo bằng new
    - Có thể chứa method trừu tượng (không có thân hàm), Các phương thức thông thường (có thể có thân hàm).
    - Phải khai báo abstract nếu có method abstract
    - Class con không phải là abstract thì bắt buộc phải override tất cả các phương thức trừu tượng của lớp cha.

    Interface:
    - Chỉ định bộ phương thức/properties phải implement
    - Không chứa logic (trừ khi dùng default implementation - C# 8+)
    - Class có thể implement nhiều interface
    - Dùng mô tả khả năng/hành vi, không phải "is-a" như class

Ví dụ phân biệt:
    Mammal kế thừa Animal → là quan hệ "is a" → dùng class.
    Dog implement IRunnable → là quan hệ "có khả năng chạy" → dùng interface

*/
namespace oop_inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
