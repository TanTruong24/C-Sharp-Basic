using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFind_Myself.Models
{
    class FileData
    {
        public string FilePath { get; set; }
        public string[] Lines { get; set; }
        public Encoding Encoding { get; set; }

    }
}