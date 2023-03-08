namespace background_service;

public class SecondWorker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly TimeService _timeService;
    private readonly Guid _guid;
    private readonly IConfiguration _configuration;

    public SecondWorker(ILogger<Worker> logger, IConfiguration configuration, TimeService timeService)
    {
        (_logger, _timeService) = (logger, timeService);
        _guid = Guid.NewGuid();
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation(_timeService.convertAllTimeZones());
                await Task.Delay(_configuration.GetValue<int>("PrintDelay"), stoppingToken);
            }
            finally
            {
            }
        }
    }
}
