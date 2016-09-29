using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public enum BERoles
    {
        SystemAdministrator,
        Administrator
    }
    public static class BERoleDetails
    {
        public static Dictionary<BERoles, String> RoleName = new Dictionary<BERoles, string>() { 
        {BERoles.SystemAdministrator,"System Administrator"},
        {BERoles.Administrator,"Administrator"}
        };
    }
}