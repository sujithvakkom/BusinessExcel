using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public class BackupTargetFilter
    {
        [Display(Name = "Month")]
        [DataType(DataType.Date)]
        public string Month { get; set; }
    }
}