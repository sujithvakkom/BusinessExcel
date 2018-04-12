using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class RolesNameModel
    {
        [Required]
        [DisplayName("Role name")]
        public String RolesName { get; set; }
    }
}