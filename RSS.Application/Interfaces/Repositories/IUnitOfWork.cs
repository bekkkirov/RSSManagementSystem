namespace RSS.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }

    public IChannelImageRepository ChannelImageRepository { get; }

    public IChannelRepository ChannelRepository { get; }

    public IFeedItemRepository FeedItemRepository { get; }

    Task SaveChangesAsync();
}