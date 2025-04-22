using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Params
{
    public class FilterOrderCriteria
    {
        public string? OrderBy { get; set; }

        public string? OrderDirection { get; set; }

        public string? Query { get; set; }
    }
}
