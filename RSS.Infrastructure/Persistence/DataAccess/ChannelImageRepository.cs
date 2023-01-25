using RSS.Application.Interfaces.Repositories;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Persistence.DataAccess;

public class ChannelImageRepository : BaseRepository<ChannelImage>, IChannelImageRepository
{
    public ChannelImageRepository(RssContext context) : base(context)
    {
    }
}