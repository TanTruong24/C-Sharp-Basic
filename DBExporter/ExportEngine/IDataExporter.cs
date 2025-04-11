using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportEngine
{
    public interface IDataExporter
    {
        string Export(List<Dictionary<string, object>> data, string outputPath);
        string FileExtension { get; }
    }
}
