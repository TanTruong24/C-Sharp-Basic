using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NFind_Myself
{
    class ExtractParams
    {
        public static string[] extFiles = { "txt", "md", "log" };

        public static string patternKeyword = "\"{1,}(.*?[^\"\\s])\"{1,}";

        public static string[] opts = { "\\n", "\\v", "\\i", "\\c" };

        public static List<string> ExtractFileNames(string input)
        {
            var matches = Regex.Matches(input, @"[^\s\""]+\.(txt|md|log)");

            List<string> files = new List<string>();
            foreach (Match match in matches)
            {
                if (extFiles.Contains(match.Value.Split('.')[1]))
                {
                    files.Add(match.Value);
                }
            }

            if (files.Count == 0)
            {
                throw new ArgumentException("Invalid file extention");
            }

            return files;
        }

        public static string ExtractKeyword(string input)
        {
            string keyword = Regex.Match(input, patternKeyword).Value;

            return keyword.Substring(1, keyword.Length - 2);
        }

        public static List<string> ExtractOptions(string input)
        {
            var extractOpts = new List<string>();

            // Tách input thành token (giữ nguyên chuỗi trong dấu ngoặc kép)
            var tokens = Regex.Matches(input, "\"[^\"]+\"|\\S+")
                              .Select(m => m.Value)
                              .ToList();

            foreach (var token in tokens)
            {
                // Bỏ qua token nằm trong dấu nháy kép
                if (!token.StartsWith("\"") && opts.Contains(token))
                {
                    extractOpts.Add(token);
                }
            }

            return extractOpts;
        }
    }
}
