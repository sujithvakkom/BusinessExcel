using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public partial class MobileAPIController : System.Web.Http.ApiController
    {
        [HttpGet]
        public UserDetail GetAuthFor(string UserName,string Password)
        {
            UserDetail result = null;
            using (SalesManageDataContext db = new SalesManageDataContext("GSLSR"))
            {
                result = db.getAuthUserDetail(UserName,Password);
            }
            return result;
        }

    }
}
