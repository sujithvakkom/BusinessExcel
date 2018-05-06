using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Models
{
    public enum BERoles
    {
        SystemAdministrator = 0,
        Administrator = 1
    }
    public static class BERoleDetails
    {
        public const string SYSTEM_ADMINISTRATOR = "System Administrator";
        public const string ADMINISTRATOR =  "Administrator" ;
        public const string Manager = "Manager";

    }
}