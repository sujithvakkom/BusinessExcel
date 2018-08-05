using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public partial class MobileAPIController : System.Web.Http.ApiController
    {
        [HttpGet]
        public IEnumerable<ItemModel> GetAllUserTarget(string UserName,DateTime Date)
        {
            string Search = "";
            int RowCount;
            List<ItemModel> result;
            using (SalesManageDataContext db = new SalesManageDataContext())
            {
                result = db.getModelDetails(Search, 1, out RowCount);
            }
            return result;
        }

    }
}
