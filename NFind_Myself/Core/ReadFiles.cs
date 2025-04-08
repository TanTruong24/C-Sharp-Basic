using NFind_Myself.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NFind_Myself.Core
{
    class ReadFiles
    {
        private static FileData ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File {path} not exsit.");
            }
            string[] lines = File.ReadAllLines(path);
            return new FileData
            {
                FilePath = path,
                Lines = lines,
                Encoding = Encoding.UTF8
            };
        }

        public static Dictionary<string, FileData> ReaderManyFiles(List<string> filePaths)
        {
            Dictionary<string, FileData> results = new Dictionary<string, FileData>();
            foreach (var path in filePaths)
            {
                try
                {
                    results.Add(path, ReadFile(path));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to read file {path}: {ex.Message}");
                }
            }
            return results;
        }
    }
}
