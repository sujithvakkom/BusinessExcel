using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessExcel.Controllers
{
    public class Constands
    {
        public static string _ErrorViewer = "_ErrorViewer";
        public static DateTime getQuarterStartDate(string quarter)
        {
            var q = quarter.Split(new char[] { '-' });
            return getQuarterStartDate(q[0].Trim(), q[1].Trim());
        }
        public static DateTime getQuarterStartDate(string quarter, string year)
        {
            DateTime qrt = new DateTime();

            switch (quarter)
            {
                case "Q1":
                    return qrt = Convert.ToDateTime("01-jan-" + year + "");

                case "Q2":
                    return qrt = Convert.ToDateTime("01-apr-" + year + "");

                case "Q3":
                    return qrt = Convert.ToDateTime("01-jul-" + year + "");

                case "Q4":
                    return qrt = Convert.ToDateTime("01-oct-" + year + "");

            }
            return qrt;
        }

    }
}