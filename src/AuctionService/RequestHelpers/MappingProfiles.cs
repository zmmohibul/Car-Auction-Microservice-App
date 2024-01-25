using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(auction => auction.Item);
        CreateMap<Item, AuctionDto>();
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s));
        CreateMap<CreateAuctionDto, Item>();
        
        CreateMap<AuctionDto, AuctionCreated>();
        CreateMap<Auction, AuctionUpdated>()
            .ForMember(d => d.Make, o => o.MapFrom(s => s.Item.Make))
            .ForMember(d => d.Model, o => o.MapFrom(s => s.Item.Model))
            .ForMember(d => d.Year, o => o.MapFrom(s => s.Item.Year))
            .ForMember(d => d.Color, o => o.MapFrom(s => s.Item.Color))
            .ForMember(d => d.Mileage, o => o.MapFrom(s => s.Item.Mileage));
    }
}