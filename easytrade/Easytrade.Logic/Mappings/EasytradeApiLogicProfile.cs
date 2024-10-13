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
            CreateMap<CreateBotRequest, Bot>();
            CreateMap<UpdateBotRequest, Bot>();

            CreateMap<Bot, BotDto>()
                .ForMember(dest => dest.Orders,
                    opt => opt.MapFrom(src => MapOrders(src)));

            CreateMap<ProfitLoss, ProfitLossDto>().ReverseMap();

            CreateMap<Order, BuyOrder>();
            CreateMap<Order, SellOrder>();
            CreateMap<BuyOrder, OrderDto>().ReverseMap();
            CreateMap<SellOrder, OrderDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
        }

        private IEnumerable<Order> MapOrders(Bot bot)
        {
            return bot.SellOrders.Concat<Order>(bot.BuyOrders)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
    }
}
