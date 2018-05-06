
using System;
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
            //using (var db = new DBSalesmanageEntities("SalesManageData")) {
            //    var row_count = new ObjectParameter("row_count",typeof(int));
            //    IEnumerable<getItemDetails_Result> x = db.getItemDetails(null, null, row_count); ;
            //    res = Json(x.ToList(), JsonRequestBehavior.AllowGet);
            //}
            return res;
        }

    }
}
