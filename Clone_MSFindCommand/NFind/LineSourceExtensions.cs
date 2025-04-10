using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFind
{
    public static class LineSourceExtensions
    {
        public static int CountMatching(this ILineSource source, Func<Line, bool> filter)
        {
            int count = 0;
            try
            {
                source.Open();
                Line? line;
                while ((line = source.ReadLine()) != null)
                {
                    if (filter(line)) count++;
                }
            }
            finally
            {
                source.Close();
            }
            return count;
        }
    }
}
