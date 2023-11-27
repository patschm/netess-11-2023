
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Streaming;

class Program
{
    static void Main() 
    {
      // WriteStreamArchaic();
       //ReadStreamArchaic();

      // WriteStreamModern();
       //ReadStreamModern();

       //WriteStreamZipped();
       //ReadStreamZipped();

       //XmlWriterTest();
       //XmlReaderTest();

       //XmlSerializeWrite();
       XmlSerializeRead();
    }

    private static void XmlSerializeRead()
    {
        FileStream file =File.OpenRead(@"D:\data2.xml");
        var reader = XmlReader.Create(file);

        XmlSerializer ser = new XmlSerializer(typeof(Person));
        while(reader.ReadToFollowing("person"))
        {
            Person? p2 =  ser.Deserialize(reader) as Person;
            p2?.Introcuce();
        }
    }

    private static void XmlSerializeWrite()
    {
        var p1 = new Person { FirstName = "Jan", LastName = "Hendriks", Age=42};
        p1.Introcuce();

       // List<Person> people = new List<Person> {p1};

        FileStream file =File.Create(@"D:\data2.xml");
        XmlWriter writer = XmlWriter.Create(file);

        XmlSerializer ser = new XmlSerializer(typeof(Person));
        ser.Serialize(writer, p1);

    }

    private static void XmlReaderTest()
    {
        FileStream file =File.OpenRead(@"D:\data.xml");
        XmlReader rdr = XmlReader.Create(file);

        bool found = rdr.ReadToFollowing("person");
        System.Console.WriteLine(found);
        
        found = rdr.ReadToDescendant("first-name");
        System.Console.WriteLine(found);
        string data = rdr.ReadElementContentAsString();
        System.Console.WriteLine(data);

        found = rdr.ReadToNextSibling("last-name");
        data = rdr.ReadElementContentAsString();
        System.Console.WriteLine(data);

        found = rdr.ReadToNextSibling("age");
        int age = rdr.ReadElementContentAsInt();
        System.Console.WriteLine(age);

        
        
    }

    private static void XmlWriterTest()
    {
        var p1 = new Person { FirstName = "Jan", LastName = "Hendriks", Age=42};

        p1.Introcuce();

        FileStream file =File.Create(@"D:\data.xml");
        XmlWriter writer = XmlWriter.Create(file);
        writer.WriteStartElement("people");
            writer.WriteStartElement("person");
                writer.WriteStartElement("first-name");
                writer.WriteString(p1.FirstName);
                writer.WriteEndElement();
                
                writer.WriteStartElement("last-name");
                writer.WriteString(p1.LastName);
                writer.WriteEndElement();
                
                writer.WriteStartElement("age");
                writer.WriteString(p1.Age.ToString());
                writer.WriteEndElement();
            writer.WriteEndElement();
        writer.WriteEndElement();
        writer.Close();
    }

    private static void ReadStreamZipped()
    {
        FileStream file =File.OpenRead(@"D:\data.zip");
        GZipStream zip = new GZipStream(file, CompressionMode.Decompress);
        StreamReader rdr = new StreamReader(zip);
        
        string? line;
        while((line = rdr.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
        }
    }

    private static void WriteStreamZipped()
    {
        FileStream file =File.Create(@"D:\data.zip");
        GZipStream zip = new GZipStream(file, CompressionMode.Compress);
        StreamWriter writer = new StreamWriter(zip);
        for(int i = 0; i < 1000; i++)
        {
            string s = $"Hello World {i}";
            writer.WriteLine(s);
        }
        
        writer.Flush();
        //file.Write(new byte[]{42});
        writer.Close();
    }
    private static void ReadStreamModern()
    {
        FileStream file =File.OpenRead(@"D:\data2.txt");
        StreamReader reader = new StreamReader(file);
        string? line;
        while((line = reader.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
        }

    }

    private static void WriteStreamModern()
    {
        FileStream file =File.Create(@"D:\data2.txt");
        StreamWriter writer = new StreamWriter(file);
        for(int i = 0; i < 1000; i++)
        {
            string s = $"Hello World {i}";
            writer.WriteLine(s);
        }
        writer.Flush();
        writer.Close();
    }

    private static void ReadStreamArchaic()
    {
        FileStream file =File.OpenRead(@"D:\data.txt");

        byte[] buffer = new byte[4];
        int nrRead;// = file.Read(buffer, 0, buffer.Length);
        while((nrRead = file.Read(buffer, 0, buffer.Length)) > 0)
        {
            var result = Encoding.UTF8.GetString(buffer);
            System.Console.Write(result);
            Array.Clear(buffer);
        }
        
        // var result = Encoding.UTF8.GetString(buffer);
        // System.Console.Write(result);
        // nrRead = file.Read(buffer, 0, buffer.Length);
        // result = Encoding.UTF8.GetString(buffer);
        // System.Console.Write(result);
    }

    private static void WriteStreamArchaic()
    {
        FileStream file =File.Create(@"D:\data.txt");
        for(int i = 0; i < 1000; i++)
        {
            string s = $"Hello World {i}\r\n";
            byte[] buffer = Encoding.UTF8.GetBytes(s);
            file.Write(buffer, 0, buffer.Length);
        }
        //file.Flush();
        file.Close();
    }
}
