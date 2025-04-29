using System.Security.Cryptography;
using System.Text;

namespace zad01;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
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
        string? publicKey;
        string? privateKey;
        string? input;
        string? output;
        UnicodeEncoding encoder = new();
        
        switch (type)
        {
            case 0:
                publicKey = rsa.ToXmlString(false);
                File.WriteAllText("publicKey.xml", publicKey);
                privateKey = rsa.ToXmlString(true);
                File.WriteAllText("privateKey.xml", privateKey);
                break;
            case 1:
                if (args.Length != 3)
                {
                    throw new Exception($"Zła liczba parametrów {args.Length}, " +
                                        $"oczekiwano 3. (<typ-polecenie> <plik-do-odczytu> <plik-do-zapisu>)");
                }
                input = args[1];
                output = args[2];

                if (!File.Exists("publicKey.xml"))
                {
                    throw new Exception("Brak wygenerowanych kluczy RSA. " +
                                        "Należy uruchomić polecenie z parametrem typ-polecenia=0");
                }

                publicKey = File.ReadAllText("publicKey.xml");

                if (!File.Exists(input))
                {
                    throw new Exception($"Brak pliku wejściowego: {input}!");
                }
                var encryptText = File.ReadAllText(input);
                var encryptTextBytes = encoder.GetBytes(encryptText);
                
                rsa.FromXmlString(publicKey);
                var encoded = rsa.Encrypt(encryptTextBytes, false);
                
                File.WriteAllBytes(output, encoded);
                break;
            case 2:
                if (args.Length != 3)
                {
                    throw new Exception($"Zła liczba parametrów {args.Length}, " +
                                        $"oczekiwano 3. (<typ-polecenie> <plik-do-odczytu> <plik-do-zapisu>)");
                }
                input = args[1];
                output = args[2];

                if (!File.Exists("privateKey.xml"))
                {
                    throw new Exception("Brak wygenerowanych kluczy RSA. " +
                                        "Należy uruchomić polecenie z parametrem typ-polecenia=0");
                }

                privateKey = File.ReadAllText("privateKey.xml");

                if (!File.Exists(input))
                {
                    throw new Exception($"Brak pliku wejściowego: {input}!");
                }
                var decryptTextBytes = File.ReadAllBytes(input);
                
                rsa.FromXmlString(privateKey);

                byte[]? decoded;
                try
                {
                    decoded = rsa.Decrypt(decryptTextBytes, false);
                }
                catch
                {
                    throw new Exception("Niepoprawny prywatny klucz RSA!");
                }
                
                File.WriteAllText(output, encoder.GetString(decoded));
                break;
            default:
                throw new Exception("Zła wartość parametru <typ-polecenia>! Oczekiwano 0 <= x <= 2");
        }
    }
}