namespace Inlines;

delegate int MatDel(int x, int y);

internal class Program
{
    static void Main(string[] args)
    {
        int c = 100;
        MatDel m1 = Add;
        MatDel m2 = (a, b) => a + b + c;     

        Console.WriteLine(m2(4,8));

    }

    static int Add(int a, int b)
    {
            return a + b;
    }
}
