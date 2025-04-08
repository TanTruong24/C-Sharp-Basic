using System.Collections.Generic;
using NFind_Myself.Core;
using NFind_Myself.Extensions;
using NFind_Myself.Models;

namespace NFind_Myself
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine("Input command find:");

            string input = Console.ReadLine();

            Console.WriteLine();

            try
            {
                var (infoFiles, optionFlags, keyword) = ProcessInput(input);

                Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();

                foreach (var item in infoFiles)
                {
                    results.Add(item.Key, FilterByOptions(optionFlags, keyword, item.Value.Lines));
                }

                Print(results, optionFlags);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static (Dictionary<string, FileData> infoFiles, Dictionary<string, bool> optionFlags, string keyword) ProcessInput(string input)
        {
            var infoFiles = ReadFiles.ReaderManyFiles(ExtractParams.ExtractFileNames(input));
            var options = ExtractParams.ExtractOptions(input);
            var keyword = ExtractParams.ExtractKeyword(input);
            return (infoFiles, options, keyword);
        }

        private static void Print(Dictionary<string, List<string>> results, Dictionary<string, bool> opts)
        {
            foreach (var item in results)
            {
                Console.WriteLine($"{item.Key}");
                if (opts.GetFlag("\\c"))
                {
                    Console.WriteLine($"Count: {item.Value.Count()}");
                }
                else
                {
                    foreach (var line in item.Value)
                    {
                        Console.WriteLine($"{line}");
                    }
                }
            }
        }

        public static List<string> FilterByOptions(Dictionary<string, bool> opts, string keyword, string[] linesValue)
        {
            List<string> result = new();

            bool hasLineNumber = opts.GetFlag("\\n");
            bool ignoreCase = opts.GetFlag("\\i");
            bool exceptMode = opts.GetFlag("\\v");

            for (int i = 0; i < linesValue.Length; i++)
            {
                string line = linesValue[i];

                if (string.IsNullOrEmpty(line)) continue;

                // Lambda kiểm tra keyword có xuất hiện trong dòng hay không
                Func<string, bool> matchFunc = line => IsMatch(line, keyword, ignoreCase);

                // Lambda lọc final: nếu đang ở chế độ loại trừ thì phủ định matchFunc
                Func<string, bool> filter = line => exceptMode ? !matchFunc(line) : matchFunc(line);

                if (filter(line))
                {
                    string prefix = hasLineNumber ? $"[{i + 1}]: " : "";
                    result.Add($"{prefix}{line}");
                }
            }
            return result;
        }

        private static bool IsMatch(string line, string keyword, bool ignoreCase)
        {
            return ignoreCase
                ? line.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                : line.Contains(keyword);
        }
    }
}