using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;



namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [Table(name: "user_tree", Schema = "db_salesmanage_user")]
    public class UserTree
    {
        public UserTree()
        {
            this.Childs = new List<UserTree>();


        }

        [Required]
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 parent_id { get; set; }


        //[Required]
        // [Key, Column(Order = 1)]
        [Display(Name = "User ID")]
        public int user_id { get; set; }


       // [Key, Column(Order = 2)]
        [Display(Name = "Left")]
        public int left_v { get; set; }



       // [Key, Column(Order = 3)]
        [Display(Name = "Right")]
        public int right_v { get; set; }


        // [Key, Column(Order = 3)]
        [Display(Name = "Level")]
        public int level_v { get; set; }



        //[Required]
        // [Key, Column(Order = 1)]
        [Display(Name = "Entity")]
        public int entity { get; set; }


        //[Required]
        // [Key, Column(Order = 1)]
        [Display(Name = "Name")]
        public string u_name { get; set; }



        //[Required]
        // [Key, Column(Order = 1)]
        [Display(Name = "User Name")]
        public string user_name { get; set; }


        public ICollection<UserTree> Childs { get; set; }

    }
}
