using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server;

public static class Server
{
    private static void StartServer()
    {
        var host = Dns.GetHostEntry("localhost");
        var ipAddress = host.AddressList[0];
        var localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket serverSocket = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );
        
        serverSocket.Bind(localEndPoint);
        serverSocket.Listen(100);

        Console.WriteLine("Server started");
        
        var clientSocket = serverSocket.Accept();
        
        var buffer = new byte[1_024];
        var received = clientSocket.Receive(buffer, SocketFlags.None);
        var clientMessage = Encoding.UTF8.GetString(buffer, 0, received);
        Console.WriteLine(clientMessage);
        
        var response = "otrzymałem: "+clientMessage;
        var echoBytes = Encoding.UTF8.GetBytes(response);
        clientSocket.Send(echoBytes, 0);
        
        try
        {
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
        }
        catch
        {
            // ignored
        }
        
        Console.WriteLine("Server stopped");
    }

    public static void Main(string []args)
    {
        StartServer();
    }
}
