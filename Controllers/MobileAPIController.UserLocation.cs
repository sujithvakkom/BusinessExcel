using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public partial class MobileAPIController : System.Web.Http.ApiController
    {
        //Post
        [HttpPost]
        public UserLocation PostUserLocation(UserLocation UserLocation)
        {
            string result = "Failed";
            using (SalesManageDataContext db = new SalesManageDataContext())
            {
                result = db.PutUserLoction(UserLocation);
            }
            return UserLocation;
        }
    }
}
