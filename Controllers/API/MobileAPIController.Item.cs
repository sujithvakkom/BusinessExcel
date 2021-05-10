using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public partial class MobileAPIController : System.Web.Http.ApiController
    {
        /*
        [HttpGet]
        public IEnumerable<ItemModel> GetAllProducts()
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
        [HttpGet]
        public IEnumerable<ItemModel> GetAllProducts(string Search,int Page, string UserName = null)
        {
            int RowCount;
            List<ItemModel> result;
            using (SalesManageDataContext db = new SalesManageDataContext())
            {
                result = db.getModelDetails(Search, Page, out RowCount);
            }
            return result;
        }
        */
    }
}
