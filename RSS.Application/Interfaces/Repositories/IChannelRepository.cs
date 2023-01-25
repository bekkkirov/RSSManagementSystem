using RSS.Domain.Entities;

namespace RSS.Application.Interfaces.Repositories;

public interface IChannelRepository : IRepository<Channel>
{
    Task<bool> AnyAsync(string link);
}