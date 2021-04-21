using BusinessExcel.Helpers;
using DataProvider;
using DataProvider.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeDelivery.Controllers.API
{
    public class GeoController : ApiController
    {
        //Post
        [HttpPost]
        public string PostUserLocation(List<LocationTracker> LocationTrackers)
        {
            string result = "Failed";
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                foreach (var loc in LocationTrackers)
                    result = db.PutUserLoction(loc);
            }
            return result;
        }
    }
}
