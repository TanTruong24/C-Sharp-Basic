namespace ncopy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var source = @"C:\Users\Admin\Downloads\CV_TruongTheTan.pdf";

            var dest = @"C:\Users\Admin\Documents\CV_TruongTheTan_copy.pdf";

            // if increase or decrease 1024, number of read file can more or less
            var buffer = new byte[1024];

            using var instream = File.OpenRead(source);

            using var outstream = File.OpenWrite(dest);

            int n = instream.Read(buffer, 0, buffer.Length);

            while (n > 0)
            {
                Console.WriteLine(n.ToString());

                outstream.Write(buffer, 0, n);

                n = instream.Read(buffer, 0, buffer.Length);
            }

            // use "using" instream, outsream auto close when command block end
            //instream.Close();
            //outstream.Close();
        }
    }
}
