using System;

using MyLib;

namespace MySecond
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cs1 = new Class1();
            var result = cs1.SayHello();
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
