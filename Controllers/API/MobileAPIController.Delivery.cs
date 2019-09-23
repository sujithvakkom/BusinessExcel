using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public partial class MobileAPIController : System.Web.Http.ApiController
    {
        [HttpGet]
        public List<delivery> GetDelivery(string receipt, int status)
        {
            List<delivery> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext("GSLSR"))
            {
                result = db.GetDelivery(receipt, status, null);
            }
            return result;
        }
        [HttpGet]
        public List<delivery> GetDelivery(string receipt, int status, string driver)
        {
            List<delivery> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext("GSLSR"))
            {
                result = db.GetDelivery(receipt, status, driver);
            }
            return result;
        }

        [HttpPost]
        public string UpdateDelivery(string receipt,int status, string driver)
        {
            string result = null;
            using (SalesManageDataContext db = new SalesManageDataContext("GSLSR"))
            {
                result = db.UpdateDelivery(receipt,status,driver);
            }
            return result;
        }

        [HttpPost]
        public string UpdateLog(string receipt, int status, string driver, string remark, int id)
        {
            string result = null;
            using (SalesManageDataContext db = new SalesManageDataContext())
            {
                result = db.UpdateLog(receipt, status, driver, remark, id);
            }
            return result;
        }
    }
}
