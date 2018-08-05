using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace BusinessExcel.Controllers.JSON
{
    public partial class JSONController : Controller
    {
        public static string USER_BY_OTHER_AUTOCOMPLETER = "UserByOtherAutoCompleter";
        public static string NON_ASSIGNED_USERAUTOCOMPLETER = "nonAssignedUserAutoCompleter";
        public JsonResult UserByOtherAutoCompleter(String Search,int Page)
        {
            JsonResult res = null;
            using (var db = new SalesManageDataContext()) {
                int temp;

                var items = db.getUserDetailByName(search: Search, Page: Page,RowCount:out temp);
                JSONPagininationModel<UserDetail> model = new JSONPagininationModel<UserDetail>();
                model.CountPerPage = 20;
                model.OutputList = items;
                model.Count = temp;
                res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }

        /// <summary>
        /// Returns non assigned users to managers or parents
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="Page"></param>
        /// <param name="Extra"></param>
        /// <returns></returns>
        public JsonResult nonAssignedUserAutoCompleter(String Search, int Page, string Extra)
        {
            JsonResult res = null;

           

            using (var db = new SalesManageDataContext())
            {
                int temp;

                var items = db.getnonAssingedUserDetails(search: Search, Page: Page, RowCount: out temp);
                JSONPagininationModel<UserDetail> model = new JSONPagininationModel<UserDetail>();
                model.CountPerPage = 20;
                model.OutputList = items;
                model.Count = temp;
                res = Json(model, JsonRequestBehavior.AllowGet);
            }
            return res;
        }


    }
}
