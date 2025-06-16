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
            // Địa chỉ server: localhost và port định nghĩa sẵn trong ChatProtocol.Constants
            var endPoint = new IPEndPoint(IPAddress.Loopback, ChatProtocol.Constants.DefaultChatPort);

            // Tạo socket TCP client
            using var clientSocket = new Socket(
                endPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
                );

            try 
            {
                await clientSocket.ConnectAsync(endPoint);
                var buffer = new byte[1024];

                var r = await clientSocket.ReceiveAsync(buffer);
                if (r == 0)
                {
                    ShowConnectionError("Server disconnected unexpectedly.");
                    return;
                }
                var WelcomeText = Encoding.UTF8.GetString(buffer, 0, r);
                if (!ChatProtocol.Constants.WelcomeText.Equals(WelcomeText))
                {
                    ShowConnectionError("Unexpected welcome message.");
                    return;
                }
                Console.WriteLine(WelcomeText);

                while (true)
                {
                    Console.Write("Enter message (empty to exit): ");
                    var msg = Console.ReadLine();

                    if (string.IsNullOrEmpty(msg))
                    {
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
            catch (SocketException ex)
            {
                ShowConnectionError($"SocketException: {ex.Message}");
            }
            catch (Exception ex)
            {
                ShowConnectionError($"Exception: {ex.Message}");
            }


        }

        private static async Task closeConnectionAsync(Socket clientSocket)
        {
            var bytes = Encoding.UTF8.GetBytes(ChatProtocol.Constants.CommandShutdown);
            await clientSocket.SendAsync(bytes);

            // Đóng kết nối
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            Console.WriteLine("Disconnected from server.");
        }

        private static void ShowConnectionError(string reason)
        {
            Console.WriteLine($"[Connection Error] {reason}");
        }
    }
}
