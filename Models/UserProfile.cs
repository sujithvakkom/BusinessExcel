using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BusinessExcel.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string UserFullName { get; set; }
    }

    [Table("SessionKeeper")]
    public class SessionKeeper
    {
        [Key]
        [Column(TypeName = "nvarchar(20)")]
        public string SessionId { get; set; }

        [Key]
        [Column(TypeName = "nvarchar(50)")]
        public string SessionKey { get; set; }

        [Column(TypeName = "ntext")]
        public string SessionDataSerial { get; set; }
    }
}
