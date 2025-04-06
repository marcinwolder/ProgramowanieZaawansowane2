class Program
{
    static CountdownEvent countdown = new(0);
    static bool end = false;

    static void Worker(object? id)
    {
        Console.WriteLine($"Wątek {id ?? "<unknown>"} rozpoczął działanie.");
        countdown.Signal();
        while (!end) Thread.Sleep(1000);
        Console.WriteLine($"Wątek {id ?? "<unknown>"} kończy.");
    }

    static void Main()
    {
        int n = 5;
        countdown = new CountdownEvent(n);
        List<Thread> threads = [];

        for (int i = 0; i < n; i++)
        {
            var thread = new Thread(Worker);
            threads.Add(thread);
            thread.Start(i);
        }
        countdown.Wait(); 
        Console.WriteLine("Wszystkie wątki rozpoczęły działanie.");

        end = true;
        foreach (var thread in threads)
        {
            thread.Join();
        }
        Console.WriteLine("Program zakończony.");
    }
}