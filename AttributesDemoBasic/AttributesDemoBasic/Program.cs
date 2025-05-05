
namespace AttributesDemoBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var myAttribute = (MyAttribute)Attribute.GetCustomAttribute(typeof(Class1).GetMethod("PrintHelloWorld"), typeof(MyAttribute));

            Console.WriteLine(myAttribute.fullname);
        }




    }
}
