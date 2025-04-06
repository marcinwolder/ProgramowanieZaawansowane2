using System.Collections.Concurrent;

class Program
{
    static ConcurrentQueue<string> foundFiles = new();
    static bool searching = true;

    static void SearchFiles(string root, string keyword)
    {
        foreach (var file in Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories))
        {
            if (file.Contains(keyword))
                foundFiles.Enqueue(file);
        }
        searching = false;
    }

    static void Main()
    {
        string path = @"C:\";
        string keyword = "Marcin";

        new Thread(() => SearchFiles(path, keyword)).Start();

        while (searching || !foundFiles.IsEmpty)
        {
            if (foundFiles.TryDequeue(out var file))
                Console.WriteLine(file);
        }
    }
}