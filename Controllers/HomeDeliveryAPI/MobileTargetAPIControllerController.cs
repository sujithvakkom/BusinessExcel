using DataProvider;
using DataProvider.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeDelivery.Controllers.API
{
    public class MobileTargetAPIController : ApiController
    {
        [HttpGet()]
        public List<UserTargetDetailsView> UserTarget(string UserId)
        {
            List<UserTargetDetailsView> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext("SALES_MANAGER"))
            {
                result = db.getUserTargetDetails(UserId);
            }
            return result;
        }
    }
}
