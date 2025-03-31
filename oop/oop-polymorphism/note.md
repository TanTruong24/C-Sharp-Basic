
## ĐA HÌNH (Polymorphism)

- Lớp con có thể override thành phần trong lớp cha nếu: Method được đánh dấu là `virtual` hoặc `abstract`.

- `virtual` có thể override, `abstract` bắt buộc triển khai

-> tại thời điểm run-time, các obj của lớp con (lớp dẫn xuất - derived class) có thể được sử lý như các obj của lớp cha (lớp cơ sở - base class)

-> base class có thể định nghĩa và triển khai các pthuc ảo (virtual method) và các lớp con có thể ghi dè (override) -> cung cấp định nghĩa và triển khai riêng. Tại runtime, CLR (Common Language Runtime - mt thực thi của .net) sẽ xác định kiểu thực của obj và gọi phương thức phù hợp đã ghi đè trong lớp con.

**Ví dụ lỗi override vì thiếu `virtual`**

```csharp
public class A
{
    public void Method1()
    {
        // Do something.
    }
}

public class B : A
{
    public override void Method1() // Lỗi CS0506
    {
        // Do something else.
    }
}
```

**Ví dụ lỗi không override method abstract**

```csharp
public abstract class A
{
    public abstract void Method1();
}

public class B : A // Lỗi CS0534
{
    public void Method3()
    {
        // Do something.
    }
}
```

### Virtual members

