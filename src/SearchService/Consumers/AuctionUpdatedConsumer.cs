using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
{
    private readonly IMapper _mapper;

    public AuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        var updatedItem = _mapper.Map<Item>(context.Message);
        var result = await DB.Update<Item>()
            .MatchID(updatedItem.ID)
            .ModifyOnly(item => new
            {
                item.Make,
                item.Model,
                item.Year,
                item.Color,
                item.Mileage
            }, updatedItem)
            .ExecuteAsync();
        
        if (!result.IsAcknowledged) 
            throw new MessageException(typeof(AuctionUpdated), "Problem updating mongodb");
    }
}