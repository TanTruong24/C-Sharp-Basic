namespace DBExportUtils
{
    public static class ExportMetadata
    {
        public static readonly HashSet<string> Formats = new(StringComparer.OrdinalIgnoreCase)
        {
            "csv",
            "sql",
            "json"
        };

        public static bool IsSupported(string format) => Formats.Contains(format);
    }
}
