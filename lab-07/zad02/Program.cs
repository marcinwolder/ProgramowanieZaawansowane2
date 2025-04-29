using System.Security.Cryptography;
using System.Text;

namespace zad02;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            throw new Exception($"Niepoprawna ilość argumentów: {args.Length}/3");
        }

        var input = args[0];
        var hash = args[1];
        
        string[] hashOptions = ["SHA256", "SHA512", "MD5"];
        if (!hashOptions.Contains(args[2]))
        {
            throw new Exception($"Błędna nazwa algorytmu haszującego: {args[2]}. " +
                                $"Prawidłowe opcje to: {string.Join(", ", hashOptions)}");
        }

        if (!File.Exists(input))
        {
            throw new Exception($"Brak pliku wejściowego {input}!");
        }
        
        Dictionary<string, Func<HashAlgorithm>> hashAlgorithmCreate = new(){
            { "SHA256", SHA256.Create },
            { "SHA512", SHA512.Create },
            { "MD5", MD5.Create }
        };
        
        var text = File.ReadAllText(input);
        var enc = Encoding.UTF8;
        
        var hashAlgorithm = hashAlgorithmCreate[args[2]]();
        var result = hashAlgorithm.ComputeHash(enc.GetBytes(text));
        var hashBuilder = new StringBuilder();
        foreach (var b in result)
        {
            hashBuilder.Append(b.ToString("x2"));
        }
        
        if (File.Exists(hash))
        {
            var savedHash = File.ReadAllText(hash);
            Console.WriteLine(savedHash != hashBuilder.ToString() ? "Hashe się różnią!" : "Hashe są zgodne!");
        }
        else
        {
            File.WriteAllText(hash, hashBuilder.ToString());
        }
    }
}