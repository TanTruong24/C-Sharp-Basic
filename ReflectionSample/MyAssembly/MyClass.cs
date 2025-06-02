
using MyMvc;

namespace MyAssembly
{
    public class MyClass
    {
        public int myPublicInt = 10;

        private int myPrivateInt = 5;

        private static int myStaticInt = 1;

        public MyClass() { }

        public MyClass(int publicInt, int privateInt)
        {
            myPublicInt = publicInt;
            myPrivateInt = privateInt;
        }

        [Action("my-action")]
        public void MyPublicVoidMethod()
        {
            PrintValue();
        }

        private void PrintValue()
        {
            Console.WriteLine($"myPublicInt = {myPublicInt}, myPrivateInt = {myPrivateInt}");
        }

        private void MyPrivateVoidMethod(string s)
        {
            Console.WriteLine($"s = {s}, myPublicInt = {myPublicInt}, myPrivateInt = {myPrivateInt}");
        }

        public static int Add(int a, int b)
        {
            return a + b;
        }
    }
}
