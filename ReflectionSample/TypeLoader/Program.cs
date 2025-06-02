
using MyMvc;
using System.Reflection;

namespace TypeLoader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: typeLoader.exe <dll file name>");
            }
            else
            {
                var filename = args[0];

                if (!File.Exists(filename))
                {
                    Console.WriteLine("File not found");
                    return;
                }

                Console.WriteLine($"Loading {filename}");

                var asm = InspectAssembly(filename);

                if (asm != null) 
                {
                    TryLoadObjectFromAssembly(asm);
                }
                
            }
        }

        private static void TryLoadObjectFromAssembly(Assembly asm)
        {
            var t = asm.GetType("MyAssembly.MyClass");

            if (t != null)
            {
                var contructorInfo = t.GetConstructor([typeof(int), typeof(int)]);

                if (contructorInfo != null)
                {
                    var myObj = contructorInfo.Invoke([1000, 2000]);

                    if (myObj != null)
                    {
                        var method = t.GetMethod("MyPublicVoidMethod", BindingFlags.Instance | BindingFlags.Public, []);

                        if (method != null)
                        {
                            var customAttributes = method.CustomAttributes;
                            var actionAttribute = customAttributes.FirstOrDefault(a => a.AttributeType.IsAssignableTo(typeof(ActionAttribute)));

                            var actionName = method.Name;

                            if (actionAttribute != null && actionAttribute.ConstructorArguments.Count > 0)
                            {
                                actionName = actionAttribute.ConstructorArguments[0].ToString();
                            }

                            Console.WriteLine($"actionName: {actionName}");

                            method.Invoke(myObj, []);
                        }

                        method = t.GetMethod("MyPrivateVoidMethod", BindingFlags.Instance | BindingFlags.NonPublic, [typeof(string)]);

                        if (method != null)
                        {
                            method.Invoke(myObj, ["TruongTheTan"]);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("MyClass type not found");
            }
        }

        private static Assembly? InspectAssembly(string filename)
        {
            var asm = Assembly.LoadFrom(filename);

            if (asm != null)
            {
                PrintTypeInfo(asm);
            }

            return asm;
        }

        private static void PrintTypeInfo(Assembly asm)
        {
            foreach(var type in asm.GetTypes())
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine(type.FullName);

                PrintMethodInfo(type);
                PrintFieldInfo(type);
            }
        }

        private static void PrintFieldInfo(Type type)
        {
            Console.WriteLine("FILES:-------------------");
            foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                Console.WriteLine($"Field: {(field.IsStatic ? "static" : string.Empty)} {field.FieldType} {field.Name}");
            }
        }

        private static void PrintMethodInfo(Type type)
        {
            Console.WriteLine("METHOD:------------");
            foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                if (method.DeclaringType == type)
                {
                    Console.WriteLine($"Method {method.Name}");
                }
            }
        }
    }
}
