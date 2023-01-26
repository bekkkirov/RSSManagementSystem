using Microsoft.EntityFrameworkCore;
using RSS.Application.Interfaces.Repositories;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.DataAccess;

public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
{
    public ChannelRepository(RssContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Channel>> GetAllWithImagesAsync()
    {
        return await _dbSet.Include(c => c.ChannelImage)
                           .AsNoTracking()
                           .ToListAsync();
    }

    public async Task<bool> AnyAsync(string link)
    {
        return await _dbSet.AnyAsync(c => c.Link == link);
    }

    public async Task<Channel?> GetByLinkAsync(string link)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Link == link);
    }

    public async Task<IEnumerable<string>> ExtractFeedLinksAsync()
    {
        return await _dbSet.Select(c => c.Link)
                           .ToListAsync();
    }
}