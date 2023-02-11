namespace Easytrade.Logic.Mappings
{
    using AutoMapper;
    using Easytrade.Contract.Dto.Bots;
    using Easytrade.Contract.Dto.Orders;
    using Easytrade.Contract.Requests.Bots;
    using Easytrade.Model.Domain.Bots;

    public class EasytradeApiLogicProfile : Profile
    {
        public EasytradeApiLogicProfile()
        {
            CreateMap<BotDto, Bot>().ReverseMap();
            CreateMap<CreateBotRequest, Bot>();
            CreateMap<UpdateBotRequest, Bot>();

            CreateMap<BuyOrder, OrderDto>().ReverseMap();
            CreateMap<SellOrder, OrderDto>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
        }
    }
}
