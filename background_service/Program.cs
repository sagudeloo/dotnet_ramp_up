using background_service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<TimeService>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
