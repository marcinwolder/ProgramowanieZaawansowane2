using System.Security.Cryptography;
using System.Text;

namespace zad03;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            throw new Exception($"Niepoprawna ilość argumentów: {args.Length}/2");
        }

        var inputPath = args[0];
        var signaturePath = args[1];

        if (!File.Exists("privateKey.xml"))
        {
            throw new Exception("Brak klucza prywatnego 'privateKey.xml'");
        }
        var privateKey = File.ReadAllText("privateKey.xml");
        
        if (!File.Exists(inputPath))
        {
            throw new Exception($"Plik wejściowy {inputPath} nie istnieje!");
        }
        var text = File.ReadAllText(inputPath);
        var textBytes = Encoding.UTF8.GetBytes(text);
        var hash = SHA256.HashData(textBytes);

        RSACryptoServiceProvider rsa = new();
        rsa.FromXmlString(privateKey);
        
        if (!File.Exists(signaturePath))
        {
            RSAPKCS1SignatureFormatter formatter = new(rsa);
            formatter.SetHashAlgorithm("SHA256");
            
            var signedHash = formatter.CreateSignature(hash);
            File.WriteAllBytes(signaturePath, signedHash);
        }
        else
        {
            var signedHash = File.ReadAllBytes(signaturePath);
            RSAPKCS1SignatureDeformatter deformatter = new(rsa);
            deformatter.SetHashAlgorithm("SHA256");

            Console.WriteLine(deformatter.VerifySignature(hash, signedHash) ? "Podpis jest poprawny" : "Podpis jest błędny");
        }
    }
}

