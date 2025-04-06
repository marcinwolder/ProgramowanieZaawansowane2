class DataItem
{
    public int ProducerId { get; set; }
}

class Program
{
    static Queue<DataItem> buffer = new Queue<DataItem>();
    static object locker = new object();
    static bool running = true;
    static int[] consumerStats = [];

    static void Producer(object? obj)
    {
        var (id, delay) = (0, 0);
        if (obj != null){
            (id, delay) = ((int, int))obj;       
        } else {
            (id, delay) = (0, 0);
        }
        Random rand = new Random(id);
        while (running)
        {
            Thread.Sleep(rand.Next(delay));
            var item = new DataItem { ProducerId = id };

            lock (locker)
            {
                buffer.Enqueue(item);
                Monitor.Pulse(locker); // powiadom konsumentów
            }
        }
    }

    static void Consumer(object? obj)
    {
        var (id, delay) = (0, 0);
        if (obj != null){
            (id, delay) = ((int, int))obj;       
        } else {
            (id, delay) = (0, 0);
        }
        Dictionary<int, int> consumed = new();
        Random rand = new Random(id);

        while (running)
        {
            DataItem? item = null;
            lock (locker)
            {
                while (buffer.Count == 0 && running)
                    Monitor.Wait(locker);

                if (buffer.Count > 0)
                    item = buffer.Dequeue();
            }

            if (item != null)
            {
                consumed.TryAdd(item.ProducerId, 0);
                consumed[item.ProducerId]++;
            }

            Thread.Sleep(rand.Next(delay));
        }

        Console.WriteLine($"Konsument {id}:");
        foreach (var kvp in consumed)
            Console.WriteLine($"  Producent {kvp.Key} - {kvp.Value}");
    }

    static void Main()
    {
        int n = 3, m = 2;
        int producerDelay = 500, consumerDelay = 800;

        for (int i = 0; i < n; i++)
            new Thread(Producer).Start((i, producerDelay));

        for (int i = 0; i < m; i++)
            new Thread(Consumer).Start((i, consumerDelay));

        new Thread(() =>
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Q) ;
            running = false;
            lock (locker) Monitor.PulseAll(locker);
        }).Start();
    }
}