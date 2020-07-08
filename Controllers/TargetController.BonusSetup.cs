using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Providers.Rest;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace BusinessExcel.Controllers
{
    public partial class TargetController : Controller
    {
        public static string BONUSMANAGEMENT = "BonusManagement";
        public static string BONUS_MANAGEMENT = "Weekly Unit incentive";
        public static string _BONUSSAVE = "_BonusSave";
        public static string _BONUSITEMSELECTOR = "_BonusItemSelector";

        public static string _BONUSDELETE = "_BonusDelete";
        public ActionResult BonusManagement()
        {
            using (var db = new SalesManageDataContext())
            {
                ViewBag.Weeks = db.getBonusWeeks(DateTime.Now.Year, DateTime.Now.AddDays(-35));
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(BONUSMANAGEMENT);
            }
            return View();
        }

        public static string _BONUSLINES = "_BonusLines";
        public PartialViewResult _BonusLines(BonusWeek week)
        {
            using (var db = new SalesManageDataContext())
            {
                ViewBag.Weeks = db.getBonusWeeksSettings(week);
            }
            var bonus = new BonusItemConfigModel();
            bonus.WeekId = week.Id;
            return PartialView(bonus);
        }

        public static string _CATOGERYSELECTORFORITEM = "_CatogerySelectorForItem";
        public PartialViewResult _CatogerySelectorForItem(string ItemCode, string ItemName)
        {
            ViewBag.ItemCode = ItemCode;
            ViewBag.ItemName = ItemName;
            var cat = new CategoryDetail();
            using (var db = new SalesManageDataContext())
            {
                var item = db.getItemDetails(ItemCode);
                if (item != null)
                    cat = db.getCategoryDetails((int)item.category_id);
            }
            return PartialView(cat ?? new CategoryDetail());
        }

        [HttpPost]
        public PartialViewResult _BonusSave(BonusItemConfigModel bonus)
        {
            var bonusSettings = new List<BonusSettings>();
            using (var db = new SalesManageDataContext())
            {
                var item = db.getItemDetails(bonus.Item.item_code);
                bonus.InventoryItemId = item.inventory_item_id;
                db.SaveBonusConfiguration(bonus);
                bonusSettings = db.getBonusWeeksSettings(bonus.Week);
            }
            return PartialView(bonusSettings);
        }

        [HttpDelete]
        public PartialViewResult _BonusDelete(int bonusId)
        {
            var bonusSettings = new List<BonusSettings>();
            using (var db = new SalesManageDataContext())
            {
                int weekId = db.Database.SqlQuery<int>("select [WEEKCALENDERID] id from [WEEKBONUS] where ID = @id",
                    new SqlParameter("id", bonusId)).FirstOrDefault();
                db.DeleteBonusConfiguration(bonusId);
                var bonus = new BonusItemConfigModel();
                bonus.WeekId = weekId;
                bonusSettings = db.getBonusWeeksSettings(bonus.Week);
            }
            return PartialView(_BONUSSAVE, bonusSettings);

        }

        public ActionResult BonusStaffView(string StaffCode,BonusStaffView staff)
        {
            staff.StaffCode = StaffCode;
            BonusStaffView view = null;
            using (var db = new SalesManageDataContext())
            {
                var userId = db.getUserID(staff.StaffCode);
                int rows = 0;
                var user = db.getUserDetailByName(staff.StaffCode, 1, out rows).FirstOrDefault();
                var target = db.getUserTargets(userId);
                var bonus = db.getBonusAchievements(userId);
                var weeks = db.getBonusWeeks(DateTime.Now.Year, DateTime.Now.AddDays(-14)).Take(5);
                foreach (var week in weeks) {
                   week.WeekSettings = db.getBonusWeeksSettings(week,staff.CategoyID==0?null:staff.CategoyID);
                }
                view = new BonusStaffView()
                {
                    StaffCode = staff.StaffCode,
                    User = user,
                    Target = target,
                    Bonus = bonus,
                    Weeks = weeks
                };
            }
            return View(view);
        }

        public ActionResult Dashboard(string StaffCode)
        {
            return View();
        }
    }
}