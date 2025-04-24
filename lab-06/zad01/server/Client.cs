using System.Net;
using System.Net.Sockets;
using System.Text;

namespace zad01;

public class Client
{
    public static void Main(string []args)
    {
        var host = Dns.GetHostEntry("localhost");
        var ipAddress = host.AddressList[0];
        var localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket socket = new(
            localEndPoint.AddressFamily, 
            SocketType.Stream, 
            ProtocolType.Tcp
        );
        socket.Connect(localEndPoint);

        const string message = "Wiadomość od klienta";
        var messageBytes = Encoding.UTF8.GetBytes(message);
        socket.Send(messageBytes, SocketFlags.None);
        
        var bufor = new byte[1_024];
        var bytesCount = socket.Receive(bufor, SocketFlags.None);
        var serverResponse = Encoding.UTF8.GetString(bufor, 0, bytesCount);
        Console.WriteLine(serverResponse);
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch
        {
            throw new Exception("Cannot shutdown client!");
        }
    }
}