
using System.Xml;
using System.Xml.Serialization;

namespace RssReader;

class Program
{
    static async Task Main(string[] args)
    {
        var stream = await CallRssAsync("https://nu.nl/rss");
        foreach(var item in ParseData(stream))
        {
            ShowItem(item);
        }

        foreach(var nr in GetNumbers().Take(2))
        {
            System.Console.WriteLine(nr);
        }
    }

    static IEnumerable<int> GetNumbers()
    {
        System.Console.WriteLine("Nummer 1");
        yield return 1;
        System.Console.WriteLine("Nummer 2");
        yield return 2;
        System.Console.WriteLine("Nummer 3");
        yield return 3;
    }

    private static async Task<Stream> CallRssAsync(string url)
    {
        var client = new HttpClient { BaseAddress= new Uri(url) };
        var stream = await client.GetStreamAsync("");
        return stream;
    }

    private static void ShowItem(Item item)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine($"{string.Join(',', item.Categories!), -80}");
        Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine($"{item.Title}");
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.WriteLine(item.Description);
        Console.ResetColor();
        Console.WriteLine();
    }

    private static IEnumerable<Item> ParseData(Stream stream)
    {
        var xml = new XmlSerializer(typeof(Item));
        var rdr = XmlReader.Create(stream);
        //List<Item> items = new List<Item>();
        while(rdr.ReadToFollowing("item"))
        {
            var item = xml.Deserialize(rdr) as Item;
            if (item != null) 
                yield return item;
                //items.Add(item);
        }
        //return items;
    }
}
