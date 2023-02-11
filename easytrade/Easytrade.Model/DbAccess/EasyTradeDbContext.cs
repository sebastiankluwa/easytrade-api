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

            SetupOrders(modelBuilder);
            SetupProfitsLosses(modelBuilder);
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
    }
}
