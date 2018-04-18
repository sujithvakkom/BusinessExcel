
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Models
{
    public class RoleContext : DbContext
    {
        public RoleContext() 
            : base("BusinessExcelData")
        {
        }
        
    }
}
