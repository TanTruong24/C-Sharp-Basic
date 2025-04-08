using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NFind_Myself.Core
{
    class ExtractParams
    {
        public static readonly string[] ExtFiles = { ".txt", ".md", ".log" };
        public const string PatternKeyword = "\"{1,}(.*?[^\"\\s])\"{1,}";
        public static readonly string[] OptionFlags  = { "\\n", "\\v", "\\i", "\\c" };

        public static List<string> ExtractFileNames(string input)
        {
            var matches = Regex.Matches(input, @"[^\s\""]+\.(txt|md|log)");

            List<string> files = new List<string>();

            foreach (Match match in matches)
            {
                string[]? matchingFiles = getMatchingFiles(match.Value);

                if (matchingFiles == null)
                {
                    continue;
                }
                foreach (var file in matchingFiles)
                {
                    if (IsValidExtension(file))
                    {
                        files.Add(file);
                    }
                }
            }

            if (files.Count == 0)
            {
                throw new ArgumentException("Invalid file extention");
            }

            return files;
        }

        /**
         * Kiểm tra xem file có phần mở rộng hợp lệ hay không (không phân biệt chữ hoa chữ thường).
         */
        private static bool IsValidExtension(string file)
        {
            return ExtFiles.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase);
        }

        private static string[]? getMatchingFiles(string inputPath)
        {
            inputPath = Path.GetFullPath(inputPath);

            if (!inputPath.Contains("*") && !inputPath.Contains("?"))
            {
                return File.Exists(inputPath) ? new[] { inputPath } : null;
            }

            string? directory = Path.GetDirectoryName(inputPath);
            string? pattern = Path.GetFileName(inputPath);

            if (directory == null || pattern == null || !Directory.Exists(directory))
            {
                return null;
            }

            return Directory.GetFiles(directory, pattern);
        }


        public static string ExtractKeyword(string input)
        {
            var match = Regex.Match(input, PatternKeyword);
            if (!match.Success)
                throw new ArgumentException("Keyword must be in double quotes");

            return match.Groups[1].Value;
        }

        public static Dictionary<string, bool> ExtractOptions(string input)
        {
            var result = OptionFlags .ToDictionary(o => o, o => false);

            // Tách input thành token (giữ nguyên chuỗi trong dấu ngoặc kép)
            var tokens = Regex.Matches(input, "\"[^\"]+\"|\\S+")
                              .Select(m => m.Value)
                              .ToList();

            foreach (var token in tokens)
            {
                // Bỏ qua token nằm trong dấu nháy kép
                if (!token.StartsWith("\"") && result.ContainsKey(token))
                {
                    result[token] = true;
                }
            }

            return result;
        }
    }
}
