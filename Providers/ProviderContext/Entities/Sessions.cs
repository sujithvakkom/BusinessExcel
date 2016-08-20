using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    [Table("Sessions")]
    public class Session
    {
        [Required]
        public string SessionId { get; set; }

        public string ApplicationName { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        public DateTime LockDate { get; set; }

        public int LockId { get; set; }

        public int Timeout { get; set; }

        //[Column(TypeName = "YesNo")]
        public bool Locked { get; set; }

        //[Column(TypeName = "Memo")]
        public string SessionItems { get; set; }

        [Required]
        public int Flags { get; set; }
    }
}