namespace Easytrade.Model.DbAccess
{
    using Easytrade.Model.Domain.Bots;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EasyTradeDbContext : DbContext
    {
        public EasyTradeDbContext(DbContextOptions<EasyTradeDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<BotEntity> Bots { get; set; }
    }
}
