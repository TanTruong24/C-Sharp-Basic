using System;

namespace CodingStyle.Services.User
{
    // Interface: bắt đầu bằng I
    public interface IUserService
    {
        void CreateUser(string userName);
    }

    // Attribute: kết thúc bằng Attribute
    public class RequiredAttribute : Attribute
    {
    }

    // Non-flag enum: dùng danh từ số ít
    public enum UserStatus
    {
        Active,
        Inactive,
        Banned
    }

    // Flag enum: dùng danh từ số nhiều và có [Flags]
    [Flags]
    public enum FilePermissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4
    }

    // Class name PascalCase
    public class UserService : IUserService
    {
        // Static field: bắt đầu bằng s_
        private static int s_totalUsers = 0;

        // Private field: bắt đầu bằng _ và camelCase
        private readonly string _serviceName;

        // Constant: PascalCase
        public const int MaxUsers = 1000;

        // Constructor: tên class PascalCase
        public UserService()
        {
            _serviceName = "UserService";
        }

        // Method name: PascalCase
        public void CreateUser(string userName)
        {
            // Parameter: camelCase
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("userName is required", nameof(userName));
            }

            Console.WriteLine($"Creating user: {userName}");

            // Local variable: camelCase
            int userId = GenerateUserId();

            Console.WriteLine($"User created with ID: {userId}");

            s_totalUsers++;
        }

        // Private method: PascalCase
        private int GenerateUserId()
        {
            return new Random().Next(1000, 9999);
        }
    }
}
