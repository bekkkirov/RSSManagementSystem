using System.ServiceModel.Syndication;
using AutoMapper;
using RSS.Application.Models;
using RSS.Domain.Entities;

namespace RSS.Infrastructure.Mapping;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ChannelImage, ChannelImageDto>();
        CreateMap<Channel, ChannelDto>();

        CreateMap<FeedItem, FeedItemDto>();

        CreateMap<SyndicationFeed, Channel>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title.Text))
            .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Description.Text))
            .ForMember(d => d.LastBuildDate, opt => opt.MapFrom(src => src.LastUpdatedTime.Date))
            .ForMember(d => d.Language, opt => opt.MapFrom(src => src.Language))
            .ForMember(d => d.Copyright, opt => opt.MapFrom(src => src.Copyright.Text))
            .ForMember(d => d.Items, opt => opt.Ignore());

        CreateMap<SyndicationItem, FeedItem>()
            .ForMember(d => d.Id, src => src.Ignore())
            .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title.Text))
            .ForMember(d => d.Link, opt => opt.MapFrom(src => src.Links.First()))
            .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Summary.Text))
            .ForMember(d => d.Author, opt => opt.MapFrom(src => src.Authors.FirstOrDefault()))
            .ForMember(d => d.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date));
    }
}