namespace Vullis;

class Program
{
    static Unmanaged um1 = new Unmanaged();
    static Unmanaged um2 = new Unmanaged();

    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.Cyan;
        try
        {
            um1.Open();
        }
        finally
        {
            um1.Dispose();
        }
        um1 = null;

        //GC.Collect();
        //GC.WaitForPendingFinalizers();

        using(um2)
        {
            um2.Open();   
        }
        

        using (Unmanaged um3 = new Unmanaged())
        {
            um3.Open();
        }

        
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.ResetColor();
    }
}



class Unmanaged : IDisposable
{
    private static bool isOpen = false;
    private FileStream fs;

    public void Open()
    {
        if (!isOpen)
        {
            if (File.Exists("xxx.txt"))
            {
                fs = File.OpenRead("xxx.txt");
            }
            else
            {
                fs = File.Create("xxx.txt");
            }
            System.Console.WriteLine("Open resource");
            isOpen = true;
        }
        else
        {
            System.Console.WriteLine("Helaas! In gebruik");
        }
    }
    public void Close()
    {
        isOpen = false;
        System.Console.WriteLine("Closed resource");
    }

    protected void Ruimop(bool fromDispose)
    {
        Close();
        if (fromDispose)
        {
            fs.Dispose();
        }
    } 
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Ruimop(true);
    }

    ~Unmanaged()
    {
        Ruimop(false);
    }
}
