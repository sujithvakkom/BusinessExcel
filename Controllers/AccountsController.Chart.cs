﻿using BusinessExcel.Filters;
using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using BootstrapHtmlHelper.ChartJs;
using Newtonsoft.Json;

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
                var source = db.DailyUpateView
                    .Where(x => x.CreateTime > startDate && x.CreateTime < endDate)
                    .GroupBy(x => new { x.Item, x.UserName, x.Name })
                    .Where(x => x.Sum(y => y.Quantity) > 0)
                    .Select(x =>
                        new graph()
                        {
                            Value = x.Sum(y => (decimal)y.Quantity),
                            XValues = x.Key.Item,
                            Label = x.Key.UserName.ToString() + " " + x.Key.Name.ToString()
                        });
                var data = new data<graph>(
                    chartType,
                    source,
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
                var chartType = ChartType.pie;
                var source = db.DailyUpateView
                    .Where(x => x.CreateTime > startDate && x.CreateTime < endDate)
                    .GroupBy(x => new { x.Item, x.UserName, x.Name })
                    .Where(x => x.Sum(y => y.Quantity) > 0)
                    .Select(x =>
                        new graph()
                        {
                            Value = x.Sum(y => (decimal)y.TotalValue),
                            XValues = x.Key.Item,
                            Label = x.Key.UserName.ToString() + " " + x.Key.Name.ToString()
                        });
                var data = new data<graph>(
                    chartType,
                    source,
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
