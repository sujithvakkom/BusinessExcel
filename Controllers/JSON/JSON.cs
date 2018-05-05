using BusinessExcel.Providers.ProviderContext;
using DBSalesManage;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace BusinessExcel.Controllers.JSON
{
    public class JSONController : Controller
    {
        //
        // GET: /JSON/

        public JsonResult ItemAutoCompleter(String Search)
        {
            JsonResult res = null;
            using (var db = new SalesManageDataContext()) {
                var row_count = new SqlParameter("row_count",typeof(int));
                


                    //db.getItemDetailsImport(null, null, null);
                var y = db.Database.SqlQuery<getItemDetails>(
                    "[sc_salesmanage_merchant].[getItemDetailsTemp] @item_code",
                    (Search != null ?
                    new SqlParameter("@item_code", Search) :
                    new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) )).ToList();

                //var y = x.ToList();


                //var x = db.getItemDetailsCall("101", 20, row_count); 
                res = Json(y, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
