using DBExportEngine.Compressor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExportEngine
{
    public class ExportService
    {
        private readonly ExporterEngineFactory _exporterFactory = new();

        public string Export(
            List<Dictionary<string, object>> data,
            string format,
            string outputFile,
            string tableName,
            bool zip=false)
        {
            var exporter = _exporterFactory.GetExporter(format);

            EnsureDirectoryExists(outputFile);

            var tempPath = exporter.Export(data, outputFile, tableName);

            if (zip)
            {
                var zipper = new ZipCompressor();
                return zipper.Compress(tempPath);
            }

            return tempPath;
        }

        private void EnsureDirectoryExists(string outputPath)
        {
            var dir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
