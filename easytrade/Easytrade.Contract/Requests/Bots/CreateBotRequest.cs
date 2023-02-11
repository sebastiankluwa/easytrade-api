namespace Easytrade.Contract.Requests.Bots
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateBotRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public decimal Allocation { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required, MinLength(1)]
        public List<string> Symbols { get; set; }

        [Range(1,20)]
        public int MaxOpenPositions { get; set; }

        [Required]
        public decimal MinimumAllocation { get; set; }
    }
}
