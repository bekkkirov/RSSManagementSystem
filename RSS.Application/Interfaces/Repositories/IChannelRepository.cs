using RSS.Domain.Entities;

namespace RSS.Application.Interfaces.Repositories;

public interface IChannelRepository : IRepository<Channel>
{
    Task<IEnumerable<Channel>> GetAllWithImagesAsync();

    Task<bool> AnyAsync(string link);

    Task<Channel?> GetByLinkAsync(string link);

    Task<IEnumerable<string>> ExtractFeedLinksAsync();
}