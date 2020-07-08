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
                UserLocation.UserID = db.getUserID(UserLocation.UserName);
                result = db.PutUserLoction(UserLocation);
            }
            return UserLocation;
        }

        //Post
        [HttpGet]
        public UserLocation GetUserLocation(string UserName)
        {
            UserLocation result = null;
            using (SalesManageDataContext db = new SalesManageDataContext())
            {
                var userId = db.getUserID(UserName);
                result = db.GetUserLoction(userId);
            }
            return result;
        }

    }
}
