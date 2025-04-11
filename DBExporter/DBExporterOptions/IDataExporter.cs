using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporterOptions
{
    public interface IDataExporter
    {
        string Export(object data);
    }
}
