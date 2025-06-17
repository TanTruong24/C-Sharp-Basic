using System.Net;
using System.Net.Sockets;

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

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }

        serverSocket.Close();
    }
}
