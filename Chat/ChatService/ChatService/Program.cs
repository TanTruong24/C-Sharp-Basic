using Microsoft.VisualBasic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int clientId = 0;

            var endPoint = new IPEndPoint(IPAddress.Loopback, ChatProtocol.Constants.DefaultChatPort);
            var serverSocket = new Socket(
                endPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
                );

            serverSocket.Bind(endPoint);

            Console.WriteLine($"Listening....port {endPoint.Port}");

            serverSocket.Listen();

            var clientHandlers = new List<Task>();

            while(true)
            {
                var clientSocket = await serverSocket.AcceptAsync(); 
                var t = handleClientRequestAsync(clientSocket, clientId++);
                clientHandlers.Add(t);
            }

            Task.WaitAll([..clientHandlers]);
        }

        private static async Task handleClientRequestAsync(Socket clientSocket, int clientId)
        {
            Console.WriteLine($"Client {clientId} connected!");

            var WelcomByte = Encoding.UTF8.GetBytes(ChatProtocol.Constants.WelcomeText);
            await clientSocket.SendAsync(WelcomByte);

            var buffer = new byte[1024];

            while (true)
            {
                var r = await clientSocket.ReceiveAsync(buffer);

                var msg = Encoding.UTF8.GetString(buffer, 0, r);

                if (msg.Equals(ChatProtocol.Constants.CommandShutdown))
                {
                    closeConnection(clientSocket);
                    break;
                }
                Console.WriteLine($"[client {clientId}] {msg}");
            }
        }

        private static void closeConnection(Socket clientSocket)
        {
           clientSocket.Close();
        }
    }
}
