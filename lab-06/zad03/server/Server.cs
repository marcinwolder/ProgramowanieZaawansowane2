using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server;

public static class Server
{
    private static bool _stop = false;
    private static string _myDir = "";

    private static string List()
    {
        Console.WriteLine("Listed all files and directories");
        var content = Directory.GetFileSystemEntries(_myDir)
            .Select(Path.GetFileName);
        content = content.Append("..");
        return string.Join("\n", content);
    }
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
    private static void StartServer()
    {
        _myDir = Directory.GetCurrentDirectory();
        Console.WriteLine("myDir: "+_myDir);
        
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
        while (!_stop)
        {
            var message = ReceiveVariableLengthMessage(clientSocket);
            
            var args = message.Split(" ");
            var command = args[0];
            args = args.Skip(1).ToArray();

            switch (command)
            {
                case "in":
                    if (args[0] == "..")
                    {
                        Console.WriteLine("Changing my_dir to parent directory");
                        var parent = Directory.GetParent(_myDir);
                        if (parent == null) break;
                        _myDir = parent.FullName;
                        Console.WriteLine("New my_dir: "+_myDir);
                    }
                    else if (Directory.GetDirectories(_myDir).Select(Path.GetFileName).Contains(args[0]))
                    {
                        _myDir = Path.Combine(_myDir, args[0]);
                    }
                    else
                    {
                        SendVariableLengthMessage(clientSocket, "katalog nie istnieje");
                        break;
                    }
                    SendVariableLengthMessage(clientSocket, List());
                    break;
                case "list":
                    SendVariableLengthMessage(clientSocket, List());
                    break;
                case "!end":
                    _stop = true;
                    break;
                default:
                    SendVariableLengthMessage(clientSocket, "nieznane polecenie");
                    break;
            }
        }
        
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
