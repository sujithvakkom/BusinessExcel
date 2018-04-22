using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [Table("UserProfile")]
    public class UserProfile
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]         
        [Column(TypeName = "Int")]
        public int UserId { get; set; }
         
        //[Column(TypeName = "String")]
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string UserName { get; set; }
        

        //[Column(TypeName = "String")]
        [Display(Name = "Full Name")]
        [StringLength(50)]
        public string UserFullName { get; set; }
    }
}
