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
        [Display(Name = "Role name")]
        public String RolesName { get; set; }
    }
}