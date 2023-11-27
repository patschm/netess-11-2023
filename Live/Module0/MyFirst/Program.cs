using MyLib;

namespace MyFirst;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var cs1 = new Class1();

        var result = cs1.SayHello();
        Console.WriteLine(  result);
    }
}
