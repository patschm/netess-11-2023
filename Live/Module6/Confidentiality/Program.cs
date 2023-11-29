
using System.Security.Cryptography;
using System.Text;

namespace Confidentiality;

class Program
{
    static void Main(string[] args)
    {
        //Asymmetric();
        Symmetrisch();
    }

    private static void Symmetrisch()
    {
        // Zender
        string msg = "Hello World";

        Aes alg = Aes.Create();
        //alg.Mode = CipherMode.ECB;
        (byte[] sharedKey, byte[] iv) cdata =(alg.Key, alg.IV);

        byte[] data;
        using MemoryStream mem = new MemoryStream();
        {
            using (CryptoStream cryp = new CryptoStream(mem, alg.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter writer = new StreamWriter(cryp))
            {
                writer.WriteLine(msg);
            }

            data = mem.ToArray();
        }
        System.Console.WriteLine(Encoding.UTF8.GetString(data));


        // Ontvanger
        Aes alg2 = Aes.Create();
        //alg2.Mode = CipherMode.ECB;
        alg2.Key = cdata.sharedKey;
        alg2.IV = cdata.iv;
        
        using (MemoryStream mem2 = new MemoryStream(data))
        {
            using (CryptoStream crypt = new CryptoStream(mem2, alg2.CreateDecryptor(), CryptoStreamMode.Read))
            {
                using (StreamReader rdr = new StreamReader(crypt))   
                {
                    var data2 = rdr.ReadToEnd();
                    System.Console.WriteLine(data2);
                }
            }
        }

    }

    private static void Asymmetric()
    {
        Ontvanger ont = new();
        Zender zend = new();

        byte[] cipher = zend.Zend(ont.PublicKey);
        System.Console.WriteLine(Encoding.UTF8.GetString(cipher));

        ont.Ontvangt(cipher);
    }
}

class Zender
{
    public byte[] Zend(string pubKey)
    {
        RSA rsa = RSA.Create();
        rsa.FromXmlString(pubKey);
        return rsa.Encrypt(Encoding.UTF8.GetBytes("Hello World"), RSAEncryptionPadding.Pkcs1);
    }
}

class Ontvanger
{
    private RSA _rsa = RSA.Create();

    public string PublicKey
    {
        get
        {
            return _rsa.ToXmlString(false);
        }
    }

    public void Ontvangt(byte[] cipher)
    {
        byte[] data = _rsa.Decrypt(cipher, RSAEncryptionPadding.Pkcs1);
        System.Console.WriteLine(Encoding.UTF8.GetString(data));
    }
}
