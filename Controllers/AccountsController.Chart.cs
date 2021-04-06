using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using System;
using System.Linq;
using System.Web.Mvc;
using BootstrapHtmlHelper.ChartJs;

namespace BusinessExcel.Controllers
{
    //[Authorize]
    //[InitializeSimpleMembership]
    public partial class AccountsController : Controller
    {
        readonly DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        readonly DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
        private chart<graph> GetGraphForCurrentMonthQuantity()
        {
            using (var db = new SalesManageDataContext())
            {
                var chartType = ChartType.bar;
                int viwer = db.getViewer_Id();
                var view = db.getViewersList();

                var source = db.GetGraphForCurrentMonthQuantity(viwer,startDate,endDate).ToList();
                var data = new data<graph>(
                    chartType,
                    source.AsQueryable(),
                    x => x.XValues.ToString(),
                    x => x.Label,
                    x => new PointPair { Key = x.XValues.ToString(), Value = (decimal)x.Value }
                    );
                var options = new options()
                {

                };
                var chart = new chart<graph>(chartType,
                    data,
                    null
                    );
                
                return chart;
            }
        }

        private chart<graph> GetGraphForCurrentMonthValue()
        {
            using (var db = new SalesManageDataContext())
            {
                int viwer = db.getViewer_Id();
                var chartType = ChartType.bar;

                var source = db.GetGraphForCurrentMonthValue(viwer,startDate,endDate).ToList();
                    var data = new data<graph>(
                    chartType,
                    source.AsQueryable(),
                    x => x.Label.ToString(),
                    x => x.XValues,
                    x => new PointPair {
                        Key = x.Label, 
                        Value = (decimal)x.Value }
                    );
                var options = new options()
                {
                    
                };
                var chart = new chart<graph>(chartType,
                    data,
                    null
                    );
                
                return chart;
            }
        }

        public static string TABLE_FOR_CURRENT_MONTH_QUANTITY = "TableForCurrentMonthQuantity";
        [HttpGet]
        public PartialViewResult TableForCurrentMonthQuantity()
        {
            return PartialView(TABLE_FOR_CURRENT_MONTH_QUANTITY, new DateTimeSpan()
            {
                StartDate = startDate,
                EndDate = endDate
            });
        }
        public static string TABLE_FOR_CURRENT_MONTH_VALUE = "TableForCurrentMonthValue";
        [HttpGet]
        public PartialViewResult TableForCurrentMonthValue()
        {
            return PartialView(TABLE_FOR_CURRENT_MONTH_VALUE, new DateTimeSpan()
            {
                StartDate = startDate,
                EndDate = endDate
            });
        }
    }
}
