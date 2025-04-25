using System.ComponentModel;
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
        
        var buffer = new byte[4];
        clientSocket.Receive(buffer, SocketFlags.None);
        var clientMessageSize = BitConverter.ToInt32(buffer);
        Console.WriteLine("clientMessageSize: "+clientMessageSize);
        
        buffer = new byte[clientMessageSize];
        clientSocket.Receive(buffer, SocketFlags.None);
        var clientMessage = Encoding.UTF8.GetString(buffer, 0, clientMessageSize);
        Console.WriteLine("clientMessage: "+clientMessage);
        
        var response = "otrzymałem: "+clientMessage;
        var responseBytes = Encoding.UTF8.GetBytes(response);
        var byteSize = responseBytes.Length;
        Console.WriteLine("byteSize: "+byteSize);
        var responseSizeBytes = BitConverter.GetBytes(byteSize);

        clientSocket.Send(responseSizeBytes);
        clientSocket.Send(responseBytes);
        
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
