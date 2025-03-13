using System.Text;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // string intepolation
            string s1 = $"truong the tan {DateTime.Now} \n";
            string s2 = "cong nghe thong tin";

            // not peromance, request 3 memories all
            string s3 = s1 + s2;

            //using string builder
            StringBuilder sb = new StringBuilder();
            sb.Append(s1);
            sb.Append(s2);

            Console.WriteLine(sb.ToString());

            //--------------

            //nullable
            int? a = null;
            Console.WriteLine($"valid null: {a.HasValue}");


			int x, y = 10;
		}
    }
}
