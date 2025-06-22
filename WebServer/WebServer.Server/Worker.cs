using System.Net;
using System.Net.Quic;
using System.Net.Sockets;
using System.Text;
using WebServer.SDK;

namespace WebServer.Server;

public class Worker : BackgroundService
{
    private WebServerOptions options;
    private readonly ILogger<Worker> _logger;

    public Worker(WebServerOptions options, ILogger<Worker> logger)
    {
        _logger = logger;
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Sử dụng WebServerOptions (bạn định nghĩa class này trong WebServerOptions.cs) để lấy IP/Port lắng nghe.
        // Nếu IPAddress rỗng → lắng nghe tất cả IP(IPAddress.Any).
        var endPoint = new IPEndPoint(
            string.IsNullOrEmpty(this.options.IPAddress) ? IPAddress.Any : IPAddress.Parse(this.options.IPAddress), 
            this.options.Port
            );

        // Tạo socket kiểu TCP.
        // SocketType.Stream và ProtocolType.Tcp tạo một TCP server cơ bản. 
        using var serverSocket = new Socket(
            endPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
            );

        //Bind socket tới IPAddress:Port.
        //Listen() bắt đầu lắng nghe client đến.
        serverSocket.Bind(endPoint);

        _logger.LogInformation($"Listening...(port: {options.Port})");
        serverSocket.Listen();

        var clientConnection = new List<ClientConnection>();

        // Khi có client kết nối, AcceptAsync nhận socket từ client.
        // Tạo một task bất đồng bộ xử lý client (HandleNewClientConnectionAsync).
        // Thêm vào list ClientConnection để theo dõi và Task.WaitAll() cuối cùng đợi xử lý xong tất cả connection.
        while (!stoppingToken.IsCancellationRequested)
        {
            var clientSocket = await serverSocket.AcceptAsync(stoppingToken);

            if (clientSocket != null)
            {
                var t = HandleNewClientConnectionAsync(clientSocket, stoppingToken);
                clientConnection.Add(new ClientConnection()
                {
                    HandlerTask = t
                });
            }
        }

        Task.WaitAll(clientConnection.Select(c => c.HandlerTask).ToArray());

        serverSocket.Close();
    }

    private async Task HandleNewClientConnectionAsync(Socket socket, CancellationToken stoppingToken)
    {
        // Tạo CancellationTokenSource với timeout 3 giây
        var cancelationTokenSource = new CancellationTokenSource(3000);

        // Đọc request từ socket qua ReadRequestAsync(...)
        WRequest request = await ReadRequestAsync(socket, CancellationTokenSource.CreateLinkedTokenSource(stoppingToken, cancelationTokenSource.Token).Token);

        // send back the response
        await SendResponseAsync(socket);

        // create response
        socket.Close();
    }

    private async Task SendResponseAsync(Socket socket)
    {
        var stream = new NetworkStream(socket);
        var streamWriter = new StreamWriter(stream);

        await streamWriter.WriteLineAsync("200 OK");
        await streamWriter.FlushAsync();
    }

    private async Task<WRequest> ReadRequestAsync(Socket socket, CancellationToken cancellationToken)
    {
        // Đọc dòng đầu tiên (ví dụ: GET /index.html HTTP/1.1)
        // Parse dòng đầu bằng RequestLineParser.
        var stream = new NetworkStream(socket);
        var reader = new StreamReader(stream, Encoding.ASCII);

        var requestBuilder = new WRequestBuilder();

        var requestLine = await reader.ReadLineAsync(cancellationToken);
        _logger.LogInformation(requestLine);

        if (requestLine != null)
        {
            if (RequestLineParser.TryParse(requestLine, out var parser))
            {
                var headerLine = await reader.ReadLineAsync(cancellationToken);
                while (!string.IsNullOrEmpty(headerLine))
                {
                    _logger.LogInformation(headerLine);

                    headerLine = await reader.ReadLineAsync(cancellationToken);
                }
            }
        }

        return requestBuilder.Build();
    }
}