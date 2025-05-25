using MyDIProject;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddTransient<IMessageWriter, ConsoleMessageWriter>();

builder.Services.AddTransient<IMessageWriter>(services => new ConsoleMessageWriter(
    services.GetRequiredService<INullInterface>()));

//builder.Services.AddTransient<INullInterface> (services => new NullClass());

var host = builder.Build();
host.Run();
