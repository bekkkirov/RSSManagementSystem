using Microsoft.EntityFrameworkCore;
using RSS.Application.Interfaces.Repositories;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.DataAccess;

public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
{
    public ChannelRepository(RssContext context) : base(context)
    {
    }

    public async Task<bool> AnyAsync(string link)
    {
        return await _dbSet.AnyAsync(c => c.Link == link);
    }
}