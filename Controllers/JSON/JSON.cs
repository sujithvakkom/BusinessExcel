using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
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


                var item_code = Search != null ?
                    new SqlParameter("@item_code", Search) :
                    new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

                int? page = null;
                var page_size = page != null ?
                    new SqlParameter("@page_size", page) :
                    new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

                int? page_num = null;
                var page_number = page != null ?
                    new SqlParameter("@page_number", page_num) :
                    new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

                int? row = null;
                var row_count = row != null ?
                    new SqlParameter("@row_count", row) :
                    new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
                row_count.Direction = System.Data.ParameterDirection.Output;

                //db.getItemDetailsImport(null, null, null);

                var items = db.Database.SqlQuery<ItemDetails>(
                                                "[sc_salesmanage_merchant].[getItemDetails]  @item_code ,@page_number ,@page_size ,@row_count OUTPUT", item_code, page_number, page_size, row_count)
                                                .ToList();

                /*
                var item_code = new SqlParameter("@item_code", "");
                //
                //calling stored procedure to get paged data.
                var items = db.Database.SqlQuery<ItemDetails>(
                                                "[sc_salesmanage_merchant].[getItemDetailsTemp] @item_code", item_code)
                                                .ToList();
                                                */
                JSONPagininationModel<ItemDetails> model = new JSONPagininationModel<ItemDetails>();
                int temp;
                model.Count = (int.TryParse(row_count.Value.ToString(), out temp)) ? temp : 0;
                model.CountPerPage = 20;
                model.OutputList = items;
                 res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

    }
}
