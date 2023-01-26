using RSS.Domain.Entities;

namespace RSS.Application.Interfaces.Repositories;

public interface IFeedItemRepository : IRepository<FeedItem>
{
    Task<IEnumerable<FeedItem>> GetUnreadAsync(DateTime date, string userName);
}