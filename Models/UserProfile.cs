using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessExcel.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string UserFullName { get; set; }
    }
}
