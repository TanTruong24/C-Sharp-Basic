# Ghi chú OOP trong C# – Kế thừa

## KẾ THỪA (Inheritance) - derive types to create more specialized behavior

### Định nghĩa

"Inheritance enables you to create new classes that reuse, extend, and modify the behavior defined in other classes. The class whose members are inherited is called the base class. The class that inherits the members of the base class is called the derived class."

=> "Kế thừa cho phép bạn tạo các lớp mới sử dụng, mở rộng và sửa đổi hành vi được xác định trong các lớp khác. 

=> Lớp có các thành phần (members) được kế thừa gọi là lớp cơ sở (base class).  
Lớp kế thừa các thành phần từ lớp cơ sở được gọi là lớp dẫn xuất (derived class)."

### Tính chất

- C# và .NET chỉ hỗ trợ đơn kế thừa (single inheritance).
- Tuy nhiên, kế thừa là chuyển tiếp (transitive).

### Mức độ truy cập ảnh hưởng kế thừa

| Access Modifier | Mô tả |
|------------------|-------|
| private          | Thành phần chỉ truy cập được trong lớp cha và các lớp con được lồng bên trong |
| protected        | Thành phần chỉ được truy cập trong lớp con |
| internal         | Thành phần chỉ truy cập được từ cùng một assembly |
| public           | Thành phần được truy cập từ bất kỳ đâu |

### Ví dụ

```csharp
public class A
{
    private int _value = 10;

    public class B : A
    {
        public int GetValue()
        {
            return _value; // Hợp lệ vì B nằm trong A
        }
    }
}
```

### sealed class

- Mặc định, mọi lớp đều có thể được kế thừa.
- Dùng từ khóa `sealed` để ngăn không cho kế thừa.
- Nếu cố gắng kế thừa từ lớp sealed sẽ gây lỗi CS0509: `"cannot derive from sealed type <typeName>."`
- **sealed method** dùng để chặn override method: 
```csharp
public class B : A
{
    public sealed override void Print() { } // Không class nào kế thừa B override được nữa
}
```
---

## Abstract Class và Interface

### Abstract Class

- Không thể khởi tạo bằng từ khóa `new`.
- Có thể chứa:
  - Method abstract (không có thân)
  - Method thường (có thân)
- Nếu có method abstract, lớp phải được đánh dấu là `abstract`.
- Nếu lớp con không phải là abstract thì bắt buộc phải override tất cả method abstract.

### Interface

- Chỉ định bộ phương thức/properties cần implement.
- Không chứa logic (trừ khi dùng default implementation từ C# 8 trở lên).
- Một class có thể implement nhiều interface.
- Dùng để mô tả hành vi/khả năng, không phải quan hệ "is-a".

### Phân biệt class và interface

| Quan hệ              | Sử dụng     | Ví dụ                    |
|----------------------|-------------|--------------------------|
| "is-a"               | Class       | Mammal kế thừa Animal    |
| "có khả năng làm gì" | Interface   | Dog implement IRunnable  |

