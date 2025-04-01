using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    class Helper
    {
        public static double CalculateAverage(List<double> scores)
        {
            if (scores.Count == 0) return 0;

            return scores.Average();
        }
    }
}
