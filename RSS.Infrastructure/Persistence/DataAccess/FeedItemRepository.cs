using RSS.Application.Interfaces.Repositories;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.DataAccess;

public class FeedItemRepository : BaseRepository<FeedItem>, IFeedItemRepository
{
    public FeedItemRepository(RssContext context) : base(context)
    {
    }
}