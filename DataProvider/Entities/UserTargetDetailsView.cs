using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataProvider.Entities
{
    public class UserTargetDetailsView
    {
        [Column("UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Column("FullName")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Column("Location")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Column("Period")]
        [Display(Name = "Period")]
        public string Period { get; set; }

        [Column("Catogery")]
        [Display(Name = "Catogery")]
        public string Catogery { get; set; }

        [Column("Target")]
        [Display(Name = "Target")]
        public decimal? Target { get; set; }

        [Column("Achievement")]
        [Display(Name = "Achievement")]
        public decimal? Achievement { get; set; }

        [Display(Name = "Lines")]
        public List<UserTargetDetailsView> Lines { get; internal set; }
        public decimal? TotalIncAmt { get; set; }
        public decimal? TotalAmountAcc { get; set; }
    }
}