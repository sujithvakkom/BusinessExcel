using BusinessExcel.Providers.ProviderContext;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Controllers.JSON
{
    public class JSONController : Controller
    {
        //
        // GET: /JSON/

        public ActionResult ItemAutoCompleter(String Search)
        {
            using (var db = new SalesManageDataContext()) {
                ObjectParameter rowCount = new ObjectParameter("rowCount",0);
                var x = db.getItemDetails("", null, rowCount);
            }
            return View();
        }

    }
}
