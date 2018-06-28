using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;



namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [Table(name: "user_tree", Schema = "db_salesmanage_user")]
    public class UserTree
    {

        [Required]
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 tree_id { get; set; }


        //[Required]
        // [Key, Column(Order = 1)]
        [Display(Name = "User ID")]
        public int user_id { get; set; }


       // [Key, Column(Order = 2)]
        [Display(Name = "Left")]
        public int left { get; set; }



       // [Key, Column(Order = 3)]
        [Display(Name = "Right")]
        public int right { get; set; }


        // [Key, Column(Order = 3)]
        [Display(Name = "Level")]
        public int level { get; set; }



        //[Required]
        // [Key, Column(Order = 1)]
        [Display(Name = "Entity")]
        public int entity { get; set; }

    }
}
