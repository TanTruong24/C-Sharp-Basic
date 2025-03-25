namespace Linq2Complex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var students = GetStudents();
            PrintStudents(students.Where(s=> s.YoB > 2002).OrderByDescending(s=>s.YoB));

            foreach (var studentName in students.OrderBy(s=>s.Name).Select(s=>s.Name))
            {
                Console.WriteLine(studentName);
            }

            var ns = GetStudents().Where(s=>s.YoB > 2004).FirstOrDefault();
            var ns2 = GetStudents().Where(s => s.YoB > 2004).Skip(1).Take(2);
            Print(ns);
            Console.WriteLine("==================");
            PrintStudents(ns2);
        }

        static void PrintStudents(IEnumerable<Student> students)
        {
            foreach (var student in students)
            {
                Print(student);
            }
        }

        private static void Print(Student student)
        {
            Console.WriteLine($"Name: {student.Name}, City: {student.City}, YoB: {student.YoB}");
        }


        static IEnumerable<Student> GetStudents()
        {
            return new Student[]
            {
                new Student()
                {
                    Name = "Tan",
                    City = "HCMC",
                    YoB = 2000
                },
                new Student()
                {
                    Name = "BTruong",
                    City = "HN",
                    YoB = 2004
                },
                new Student()
                {
                    Name = "ATThe",
                    City = "DN",
                    YoB = 2007
                },
                new Student()
                {
                    Name = "ATThe-1",
                    City = "DN",
                    YoB = 2006
                },
                new Student()
                {
                    Name = "ATThe-2",
                    City = "DN",
                    YoB = 2009
                },
                new Student()
                {
                    Name = "ATThe-5",
                    City = "DN",
                    YoB = 20010
                }
            };
        }
    }   
}
