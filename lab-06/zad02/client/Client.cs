using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client;

public static class Client
{
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

        const string message = "Wiadomość od klienta";
        // \/ 2000B Lorem ipsum
        // const string message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eget imperdiet diam. Phasellus venenatis non nunc non tincidunt. Aenean nec magna vitae leo vestibulum varius. Phasellus nec odio sollicitudin, lacinia diam varius, finibus nunc. Cras aliquet lobortis lorem quis commodo. Maecenas commodo, magna vel luctus aliquet, orci libero pulvinar augue, ut euismod risus ante pharetra turpis. Integer tincidunt felis id turpis aliquam posuere. Cras gravida lacinia eros, consequat pretium leo rhoncus in. Donec commodo, erat ut pellentesque aliquet, lectus tellus mollis nisi, sit amet porttitor ipsum ex vitae justo. Integer a enim sed nisl mattis pharetra vitae sed purus. Nulla facilisi. Vivamus sed venenatis nibh. Pellentesque luctus fermentum sapien, in convallis erat posuere sit amet. Integer vitae dignissim enim, eget tempus odio. Nam magna justo, tincidunt eu dui in, posuere ullamcorper tellus. Nam volutpat at lacus nec maximus.\n\nMauris vel augue ac dui porttitor tincidunt. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed placerat lacus tellus, et congue sem venenatis sit amet. Phasellus aliquet pharetra orci at imperdiet. Donec laoreet, quam id laoreet auctor, purus tortor dictum tellus, quis ultrices est ante nec nisi. Interdum et malesuada fames ac ante ipsum primis in faucibus. Proin sodales lorem nisi, ac sollicitudin tellus bibendum sed. Ut ornare vulputate laoreet. Fusce id blandit enim.\n\nVestibulum facilisis quam nec turpis auctor, non blandit urna volutpat. Nunc tristique egestas congue. Pellentesque sit amet orci suscipit, efficitur urna ac, ultricies felis. Etiam posuere, ex id ultricies malesuada, sem sapien imperdiet mi, et feugiat justo dolor ut orci. Nunc tincidunt vel magna at ullamcorper. Quisque gravida sapien sed nisi malesuada, in pellentesque metus elementum. Pellentesque nisl tortor, pharetra eget ex sit amet, luctus luctus mi. Maecenas quis efficitur sapien. In dictum dolor eu nisi dignissim venenatis. Nam nec ex sem sodales sed.";
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var byteSize = messageBytes.Length;
        var messageSizeBytes = BitConverter.GetBytes(byteSize);
        Console.WriteLine("byteSize: "+byteSize);
        
        socket.Send(messageSizeBytes);
        socket.Send(messageBytes);
        
        var bufor = new byte[4];
        socket.Receive(bufor, SocketFlags.None);
        var serverResponseSize = BitConverter.ToInt32(bufor);
        Console.WriteLine("serverResponseSize: "+serverResponseSize);
        
        bufor = new byte[serverResponseSize];
        socket.Receive(bufor, SocketFlags.None);
        var serverResponse = Encoding.UTF8.GetString(bufor, 0, serverResponseSize);
        Console.WriteLine("serverResponse: "+serverResponse);
        
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