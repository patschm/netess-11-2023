namespace MyLib;

[My(Age = 42)]
public class Person : IIntroducable
{
    private int _age;
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0 && value < 123)
            {
                _age = value;
            }
        }
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public void Introduce()
    {
        System.Console.WriteLine($"I'm {FirstName} {LastName} ({Age})");
    }
}
