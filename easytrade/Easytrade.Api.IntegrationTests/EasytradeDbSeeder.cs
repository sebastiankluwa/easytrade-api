namespace Easytrade.Api.IntegrationTests
{
    using Easytrade.Model.DbAccess;

    public class EasytradeDbSeeder
    {
        internal static void SeedData(EasyTradeDbContext db)
        {
            db.Bots.RemoveRange(db.Bots);

            db.Bots.AddRange(EasyTradeSeedData.GetBotsDefaults());

            db.SaveChanges();

            db.BuyOrders.RemoveRange(db.BuyOrders);

            db.BuyOrders.AddRange(EasyTradeSeedData.GetBuyOrdersDefaults());

            db.SaveChanges();

            db.SellOrders.RemoveRange(db.SellOrders);

            db.SellOrders.AddRange(EasyTradeSeedData.GetSellOrdersDefaults());

            db.SaveChanges();

            db.ProfitsLosses.RemoveRange(db.ProfitsLosses);

            db.ProfitsLosses.AddRange(EasyTradeSeedData.GetProfitsLossesDefaults());

            db.SaveChanges();
        }
    }
}
