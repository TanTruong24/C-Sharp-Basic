using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, ChatProtocol.Constants.DefaultChatPort);

            var clientSocket = new Socket(
                endPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
                );

            await clientSocket.ConnectAsync(endPoint);
            var buffer = new byte[1024];

            var r = await clientSocket.ReceiveAsync(buffer);
            if (r == 0)
            {
                showConnectionError();
                return;
            }
            var WelcomeText = Encoding.UTF8.GetString(buffer, 0, r);
            if (!ChatProtocol.Constants.WelcomeText.Equals(WelcomeText))
            {
                showConnectionError();
                return;
            }
            Console.WriteLine(WelcomeText);

            while (true)
            {
                Console.Write("Enter message: ");
                var msg = Console.ReadLine();

                if (string.IsNullOrEmpty(msg)) {
                    await closeConnectionAsync(clientSocket);
                    return;
                }
                else
                {
                    var bytes = Encoding.UTF8.GetBytes(msg);
                    await clientSocket.SendAsync(bytes);
                }

            }
        }

        private static async Task closeConnectionAsync(Socket clientSocket)
        {
            var bytes = Encoding.UTF8.GetBytes(ChatProtocol.Constants.CommandShutdown);
            await clientSocket.SendAsync(bytes);

            clientSocket.Close();
        }

        private static void showConnectionError()
        {
            Console.WriteLine($"Connection Error");
        }
    }
}
