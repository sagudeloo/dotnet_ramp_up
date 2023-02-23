namespace background_service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly Guid _guid;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        (_logger, _serviceProvider) = (logger, serviceProvider);
        _guid = Guid.NewGuid();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    TimeService timeService = scope.ServiceProvider.GetRequiredService<TimeService>();
                    _logger.LogInformation(timeService.convertAllTimeZones());
                    await Task.Delay(5000, stoppingToken);
                }
            }
            finally
            {
                _logger.LogInformation("Background service completed");
            }
        }
    }
}
