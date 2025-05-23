﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    public class Student
    {
        /**
         * - static dùng để chia sẻ dữ liệu hoặc phương thức chung giữa mọi đối tượng của lớp
         * - không cần tạo object để gọi các thành viên static
         * - static không thể truy cập non-static, vì nó không biết object cụ thể nào
         * - const cũng giống như static về mặt hoạt động: tồn tại 1 lần duy nhất, không thay đổi
         * - static method không thể override vì nó không fawns với object nào, mà gắn với class
         * - không có biến cục bộ static trong c#
         */

        // static fields
        public static int totalStudents { get; private set; }

        private static Dictionary<string, double> allScoreStudents = new Dictionary<string, double>();

        // Const: không đổi, thuộc class
        public const string schoolName = "Cam My High School";

        public string ID { get; private set; }

        // Instance fields
        // string là kiểu reference type -> nullable (string?) or not null (string)
        // nếu khai báo "public string Name" bị warning vì Name không được phép null, nhưng không biết giá trị mặc định nào gán cho Name -> warning
        public string Name { get; set; }

        public double Score { get; private set; }

        // Static constructor: chỉ chạy 1 lần duy nhất
        static Student()
        {
            Console.WriteLine("Students system....");
        }

        private Student(string id, string name, double score)
        {
            ID = id;
            Name = name;
            Score = score;

            totalStudents++;
            allScoreStudents.Add(id, score);
        }

        public static Student create(string id, string name, double score)
        {
            if (allScoreStudents.ContainsKey(id))
            {
                throw new ArgumentException($"ID {id} existed");
            }

            return new Student(id, name, score);
        }

        public static Dictionary<string, double> getAllScore()
        {
            return allScoreStudents;
        }

        public static void ShowTotalStudents()
        {
            Console.WriteLine($"Total students: {totalStudents}");
        }

        public static void ShowAllScore()
        {
            foreach (var item in allScoreStudents)
            {
                Console.WriteLine($"Student ID: {item.Key} - Score: {item.Value}");
            }

        }
    }
}
