using System.Net;
using System.Net.Sockets;

namespace zad01;

public class Server
{
    private Socket? _listener = null;
    private void StartServer()
    {
        var host = Dns.GetHostEntry("localhost");
        var ipAddress = host.AddressList[0];
        var localEndPoint = new IPEndPoint(ipAddress, 11000);

        try {
            _listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(localEndPoint);
            _listener.Listen(10);

            // symulacja komunikacji klient - serwer
            // w losowych odstępach czasu wysyłamy wiadomość do każdego wątku klienta 
            Random rand = new Random();
            Task symKomunikacji = new Task(async ()=>
            {
                while (!koniec)
                {
                    // sekcja krytyczna - nie możemy modyfikować kolekcji workerThreads
                    // z innego miejsca programu, jeśli idzie po niej pętla foreach
                    // ta sekcja wyklucza się z sekcją dodawania nowego połączenia do listy
                    // workerThreads
                    lock (workerThreads)
                    {
                        foreach(WatekKlientaTCP wt in workerThreads)
                        {
                            // jeśli wątek ustawiony jest na koniec, to usuwamy go
                            if (wt.koniec)
                                workerThreads.Remove(wt);
                            else//jeśli nie, wysyłamy wiadomość
                            {
                                wt.WyslijWiadomosc("Serwer mówi: cześć!");
                            }       
                        }
                    }
                    Task.Delay(rand.Next(500,5000)).Wait();
                }
            });
            symKomunikacji.Start();
            //
            Console.WriteLine("Serwer czeka na nowe połączenia");
            //wątek czeka do zakończenia programu, tak naprawdę program serwera
            while (!koniec) 
            {
                Socket handler = listener.Accept();
                Console.WriteLine("Odebrano połączenie");
                WatekKlientaTCP wt = new WatekKlientaTCP(handler, 
                    OdebranaWiadomosc, UsunWorkera);
                Task t = new Task(wt.Start);
                t.Start();
                workerThreads.Add(wt);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private void StopServer()
    {
        
    }
    
    public static int Main(string[] args)
    {
        Server server = new();
        Task serverThread = new(server.StartServer);
        serverThread.Start();

        new Task(() =>
            {
                while (Console.ReadKey(true).Key != ConsoleKey.Q) ;
                server.StopServer();
            }
        ).Start();
            
        return 0;
    }
}
