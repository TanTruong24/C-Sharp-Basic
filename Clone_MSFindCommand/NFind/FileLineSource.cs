using System.Runtime.CompilerServices;

namespace NFind
{
    internal class FileLineSource : ILineSource
    {
        private readonly string path;
        private readonly string filename;
        private readonly string pattern;
        private StreamReader? reader;
        private int lineNumber;

        public FileLineSource(string path, string filename)
        {
            this.path = path;
            this.filename = filename;
        }

        public string Name => filename;

        public void Close()
        {
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
        }

        public void Open()
        {
            if (reader != null)
            {
                throw new InvalidOperationException();
            }

            lineNumber = 0;
            reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
        }

        public Line? ReadLine()
        {
            if (reader == null)
            {
                throw new InvalidOperationException();
            }

            var s = reader.ReadLine();

            if (s == null)
            {
                return null;
            }
            else
            {
                return new Line() { LineNumber = ++lineNumber, Text = s };
            }
        }
    }
}