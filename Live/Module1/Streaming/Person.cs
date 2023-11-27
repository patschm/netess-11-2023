using System.Xml.Serialization;

namespace Streaming;

[XmlRoot("person")]
public class Person
{
    private int _age = 55;

    [XmlAttribute("age")]
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    [XmlElement("first-name")]
    public string? FirstName { get; set; }

    [XmlElement("last-name")]
    public string? LastName { get; set; }

    public void Introcuce()
    {
        System.Console.WriteLine($"Hello, I'm {FirstName} {LastName} ({Age})");
    }
    
}
