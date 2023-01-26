using System.ServiceModel.Syndication;
using System.Xml;
using AutoMapper;
using RSS.Application.Exceptions;
using RSS.Application.Interfaces.Repositories;
using RSS.Application.Interfaces.Services;
using RSS.Application.Models;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Services;

public class ChannelService : IChannelService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public ChannelService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ChannelDto>> GetAllAsync()
    {
        var channels = await _unitOfWork.ChannelRepository.GetAllWithImagesAsync();

        return _mapper.Map<IEnumerable<ChannelDto>>(channels);
    }

    public async Task<ChannelDto> GetByIdAsync(int id)
    {
        var channel = await _unitOfWork.ChannelRepository.GetByIdAsync(id);

        if (channel is null)
        {
            throw new NotFoundException("Channel with specified id wasn't found.");
        }

        return _mapper.Map<ChannelDto>(channel);
    }

    public async Task<ChannelDto> AddAsync(string feedUrl)
    {
        Channel? channel;

        if (!await _unitOfWork.ChannelRepository.AnyAsync(feedUrl))
        {
            using var reader = XmlReader.Create(feedUrl);
            var feed = SyndicationFeed.Load(reader);

            channel = _mapper.Map<Channel>(feed);

            var channelImage = new ChannelImage()
            {
                Title = channel.Title,
                Link = feedUrl,
                Url = feed.ImageUrl?.AbsoluteUri,
            };

            channel.Link = feedUrl;
            channel.ChannelImage = channelImage;

            _unitOfWork.ChannelRepository.Add(channel);
        }

        else
        {
            channel = await _unitOfWork.ChannelRepository.GetByLinkAsync(feedUrl);
        }

        var userName = _currentUserService.GetUserName();
        var currentUser = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);

        if (!currentUser.Channels.Any(c => c.Link == feedUrl))
        {
            currentUser.Channels.Add(channel);
        }

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ChannelDto>(channel);
    }

    public async Task FetchNewsAsync(IEnumerable<string> links)
    {
        foreach (var link in links)
        {
            var channel = await _unitOfWork.ChannelRepository.GetByLinkAsync(link);
            var news = FetchNewsFromLink(link);

            channel.Items.AddRange(news);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private IEnumerable<FeedItem> FetchNewsFromLink(string link)
    {
        using var reader = XmlReader.Create(link);
        var feed = SyndicationFeed.Load(reader);

        var news = _mapper.Map<IEnumerable<FeedItem>>(feed.Items);

        return news;
    }
}