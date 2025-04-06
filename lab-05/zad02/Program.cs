class Program
{
    static void MonitorDirectory(string path)
    {
        var watcher = new FileSystemWatcher(path);
        watcher.Created += (s, e) => Console.WriteLine($"Dodano plik {e.Name}");
        watcher.Deleted += (s, e) => Console.WriteLine($"Usunięto plik {e.Name}");
        watcher.EnableRaisingEvents = true;
        while (true) Thread.Sleep(1000);
    }

    static void Main()
    {
        string path = @"C:\Test";
        Thread monitorThread = new Thread(() => MonitorDirectory(path));
        monitorThread.Start();

        while (Console.ReadKey(true).Key != ConsoleKey.Q) ;
        Environment.Exit(0); 
    }
}