using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public partial class MobileTargetAPIController : System.Web.Http.ApiController
    {
        [HttpGet]
        public List<UserTargets> GetAllUserTarget(int UserId)
        {
            UserTargetDetailsView result;
            UserTargetDetailsView x= new UserTargetDetailsView();
            x.UserID = UserId;
            int temp = UserId;
            using (SalesManageDataContext db = new SalesManageDataContext())
            {
                result = db.getUserTargetDetails(x);
                result = db.getUserTargets(temp);
            }
            return result.UserTargets;
        }

    }
}
