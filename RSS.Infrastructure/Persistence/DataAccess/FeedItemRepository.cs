using Microsoft.EntityFrameworkCore;
using RSS.Application.Interfaces.Repositories;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.DataAccess;

public class FeedItemRepository : BaseRepository<FeedItem>, IFeedItemRepository
{
    public FeedItemRepository(RssContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FeedItem>> GetUnreadAsync(DateTime date, string userName)
    {
        var news = await _dbSet.Include(n => n.ReadBy)
                                            .Where(n => n.PublishDate.Date == date.Date && n.ReadBy.All(u => u.UserName != userName))
                                            .ToListAsync();

        return news;
    }
}