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
        var endPoint = new IPEndPoint(
            string.IsNullOrEmpty(this.options.IPAddress) ? IPAddress.Any : IPAddress.Parse(this.options.IPAddress), 
            this.options.Port
            );

        using var serverSocket = new Socket(
            endPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
            );

        serverSocket.Bind(endPoint);

        _logger.LogInformation($"Listening...(port: {options.Port})");
        serverSocket.Listen();

        var clientConnection = new List<ClientConnection>();

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
        var cancelationTokenSource = new CancellationTokenSource(3000);
        // read request from socket
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