
using System.ComponentModel.DataAnnotations;

namespace BusinessExcel.Models
{
    public class IncentiveFactor
    {
        [Display(Name = "Account")]
        public int? Account { get; set; }

        [Display(Name = "Line")]
        public int? Line { get; set; }

        [Display(Name = "Factor")]
        public decimal? Factor{ get; set; }

        [Display(Name = "Threshold")]
        public decimal? Threshold { get; set; }
    }
}