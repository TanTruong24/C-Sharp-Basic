using System.Collections.Generic;

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
                Dictionary<string, string[]> infoFiles = ReadFiles.ReaderManyFiles(ExtractParams.ExtractFileNames(input));

                List<string> options = ExtractParams.ExtractOptions(input);

                string keyword = ExtractParams.ExtractKeyword(input);

                Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();

                foreach (var item in infoFiles)
                {
                    results.Add(item.Key, ApplyOptions(options, keyword, item.Value));
                }

                foreach (var item in results)
                {
                    Console.WriteLine($"{item.Key}");
                    if (HasCount(options))
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public static List<string> ApplyOptions(List<string> opts, string keyword, string[] linesValue)
        {
            List<string> constainKeywords = new List<string>();
            List<string> exceptKeywords = new List<string>();
            List<string> res = new List<string>();

            bool hasLineNumer = HasLineNumber(opts);
            for (int i = 0; i < linesValue.Length; i++)
            {
                string lineNumber = hasLineNumer ? $"[{i + 1}]: " : "";
                if (ExistKeyword(opts, keyword, linesValue[i]))
                {
                    constainKeywords.Add($"{lineNumber}{linesValue[i]}");
                }
                else
                {
                    exceptKeywords.Add($"{lineNumber}{linesValue[i]}");
                }
            }

            res = HasExceptKeyword(opts) ? exceptKeywords : constainKeywords;
            return res;
        }

        private static bool ExistKeyword(List<string> opts, string keyword, string line)
        {
            return HasCaseInsensitive(opts)
                ? line.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                : line.Contains(keyword);
        }


        private static bool HasCaseInsensitive(List<string> opts)
        {
            var match = opts.FirstOrDefault(item => item.Contains("\\i"));
            return match != null;
        }

        private static bool HasLineNumber(List<string> opts)
        {
            var match = opts.FirstOrDefault(item => item.Contains("\\n"));
            return match != null;
        }

        private static bool HasExceptKeyword(List<string> opts)
        {
            var match = opts.FirstOrDefault(item => item.Contains("\\v"));
            return match != null;
        }

        private static bool HasCount(List<string> opts)
        {
            var match = opts.FirstOrDefault(item => item.Contains("\\c"));
            return match != null;
        }
    }
}
