using BusinessExcel.Providers.ProviderContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessExcel.Controllers.API
{
    public class DeliveryAPIController : ApiController
    {
        [HttpPost]

        public string AddDelivery(string itemcode, string customerName, string customerPhone, string customerEmirate, string pONumber,int quantity, string userName)
        {
            string result = null;
            using (SalesManageDataContext db = new SalesManageDataContext("GSLSR")) 
            {
                result = db.AddDelivery(itemcode, customerName, customerPhone,
                    customerEmirate,pONumber,quantity, userName);
            }
            return result;
        }
    }
}
