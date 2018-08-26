using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [ComplexType()]
    public class CategoryDetail
    {
        [Display(Name ="Line")]
        [Required]
        [Column("category_id", Order = 1)]
        public int category_id { get; set; }

        [Display(Name ="Description")]
        [Column("description", Order = 2)]
        public string description { get; set; }

        [Display(Name = "Base Incentive Pct.")]
        [Required]
        [Column("base_incentive", Order = 3)]
        public decimal? base_incentive { get; set; }

        [Display(Name = "Total Sale Factor")]
        [Required]
        [Column("total_sale_factor", Order = 4)]
        public decimal? total_sale_factor { get; set; }

        [Display(Name = "Category Sell-in Factor")]
        [Required]
        [Column("category_sellin_factor", Order = 4)]
        public decimal? category_sellin_factor { get; set; }
    }
}