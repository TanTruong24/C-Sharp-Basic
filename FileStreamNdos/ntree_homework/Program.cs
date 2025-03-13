using System.Collections.ObjectModel;
using System.IO;

namespace ntree_homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("input path: ");
            string path = Console.ReadLine()?.Trim('"');

            var directoryInfo = new DirectoryInfo(path);


            Console.WriteLine(directoryInfo.Name);
            PrintDirectoryTree(directoryInfo);
        }

        public static void PrintDirectoryTree(DirectoryInfo directoryInfo, int level = 0)
        {
            var directories = directoryInfo.GetDirectories();
            if (directories.Length == 0) 
            {
                return;
            }
            try
            {
                foreach (var directory in directories)
                {
                    Console.WriteLine($"{new string('\t', level)}├──{directory.Name}");

                    PrintDirectoryTree(directory, level + 1);

                    foreach (var file in directory.GetFiles())
                    {
                        Console.WriteLine($"{new string('\t', level + 1)}├──{file.Name}");
                    }
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
