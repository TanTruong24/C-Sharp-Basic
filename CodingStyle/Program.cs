using CodingStyle.Services.User;

namespace CodingStyle
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            IUserService userService1 = new UserService();
            userService1.CreateUser("Alice");

            FilePermissions permissions1 = FilePermissions.Read | FilePermissions.Write;
            Console.WriteLine($"Permissions: {permissions1}");

            //===================================
            Console.WriteLine("=== Demo Coding Style ===");

            // var: chỉ khi RHS rõ kiểu (new)
            var userService = new UserService();

            userService.CreateUser("Alice");

            // Enum flag: dùng |
            FilePermissions permissions = FilePermissions.Read | FilePermissions.Write;
            Console.WriteLine($"Permissions: {permissions}");

            // goto label demo (indent -1)
            Start:
                Console.WriteLine("This is a label demo");

                // if đơn dòng: đồng bộ style
                if (permissions != FilePermissions.None)
                    Console.WriteLine("Access permission");

                // Dùng nameof thay vì string
                Console.WriteLine(nameof(userService));
        }
    }
}
