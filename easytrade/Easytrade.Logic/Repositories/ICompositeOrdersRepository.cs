﻿namespace Easytrade.Logic.Repositories
{
    using Easytrade.Contract.Dto.Orders;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompositeOrdersRepository
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersByBotId(long botId);
    }
}