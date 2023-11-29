
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace Integrity;

class Program
{
    static void Main(string[] args)
    {
        //Hashing();
        //Symmetric();
        Asymmetrisch();

    }

    private static void Asymmetrisch()
    {
        // Zender
        byte[] hash = CreateHash("Hello World");
        DSA dsa = DSA.Create();// new DSACryptoServiceProvider();
        string pubKey = dsa.ToXmlString(false);
        byte[] signature = dsa.SignData(hash, HashAlgorithmName.SHA1);


        // Ontvanger
        byte[] hash2 = CreateHash("Hello World");
        DSA dsa2 = DSA.Create();
        dsa2.FromXmlString(pubKey);
        bool isOk = dsa2.VerifyData(hash2, signature, HashAlgorithmName.SHA1);
        System.Console.WriteLine(isOk ? "Origineel" : "Mee gekloot");
    }

    private static void Symmetric()
    {
        // Zender
        HMACSHA1 hmac = new HMACSHA1();
        hmac.Key = Encoding.UTF8.GetBytes("Pa$$w0rd");
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Hello World"));
        System.Console.WriteLine(Convert.ToBase64String(hash));

        // Ontvanger
        HMACSHA1 hmac2 = new HMACSHA1();
        hmac2.Key = Encoding.UTF8.GetBytes("Pa$$w0rd");
        var hash2 = hmac2.ComputeHash(Encoding.UTF8.GetBytes("Hello World"));
        System.Console.WriteLine(Convert.ToBase64String(hash2));
    }

    private static void Hashing()
    {
        byte[] hash = CreateHash("Hello World");
        System.Console.WriteLine(Convert.ToBase64String(hash));
        Validate("Hello World", hash);
    }

    private static void Validate(string document, byte[] hash)
    {
        SHA1 alg = SHA1.Create();

        var h = alg.ComputeHash(Encoding.UTF8.GetBytes(document));
        System.Console.WriteLine(Convert.ToBase64String(h));
    }

    private static byte[] CreateHash(string document)
    {
        SHA1 alg = SHA1.Create();

        return alg.ComputeHash(Encoding.UTF8.GetBytes(document));
    }
}
