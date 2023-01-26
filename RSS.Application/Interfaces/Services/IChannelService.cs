using RSS.Application.Models;

namespace RSS.Application.Interfaces.Services;

public interface IChannelService
{
    Task<IEnumerable<ChannelDto>> GetAllAsync();

    Task<ChannelDto> GetByIdAsync(int id);

    Task<ChannelDto> AddAsync(string feedUrl);

    Task FetchNewsAsync(IEnumerable<string> links);
}