using System.Net;
using System.Net.Sockets;
using System.Text;

namespace zad01;

public class Server
{
    private bool _end = false;
    private Socket? _serverSocket = null;
    private void StartServer()
    {
        var host = Dns.GetHostEntry("localhost");
        var ipAddress = host.AddressList[0];
        var localEndPoint = new IPEndPoint(ipAddress, 11000);

        _serverSocket = new Socket(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        _serverSocket.Bind(localEndPoint);
        _serverSocket.Listen(100);

        Console.WriteLine("Started server...");
        var clientSocket = _serverSocket.Accept();
        
        var bufor = new byte[1_024];
        var received = clientSocket.Receive(bufor, SocketFlags.None);
        var clientMessage = Encoding.UTF8.GetString(bufor, 0, received);
        Console.WriteLine(clientMessage);
        
        var serverResponse = "odczytałem: "+clientMessage;
        var echoBytes = Encoding.UTF8.GetBytes(serverResponse);
        clientSocket.Send(echoBytes, 0);

        _end = true;
    }

    private void StopServer()
    {
        try
        {
            if (_serverSocket == null) return;
            _serverSocket.Shutdown(SocketShutdown.Receive);
            _serverSocket.Close();
            Console.WriteLine("Stopped server...");
        }
        catch
        {
            throw new Exception("Cannot shutdown a server!");
        }
    }
    
    public static int Main(string[] args)
    {
        Server server = new();
        Task serverThread = new(server.StartServer);
        serverThread.Start();

        var task = new Task(() =>
            {
                while (Console.ReadKey(true).Key != ConsoleKey.Q || server._end) {}
                server.StopServer();
            }
        );
        task.Start();
        task.Wait();
        
        return 0;
    }
}
