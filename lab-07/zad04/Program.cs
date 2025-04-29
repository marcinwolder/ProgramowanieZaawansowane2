using System.Security.Cryptography;
using System.Text;

namespace zad04;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            throw new Exception($"Niepoprawna ilość argumentów: {args.Length}/4");
        }
        
        var inputPath = args[0];
        var outputPath = args[1];
        var password = args[2];
        if (!int.TryParse(args[3], out var operationType))
        {
            throw new Exception($"Parametr <typ-operacji> powinien być INT'em, otrzymano {args[3]}");
        }

        if (!File.Exists(inputPath))
        {
            throw new Exception($"Podany plik wejściowy {inputPath} nie istnieje");
        }

        var salt = "12345678"u8.ToArray();
        var initVector = "1234567812345678"u8.ToArray();
        const int liczbaIteracji = 2000;
        
        var k1 = new Rfc2898DeriveBytes(password, salt, liczbaIteracji, HashAlgorithmName.SHA256);
        
        var encAlg = Aes.Create();
        encAlg.Key = k1.GetBytes(16);
        encAlg.IV = initVector;
                
        var stream = new MemoryStream();
        
        switch (operationType)
        {
            case 0:
                var encrypt = new CryptoStream(stream, encAlg.CreateEncryptor(), CryptoStreamMode.Write);
                var text = File.ReadAllText(inputPath);
                var textBytes = Encoding.UTF8.GetBytes(text);
                
                encrypt.Write(textBytes, 0, textBytes.Length);
                encrypt.FlushFinalBlock();
                encrypt.Close();
                
                var encryptedData = stream.ToArray();
                File.WriteAllBytes(outputPath, encryptedData);
                break;
            case 1:
                var decrypt = new CryptoStream(stream, encAlg.CreateDecryptor(), CryptoStreamMode.Write);
                var encryptedBytes = File.ReadAllBytes(inputPath);
                
                decrypt.Write(encryptedBytes, 0, encryptedBytes.Length);
                decrypt.Flush();
                try
                {
                    decrypt.Close();
                }
                catch
                {
                    throw new Exception("Złe hasło");
                }
                
                var decryptedData = stream.ToArray();
                var decryptedText = Encoding.UTF8.GetString(decryptedData);
                Console.WriteLine(decryptedText);
                break;
            default:
                throw new Exception($"Parametr <typ-operacji> powinien mieć wartość 0 lub 1, otrzymano: {operationType}");
        }
    }
}

