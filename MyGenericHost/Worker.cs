using MyGenericHost.Services;

namespace MyGenericHost;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEmailService _emailService;

    // Inject logger and email service using Dependency Injection
    public Worker(ILogger<Worker> logger, IEmailService emailSerivce)
    {
        _logger = logger;
        _emailService = emailSerivce;
    }

    // This method runs in the background when the app starts
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker Start");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Start send email: {time}", DateTimeOffset.Now);
            _emailService.Send("thetan@gmail.com", "Test Hello!", "Email automation sended The Tan");

            await Task.Delay(TimeSpan.FromSeconds(1000), stoppingToken);
        }

        _logger.LogWarning("App end...!");
    }
}
