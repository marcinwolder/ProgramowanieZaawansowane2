using System.Security.Cryptography;

namespace zad01;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            throw new Exception("Brak parametru <typ-polecenia>!");
        }

        int type;
        try
        {
            type = int.Parse(args[0]);
        }
        catch
        {
            throw new Exception("Zły format parametru <typ-polecenia>! oczekiwano inta");
        }

        RSACryptoServiceProvider rsa = new();
        
        switch (type)
        {
            case 0:
                var publicKey = rsa.ToXmlString(false);
                File.WriteAllText("publicKey.dat", publicKey);
                var privateKey = rsa.ToXmlString(true);
                File.WriteAllText("privateKey.dat", privateKey);
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                throw new Exception("Zła wartość parametru <typ-polecenia>! Oczekiwano 0 <= x <= 2");
        }
    }
}