namespace Easytrade.Model.Domain.Bots
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Bots")]
    [Index(nameof(Name), IsUnique = true)]
    public class BotEntity
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        
        public decimal? Allocation { get; set; }

        public decimal? TotalProfit { get; set; }

        public decimal? DayProfit { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [MinLength(1)]
        public List<string> Symbols { get; set; }

        [Range(1, 20)]
        public int MaxOpenPositions { get; set; }

        public decimal? MinimumAllocation { get; set; }

    }
}
