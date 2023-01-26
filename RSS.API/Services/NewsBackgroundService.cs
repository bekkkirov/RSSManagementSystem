using RSS.Application.Interfaces.Repositories;
using RSS.Application.Interfaces.Services;

namespace RSS.API.Services;

public class NewsBackgroundService : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromHours(3);
    private readonly IServiceScopeFactory _scopeFactory;

    public NewsBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var channelService = scope.ServiceProvider.GetRequiredService<IChannelService>();

            var links = await unitOfWork.ChannelRepository.ExtractFeedLinksAsync();

            await channelService.FetchNewsAsync(links);
        }
    }
}