using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client;

public static class Client
{
    private static bool _stop = false;
    private static string ReceiveVariableLengthMessage(Socket remoteSocket)
    {
        var bufor = new byte[4];
        remoteSocket.Receive(bufor, SocketFlags.None);
        var messageSize = BitConverter.ToInt32(bufor);
        
        bufor = new byte[messageSize];
        remoteSocket.Receive(bufor, SocketFlags.None);
        var message = Encoding.UTF8.GetString(bufor, 0, messageSize);
        return message;
    }
    private static void SendVariableLengthMessage(Socket remoteSocket, string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var byteSize = messageBytes.Length;
        var messageSizeBytes = BitConverter.GetBytes(byteSize);
        
        remoteSocket.Send(messageSizeBytes);
        remoteSocket.Send(messageBytes);
    }
    private static void StartClient()
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

        while (!_stop)
        {
            Console.Write("# ");
            var input = Console.ReadLine() ?? string.Empty;
            SendVariableLengthMessage(socket, input);
            if (input == "!end")
            {
                _stop = true;
                break;
            }
            Console.WriteLine(ReceiveVariableLengthMessage(socket));
        }
        
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch
        {
            // ignored
        }
    }

    public static void Main(string []args)
    {
        StartClient();
    }
}