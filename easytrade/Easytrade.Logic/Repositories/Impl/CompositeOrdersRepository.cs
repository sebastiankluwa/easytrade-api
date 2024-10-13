namespace Easytrade.Logic.Repositories.Impl
{
    using AutoMapper;
    using Easytrade.Contract.Dto.Orders;
    using Easytrade.Model.Domain.Bots;
    using Easytrade.Model.Repositories.Impl;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompositeOrdersRepository : ICompositeOrdersRepository
    {
        private readonly IBuyOrderRepository _buyOrderRepository;
        private readonly ISellOrderRepository _sellOrderRepository;
        private readonly IMapper _mapper;

        public CompositeOrdersRepository(IBuyOrderRepository buyOrderRepository, 
            ISellOrderRepository sellOrderRepository, IMapper mapper)
        {
            _buyOrderRepository = buyOrderRepository;
            _sellOrderRepository = sellOrderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrder(long orderId)
        {
            var buyOrder = await _buyOrderRepository.GetOrderByIdAsync(orderId);
            var sellOrder = await _sellOrderRepository.GetOrderByIdAsync(orderId);

            return sellOrder != null 
                ? _mapper.Map<OrderDto>(sellOrder) 
                : _mapper.Map<OrderDto>(buyOrder);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersByBotId(long botId)
        {
            var buyOrders = await _buyOrderRepository.GetAllOrdersByBotIdAsync(botId);
            var sellOrders = await _sellOrderRepository.GetAllOrdersByBotIdAsync(botId);

            var concatenatedOrders = buyOrders.Concat<Order>(sellOrders)
                .OrderByDescending(o => o.OrderDate);

            return _mapper.Map<IEnumerable<OrderDto>>(concatenatedOrders);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOpenOrdersByBotId(long botId)
        {
            var buyOpenOrders = await _buyOrderRepository.GetAllOpenOrdersAsync(botId);
            var sellOpenOrders = await _sellOrderRepository.GetAllOpenOrdersAsync(botId);

            var concatenatedOrders = buyOpenOrders.Concat<Order>(sellOpenOrders);

            return _mapper.Map<IEnumerable<OrderDto>>(concatenatedOrders);
        }
    }
}
