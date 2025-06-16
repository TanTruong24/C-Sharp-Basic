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
            serverSocket.Listen();

            Console.WriteLine($"Listening....port {endPoint.Port}");

            var clientHandlers = new List<Task>();

            try
            {
                while (true)
                {
                    var clientSocket = await serverSocket.AcceptAsync();
                    int currentClientId = clientId++; // Tăng ID client
                    var handlerTask = handleClientRequestAsync(clientSocket, currentClientId);
                    clientHandlers.Add(handlerTask);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Server Error] {ex.Message}");
            }
            finally
            {
                // Đảm bảo socket được đóng khi kết thúc
                serverSocket.Close();
                Console.WriteLine("[Server] Shutting down...");
            }

        }

        private static async Task handleClientRequestAsync(Socket clientSocket, int clientId)
        {
            Console.WriteLine($"Client {clientId} connected!");

            var WelcomByte = Encoding.UTF8.GetBytes(ChatProtocol.Constants.WelcomeText);
            await clientSocket.SendAsync(WelcomByte);

            var buffer = new byte[1024];

            try
            {
                while (true)
                {
                    var received = await clientSocket.ReceiveAsync(buffer);

                    // Nếu client ngắt kết nối (graceful)
                    if (received == 0)
                    {
                        Console.WriteLine($"[Client {clientId}] Disconnected (graceful).");
                        break;
                    }

                    var msg = Encoding.UTF8.GetString(buffer, 0, received);

                    if (msg.Equals(ChatProtocol.Constants.CommandShutdown))
                    {
                        closeConnection(clientSocket);
                        break;
                    }
                    Console.WriteLine($"[client {clientId}] {msg}");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"[Client {clientId}] Socket error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Client {clientId}] Error: {ex.Message}");
            }
            finally
            {
                clientSocket.Close();
                Console.WriteLine($"[Client {clientId}] Connection closed.");
            }
        }

        private static void closeConnection(Socket clientSocket)
        {
           clientSocket.Close();
        }
    }
}
