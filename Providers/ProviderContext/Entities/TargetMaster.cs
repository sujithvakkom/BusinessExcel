using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [Table(name: "target_m", Schema = "db_salesmanage_user")]
    public class TargetMaster
    {

        [Required]
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Target  Id")]
        public int target_id { get; set; }

        

    

        
        [Display(Name = "Target Name")]
        public string description { get; set; }

     
        [Display(Name = "Total Incentive (%)")]
        public decimal incentive_pct { get; set; }



    }
}