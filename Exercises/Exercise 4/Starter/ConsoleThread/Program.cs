
using System.Threading.Channels;

namespace ConsoleThread;

internal class Program
{
    static void Main(string[] args)
    {
        //CalculateSync();
        CalculateASync();
        Console.WriteLine("Terug in Main");
        Console.ReadLine();
    }

    private static void CalculateSync()
    {
        int result = LongAdd(2, 3);
        Console.WriteLine($"Het resultaat is {result}");
        //Console.WriteLine("Het resultaat is {0}", result);
    }
    private static void CalculateASync()
    {
        //Func<int, int, int> del1 = LongAdd;

        //IAsyncResult ar = del1.BeginInvoke(3, 4, ax => {
        //    int result = del1.EndInvoke(ax);

        //    Console.WriteLine($"Het resultaat is {result}");
        //}, null);

        //Task t1 = new Task(() => {
        //    int result = LongAdd(2, 3);
        //    Console.WriteLine($"Het resultaat is {result}");
        //});
        //t1.Start();
        // Task.WaitAll(t1);

        var t2 = Task.Run(() =>
        {
            int result = LongAdd(2, 3);
            return result;          
        });
        t2.ContinueWith(pt => Console.WriteLine($"Het resultaat is {pt.Result}"));

        //Console.WriteLine($"Het resultaat is {t2.Result}");
        //t2.ContinueWith(pt => Console.WriteLine(pt.Status))
        //    .ContinueWith(pt=>Console.WriteLine("Doen we ook"));



    }

    static int LongAdd(int a, int b)
    {
        Task.Delay(5000).Wait();
        return a + b;
    }
}
