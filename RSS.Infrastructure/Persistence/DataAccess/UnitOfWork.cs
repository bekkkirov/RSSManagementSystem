using RSS.Application.Interfaces.Repositories;

namespace RSS.Infrastructure.Persistence.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly RssContext _context;

    public IUserRepository UserRepository { get; }

    public IChannelImageRepository ChannelImageRepository { get; }

    public IChannelRepository ChannelRepository { get; }

    public IFeedItemRepository FeedItemRepository { get; }

    public UnitOfWork(RssContext context,
                      IUserRepository userRepository, IChannelImageRepository channelImageRepository,
                      IChannelRepository channelRepository, IFeedItemRepository feedItemRepository)
    {
        _context = context;

        UserRepository = userRepository;
        ChannelImageRepository = channelImageRepository;
        ChannelRepository = channelRepository;
        FeedItemRepository = feedItemRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}