using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public static class Util
    {
        public static String getStatusDes(int status)
        {
            switch (status)
            {
                case 1:
                    return "New Sale";
                case 2:
                    return "ERP Entered";
                case 3:
                    return "ERP Picked";
                case 4:
                    return "Backordered";
                case 5:
                    return "Ready to Delivery";
                case 6:
                    return "Cancelled";
                case 7:
                    return "Scheduled";
                case 8:
                    return "Vehicle Assigned";
                case 9:
                    return "Loaded";
                case 10:
                    return "On Hold";
                case 11:
                    return "Delivered";
                case 12:
                    return "Delivery Cancelled";
                case 13:
                    return "Closed";
                case 20:
                    return "Pending";
                case 14:
                    return "Rescheduled";
                case 15:
                    return "Processing";
                case 16:
                    return "Rejected";

                default: return "Missing";
            }
        }
    }
}
