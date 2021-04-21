using BusinessExcel.Helpers;
using DataProvider;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeDelivery.Controllers.API
{
    public class RetailerInfoProviderController : ApiController
    {
        [HttpGet]
        public List<string> GetRetailer()
        {
            List<String> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getRetailers();
            }
            return result;
        }
    }
}