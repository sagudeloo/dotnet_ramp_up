namespace background_service;

public class SecondWorker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly TimeService _timeService;
    private readonly Guid _guid;

    public SecondWorker(ILogger<Worker> logger, TimeService timeService)
    {
        (_logger, _timeService) = (logger, timeService);
        _guid = Guid.NewGuid();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation(_timeService.convertAllTimeZones());
                await Task.Delay(5000, stoppingToken);
            }
            finally
            {
            }
        }
    }
}
