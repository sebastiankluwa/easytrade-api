namespace Easytrade.Logic.Repositories.Impl
{
    using AutoMapper;
    using Easytrade.Contract.Dto.Orders;
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

        public async Task<IEnumerable<OrderDto>> GetAllOrdersByBotId(long botId)
        {
            var tasks = new[] { GetBuyOrders(botId), GetSellOrders(botId) };

            var tasksResult = await Task.WhenAll(tasks);

            var result = tasksResult
                .SelectMany(tr => tr)
                .ToList();

            return result;
        }

        private async Task<IEnumerable<OrderDto>> GetBuyOrders(long botId)
        {
            var buyOrders = await _buyOrderRepository.GetAllOrdersByBotIdAsync(botId);

            return _mapper.Map<IEnumerable<OrderDto>>(buyOrders);
        }

        private async Task<IEnumerable<OrderDto>> GetSellOrders(long botId)
        {
            var sellOrders = await _sellOrderRepository.GetAllOrdersByBotIdAsync(botId);

            return _mapper.Map<IEnumerable<OrderDto>>(sellOrders);
        }
    }
}
