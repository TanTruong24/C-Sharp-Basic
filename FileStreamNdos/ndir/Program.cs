﻿namespace ndir
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\";

            var dir = new DirectoryInfo(path);
            
            var directories = dir.GetDirectories();

            foreach (var d in directories) 
            { 
                Console.WriteLine($"{d.LastWriteTime:MM/dd/yyyy}");
            }

            var files = dir.GetFiles();
            foreach (var f in files)
            {
                Console.WriteLine($"{f.LastWriteTime:MM/dd/yyyy} {f.LastWriteTime:HH:mm} {f.Length} {f.Name}");
            }
        }
    }
}
