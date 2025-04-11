using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExportEngine.Compressor
{
    public class ZipCompressor
    {
        public string Compress(string filePath)
        {
            var zipPath = filePath + ".zip";
            using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
            archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
            return zipPath;
        }
    }
}
