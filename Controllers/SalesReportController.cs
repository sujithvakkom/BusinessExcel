using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public class SalesReportController : Controller
    {
        //
        // GET: /SalesReport/

        public ActionResult SalesCategoryWiseIndex()
        {
            return View();
        }

    }
}
