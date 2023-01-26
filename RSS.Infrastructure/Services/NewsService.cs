using AutoMapper;
using RSS.Application.Exceptions;
using RSS.Application.Interfaces.Repositories;
using RSS.Application.Interfaces.Services;
using RSS.Application.Models;

namespace RSS.Infrastructure.Services;

public class NewsService : INewsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public NewsService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FeedItemDto>> GetUnreadByDateAsync(DateTime date)
    {
        var userName = _currentUserService.GetUserName();

        var news = await _unitOfWork.FeedItemRepository.GetUnreadAsync(date, userName);

        return _mapper.Map<IEnumerable<FeedItemDto>>(news);
    }

    public async Task MarkAsReadAsync(int id)
    {
        var feedItem = await _unitOfWork.FeedItemRepository.GetByIdAsync(id);

        if (feedItem is null)
        {
            throw new NotFoundException("News with specified id wasn't found");
        }

        var userId = _currentUserService.GetId();
        var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        currentUser.ReadNews.Add(feedItem);
        await _unitOfWork.SaveChangesAsync();
    }
}