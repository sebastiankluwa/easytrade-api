namespace Easytrade.Model.DbAccess
{
    using Easytrade.Model.Domain.Bots;
    using System;

    public static class EasyTradeSeedData
    {
        public static Bot[] GetBotsDefaults()
        {
            return new[]
            {
                new Bot
                {
                    Id = 1,
                    Name = "MACD Bot",
                    Allocation = 100000,
                    TotalProfit = 12000,
                    DayProfit = 0,
                    IsActive = true,
                    Symbols = new string[] { "BTCUSDT" },
                    MaxOpenPositions = 5,
                    MinimumAllocation = 10
                },
                new Bot
                {
                    Id = 2,
                    Name = "RSI Bot",
                    Allocation = 15000,
                    TotalProfit = 3000,
                    DayProfit = 0,
                    IsActive = true,
                    Symbols = new string[] { "ETHUSDT" },
                    MaxOpenPositions = 2,
                    MinimumAllocation = 5
                },
                new Bot
                {
                    Id = 3,
                    Name = "EMA Bot",
                    Allocation = 75,
                    TotalProfit = 0,
                    DayProfit = 0,
                    IsActive = false,
                    Symbols = new string[] { "LTCUSDT" },
                    MaxOpenPositions = 3,
                    MinimumAllocation = 7.5m
                }
            };
        }

        public static BuyOrder[] GetBuyOrdersDefaults()
        {
            return new[]
            {
                new BuyOrder
                {
                    Id = 1,
                    BotId = 1,
                    ProfitLossId = 1,
                    ReferenceOrderId = 1,
                    Side = "Buy",
                    OrderDate = new DateTime(2022, 12, 1, 13, 1, 1, DateTimeKind.Utc),
                    Amount = 10,
                    Fee = 0.5m,
                    Pair = "BTCUSDT",
                    Rate = 10000,
                    Status = OrderStatus.Filled
                },
                new BuyOrder
                {
                    Id = 2,
                    BotId = 1,
                    ProfitLossId = 2,
                    ReferenceOrderId = 2,
                    Side = "Buy",
                    OrderDate = new DateTime(2022, 12, 3, 13, 1, 1, DateTimeKind.Utc),
                    Amount = 20,
                    Fee = 1m,
                    Pair = "BTCUSDT",
                    Rate = 1000,
                    Status = OrderStatus.Filled
                },
                new BuyOrder
                {
                    Id = 3,
                    BotId = 2,
                    ProfitLossId = 3,
                    ReferenceOrderId = 3,
                    Side = "Buy",
                    OrderDate = new DateTime(2022, 12, 5, 13, 1, 1, DateTimeKind.Utc),
                    Amount = 30,
                    Fee = 1.5m,
                    Pair = "ETHUSDT",
                    Rate = 400,
                    Status = OrderStatus.Filled
                },
                new BuyOrder
                {
                    Id = 4,
                    BotId = 2,
                    ProfitLossId = null,
                    ReferenceOrderId = 4,
                    Side = "Buy",
                    OrderDate = new DateTime(2022, 12, 7, 13, 1, 1, DateTimeKind.Utc),
                    Amount = 40,
                    Fee = 2m,
                    Pair = "ETHUSDT",
                    Rate = 200,
                    Status = OrderStatus.Pending
                }
            };
        }

        public static SellOrder[] GetSellOrdersDefaults()
        {
            return new[]
            {
                new SellOrder
                {
                    Id = 1,
                    BotId = 1,
                    ProfitLossId = 1,
                    ReferenceOrderId = 1,
                    Side = "Sell",
                    OrderDate = new DateTime(2022, 12, 2, 13, 1, 1, DateTimeKind.Utc),
                    Amount = 10,
                    Fee = 0.5m,
                    Pair = "BTCUSDT",
                    Rate = 11000,
                    Status = OrderStatus.Filled
                },
                new SellOrder
                {
                    Id = 2,
                    BotId = 1,
                    ProfitLossId = 2,
                    ReferenceOrderId = 2,
                    Side = "Sell",
                    OrderDate = new DateTime(2022, 12, 4, 13, 1, 1, DateTimeKind.Utc),
                    Amount = 20,
                    Fee = 1m,
                    Pair = "BTCUSDT",
                    Rate = 1100,
                    Status = OrderStatus.Filled
                },
                new SellOrder
                {
                    Id = 3,
                    BotId = 2,
                    ProfitLossId = 2,
                    ReferenceOrderId = 2,
                    Side = "Sell",
                    OrderDate = new DateTime(2022, 12, 6, 13, 1, 1, DateTimeKind.Utc),
                    Amount = 30,
                    Fee = 2m,
                    Pair = "ETHUSDT",
                    Rate = 500,
                    Status = OrderStatus.Filled
                }
            };
        }

        public static ProfitLoss[] GetProfitsLossesDefaults()
        {
            return new[]
            {
                new ProfitLoss
                {
                    Id = 1,
                    BuyOrderId = 1,
                    SellOrderId = 1,
                    Result = 10000,
                    CompletionDate = new DateTime(2022, 12, 2, 13, 1, 1, DateTimeKind.Utc)
                },
                new ProfitLoss
                {
                    Id = 2,
                    BuyOrderId = 2,
                    SellOrderId = 2,
                    Result = 2000,
                    CompletionDate = new DateTime(2022, 12, 4, 13, 1, 1, DateTimeKind.Utc)
                },
                new ProfitLoss
                {
                    Id = 3,
                    BuyOrderId = 3,
                    SellOrderId = 3,
                    Result = 3000,
                    CompletionDate = new DateTime(2022, 12, 6, 13, 1, 1, DateTimeKind.Utc)
                }
            };
        }
    }
}
