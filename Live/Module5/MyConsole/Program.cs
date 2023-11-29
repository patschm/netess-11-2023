
using System.Reflection;
using System.Runtime.InteropServices;

namespace MyConsole;


class Program
{
    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        //Person p = new Person {FirstName="Hennie", LastName="Peters", Age = 54};
        //p.Introduce();

        Assembly asm = Assembly.LoadFrom(@"D:\Dist\MyLib.dll");
        System.Console.WriteLine(asm.FullName);
        ShowContent(asm);
        CreateObject(asm);
        Console.ResetColor();
    }

    private static void CreateObject(Assembly asm)
    {  
        Type? ptype = asm.GetType("MyLib.Person");
        object? p1 = Activator.CreateInstance(ptype!);

        ptype?.GetProperty("Age")?.SetValue(p1, 42);
        ptype?.GetProperty("FirstName")?.SetValue(p1, "Kees");
        ptype?.GetProperty("LastName")?.SetValue(p1, "Jansma");

        ptype?.GetField("_age", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(p1, -100);

        ptype?.GetMethod("Introduce")?.Invoke(p1, new object[]{});

        dynamic? p2 = Activator.CreateInstance(ptype!);

        p2!.FirstName = "Peter";
        p2!.LastName = "Hendriks";
        p2!.Age = 34;
        p2!.Introduce();

        System.Console.WriteLine(p1?.GetType().GetCustomAttributes()?.FirstOrDefault());
    }

    private static void ShowContent(Assembly asm)
    {
        foreach(Type t in  asm.GetTypes())
        {
            System.Console.WriteLine(t.FullName);
            System.Console.WriteLine("Implements "+ t.GetInterfaces()?.FirstOrDefault()?.FullName);
           System.Console.WriteLine("Has Attribute " + t.GetCustomAttributes()?.FirstOrDefault());
   
            System.Console.WriteLine("Methods:");
            foreach(var mi in t.GetMethods())
            {
                
                System.Console.WriteLine("\t" + mi.Name);
                foreach(var p in mi.GetParameters())
                {
                    System.Console.WriteLine("\t\t" + p.Name);
                }
            }
            System.Console.WriteLine("Fields:");
            foreach(var mi in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                
                System.Console.WriteLine("\t" + mi.Name);
            }
            System.Console.WriteLine("Properties:");
            foreach(var mi in t.GetProperties())
            {

                System.Console.WriteLine("\t" + mi.Name);
            }
            System.Console.WriteLine("Constructors:");
            foreach(var mi in t.GetConstructors())
            {
                System.Console.WriteLine("\t" + mi.Name);
            }
            System.Console.WriteLine("============================================");
        }
    }
}
