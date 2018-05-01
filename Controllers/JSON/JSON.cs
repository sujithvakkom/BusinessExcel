using BusinessExcel.Providers.ProviderContext;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
                var row_count = new ObjectParameter("row_count",System.Data.SqlDbType.Int);
                var x = db.getItemDetails(null, null, row_count).ToList(); ;
                res = Json(x,JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
