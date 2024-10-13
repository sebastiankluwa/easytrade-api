namespace Easytrade.Model.DbAccess
{
    using Easytrade.Model.Domain.Bots;
    using Microsoft.EntityFrameworkCore;

    public class EasyTradeDbContext : DbContext
    {
        public EasyTradeDbContext(DbContextOptions<EasyTradeDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Bot> Bots { get; set; }
        public DbSet<BuyOrder> BuyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }
        public DbSet<ProfitLoss> ProfitsLosses{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Bot>()
            //    .Property(r => r.Symbols)
            //    .HasConversion(
            //        v => string.Join(",", v),
            //        v => v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            SetupBots(modelBuilder);
            SetupOrders(modelBuilder);
            SetupProfitsLosses(modelBuilder);

            SeedBots(modelBuilder);
            SeedBuyOrders(modelBuilder);
            SeedSellOrders(modelBuilder);
            SeedProfitsLosses(modelBuilder);
        }

        private static void SetupBots(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bot>()
                .HasMany(o => o.SellOrders)
                .WithOne(pl => pl.Bot)
                .HasForeignKey(o => o.BotId);

            modelBuilder.Entity<Bot>()
                .HasMany(o => o.BuyOrders)
                .WithOne(pl => pl.Bot)
                .HasForeignKey(o => o.BotId);
        }

        private static void SetupOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyOrder>()
                .HasOne(o => o.ProfitLoss)
                .WithOne(pl => pl.BuyOrder)
                .HasForeignKey<BuyOrder>(o => o.ProfitLossId);

            modelBuilder.Entity<SellOrder>()
                .HasOne(o => o.ProfitLoss)
                .WithOne(pl => pl.SellOrder)
                .HasForeignKey<SellOrder>(o => o.ProfitLossId);

            modelBuilder.Entity<SellOrder>()
                .HasOne(o => o.Bot)
                .WithMany(p => p.SellOrders)
                .HasForeignKey(p => p.BotId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BuyOrder>()
                .HasOne(o => o.Bot)
                .WithMany(p => p.BuyOrders)
                .HasForeignKey(p => p.BotId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void SetupProfitsLosses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfitLoss>()
                .HasOne(pl => pl.BuyOrder)
                .WithOne(o => o.ProfitLoss)
                .HasForeignKey<ProfitLoss>(pl => pl.BuyOrderId);

            modelBuilder.Entity<ProfitLoss>()
                .HasOne(pl => pl.SellOrder)
                .WithOne(o => o.ProfitLoss)
                .HasForeignKey<ProfitLoss>(pl => pl.SellOrderId);
        }

        private static void SeedBots(ModelBuilder modelBuilder)
        {
            var seedData = EasyTradeSeedData.GetBotsDefaults();
            modelBuilder.Entity<Bot>().HasData(seedData);
        }

        private static void SeedBuyOrders(ModelBuilder modelBuilder)
        {
            var seedData = EasyTradeSeedData.GetBuyOrdersDefaults();
            modelBuilder.Entity<BuyOrder>().HasData(seedData);
        }

        private static void SeedSellOrders(ModelBuilder modelBuilder)
        {
            var seedData = EasyTradeSeedData.GetSellOrdersDefaults();
            modelBuilder.Entity<SellOrder>().HasData(seedData);
        }

        private static void SeedProfitsLosses(ModelBuilder modelBuilder)
        {
            var seedData = EasyTradeSeedData.GetProfitsLossesDefaults();
            modelBuilder.Entity<ProfitLoss>().HasData(seedData);
        }
    }
}
