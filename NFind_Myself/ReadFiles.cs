using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NFind_Myself
{
    class ReadFiles
    {
        private static bool EnsureFileExist(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"{filename} not exist!");
                return false;
            }
            return true;
        }

        public static string[]? Reader(string path)
        {
            return EnsureFileExist(path) ? File.ReadAllLines(path) : null;
        }

        public static Dictionary<string, string[]> ReaderManyFiles(List<string> files)
        {
            Dictionary<string, string[]> results = new Dictionary<string, string[]>();
            foreach (var file in files)
            {
                string[]? resultReader = Reader(file);
                if (resultReader != null)
                {
                    results.Add(file, resultReader);
                }
            }
            return results;
        }
    }
}
