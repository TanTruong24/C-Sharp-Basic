namespace exceptionDemo
{
    internal class Program
    {
        // https://learn.microsoft.com/vi-vn/dotnet/csharp/fundamentals/exceptions/
        // https://learn.microsoft.com/en-us/dotnet/standard/exceptions/
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            try
            {
                int n = int.Parse(Console.ReadLine());

                int x = 10 / n;

                Console.WriteLine(x);

                throw new Exception("abc");
            }
            catch (DivideByZeroException ex) {
                Console.WriteLine(ex);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString()); 
            }
            finally {
                Console.WriteLine("Finally");
            }
        }
    }
}
