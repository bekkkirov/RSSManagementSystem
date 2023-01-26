using RSS.Application.Models;

namespace RSS.Application.Interfaces.Services;

public interface INewsService
{
    Task<IEnumerable<FeedItemDto>> GetUnreadByDateAsync(DateTime date);

    Task MarkAsReadAsync(int id);
}