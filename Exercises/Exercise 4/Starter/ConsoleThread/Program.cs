
using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using System.Threading.Channels;

namespace ConsoleThread;

internal class Program
{
    static void Main(string[] args)
    {
        //CalculateSync();
        //CalculateASync();
        //Cancellen();
        //Fouten();
        //CalculateHip();
        //AndreRieuAsync();
        //HoeMeerMeerZielenHoeMeerVreugd();
        //ParkeerGarage();
        //ThreadSafety();

        //AppDomain.CurrentDomain.UnhandledException

        Console.WriteLine("Terug in Main");
        Console.ReadLine();
    }

    private static void ThreadSafety()
    {
       ConcurrentBag<int> list = new ConcurrentBag<int>();
        list.Add(1);
        //ConcurrentDictionary
           
    }

    private static void ParkeerGarage()
    {
        SemaphoreSlim slagboom = new SemaphoreSlim(20, 20);
        var rnd = new Random();
        Parallel.For(0, 100, idx => {
            Console.WriteLine($"Auto {idx} staat voor de slagboom");
            slagboom.Wait();
            Console.WriteLine($"Auto {idx} rijdt in");
            Task.Delay(rnd.Next(3000, 10000)).Wait();
            slagboom.Release();
            Console.WriteLine($"Auto {idx} rijdt eruit");
        });
    }

    static object stokje = new object();

    private static void HoeMeerMeerZielenHoeMeerVreugd()
    {
        int counter = 0;

        Parallel.For(0, 10, idx => {
            Console.WriteLine($"ThreadID = {Thread.CurrentThread.ManagedThreadId} ({idx})");
            lock (stokje)
            {
                int myvar = counter;
                Task.Delay(100).Wait();
                myvar++;
                counter = myvar;
                Console.WriteLine(counter);
            }
        });
    }

    private static async Task AndreRieuAsync()
    {
        int a = 0, b = 0;
        var zaklamp1 = new AutoResetEvent(false); //ManualResetEventSlim()
        var zaklamp2 = new AutoResetEvent(false);

        var t1 = Task.Run(() =>
        {
            Console.WriteLine("Taak 1 is bezig..");
            Task.Delay(3000).Wait();
            a =  10;
            //zaklamp1.Set();
        });

        var t2 = Task.Run(() =>
        {
            Console.WriteLine("Taak 2 is bezig...");
            Task.Delay(5000).Wait();
            b = 20;
           // zaklamp2.Set();
        });


        //WaitHandle.WaitAll(new WaitHandle[] { t1, t2});
        await Task.WhenAny(t1, t2);

        int result = a + b;
        Console.WriteLine(result);




    }

    private static async void CalculateHip()
    {
        //var t2 = Task.Run(() =>
        //{
        //    int result = LongAdd(2, 3);
        //    return result;
        //});

        //int result = await t2;
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        int result = await LongAddAsync(3, 4);
        Console.WriteLine("En verder");
        Console.WriteLine(result);
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

        result = await LongAddAsync(5, 6);
        Console.WriteLine("En verder");
        Console.WriteLine(result);
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

        try
        {         
            await Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Doe iets");
                Task.Delay(500).Wait();
                throw new Exception("Ooops!");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
    }

    private static void Fouten()
    {

            Task.Run(() =>
            {
                Console.WriteLine("Doe iets");
                Task.Delay(500).Wait();
                throw new Exception("Ooops!");
            }).ContinueWith(pt=> 
            { 
                if (pt.Exception != null)
                {
                    Console.WriteLine(pt.Status);
                    Console.WriteLine(pt.Exception?.InnerException?.Message);
                }
            });

    }

    private static void Cancellen()
    {
        CancellationTokenSource nikko = new CancellationTokenSource();
        CancellationToken bom = nikko.Token;

        var t1 = Task.Run(() => { 
            for(int i = 0; ;i++ )
            {
                if (bom.IsCancellationRequested)
                    return;
                Console.WriteLine($"Run {i}");
                Task.Delay(100).Wait();
                //Thread.SpinWait(100);
            }
        });


        Console.ReadLine();
        
        nikko.Cancel();
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

    static Task<int> LongAddAsync(int a, int b)
    {
        return Task.Run(() => LongAdd(a, b));
    }
}
