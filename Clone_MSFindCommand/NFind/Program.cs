using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace NFind
{
    public class Program
    {
        public const string PatternKeyword = "\"{1,}(.*?[^\"\\s])\"{1,}";

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("FIND: Parameter format not correct");
                return;
            }

            var findOptions = BuildOptions(args);

            if (findOptions.HelpMode)
            {
                PrintHelp();
                return ;
            }

            var sources = LineSourceFactory.CreateInstance(findOptions.Path, findOptions.SkipOffLineFiles);

            foreach (var source in sources)
            {
                ProcessSource(source, findOptions);
            }
        } 

        private static void ProcessSource(ILineSource source, FindOptions findOptions)
        {
            var stringComparison = findOptions.IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            Func<Line, bool> filter = line =>
            {
                bool contains = line.Text.Contains(findOptions.StringToFind, stringComparison);
                return findOptions.FindDontConstain ? !contains : contains;
            };

            

            if (findOptions.CountMode)
            {
                Console.Write($"-------------{source.Name.ToUpper()}");
                Console.WriteLine($": {source.CountMatching(filter)}");
            }
            else
            {
                Console.WriteLine($"-------------{source.Name.ToUpper()}");
                try
                {
                    source = new FilteredLineSource(source, filter);
                    source.Open();

                    Line? line;
                    while ((line = source.ReadLine()) != null)
                    {
                        Print(line, findOptions.ShowLineNumber);
                    }
                }
                finally
                {
                    source.Close();
                }
            }
        }

        private static void Print(Line line, bool printLineNumber)
        {
            if (printLineNumber)
            {
                Console.WriteLine($"[{line.LineNumber}] {line.Text}");
            }
            else
            {
                Console.WriteLine($"{line.Text}");
            }
            
        }

        public static FindOptions BuildOptions(string[] args) 
        {
            var options = new FindOptions();

            foreach (var arg in args) 
            {
                var lower = arg.ToLowerInvariant();
                if (lower == "/v")
                    options.FindDontConstain = true;
                else if (lower == "/c")
                    options.CountMode = true;
                else if (lower == "/n")
                    options.ShowLineNumber = true;
                else if (lower == "/i")
                    options.IsCaseSensitive = false;
                else if (lower == "off" || lower == "offline")
                    options.SkipOffLineFiles = false;
                else if (lower == "/?" || lower == "?")
                    options.HelpMode = true;
                else
                {
                    var match = Regex.Match(arg, PatternKeyword);

                    if (match.Success && string.IsNullOrEmpty(options.StringToFind))
                    {
                        options.StringToFind = match.Groups[1].Value;
                    }
                    else if (string.IsNullOrEmpty(options.Path))
                    {
                        options.Path = arg;
                    }
                    else
                    {
                        Console.WriteLine("FIND: Parameter format not correct");
                        throw new ArgumentException(arg);
                    }
                }
            }
            if (string.IsNullOrEmpty(options.StringToFind) && !options.HelpMode)
            {
                Console.WriteLine("FIND: Missing search string in double quotes.");
                throw new ArgumentException("Missing keyword in format: \"your text\"");
            }

            return options;
        }

        private static void PrintHelp()
        {
            Console.WriteLine(
            @"FIND [/V] [/C] [/N] [/I] [/OFF[LINE]] ""string"" [[drive:][path]filename[ ...]]

              /V         Displays all lines NOT containing the specified string.
              /C         Displays only the count of lines containing the string.
              /N         Displays line numbers with the displayed lines.
              /I         Ignores the case of characters when searching for the string.
              /OFF[LINE] Do not skip files with offline attribute set.
              ""string""   Specifies the text string to find.
              [drive:][path]filename
                         Specifies a file or files to search."
            );
        }
    }
}
