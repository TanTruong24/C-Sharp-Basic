using MyGenericHost.Services;
using MyGenericHost;

/*
 * 
 * 
 */

// Create a host builder with default settings:
// - Loads configuration (appsettings.json, environment variables, command-line args)
// - Sets up logging
// - Sets up Dependency Injection (DI) container
var builder = Host.CreateApplicationBuilder(args);

// Register IEmailService with its implementation EmailService as a singleton.
// This means the same instance will be used throughout the app.
builder.Services.AddSingleton<IEmailService, EmailService>();

// Register the background worker service.
// The Worker class should inherit from BackgroundService.
builder.Services.AddHostedService<Worker>();

// Build the host with all the configured services and settings
var host = builder.Build();

// Run the app. This starts any hosted services and keeps the app running
host.Run();


/*
 * Không dùng DI
 * 
    var services = new ServiceCollection();

    // tự đăng ký các service
    services.AddSingleton<IEmailService, EmailService>();
    services.AddSingleton<Worker>();

    // tự tạo container
    var provider = services.BuildServiceProvider();

    // Tự resolve service
    var worker = provider.GetRequiredService<Worker>();

    // gọi thành công "main logic"
    await worker.Run();
*
*/