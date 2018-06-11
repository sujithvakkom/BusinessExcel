
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Models;
using BusinessExcel.Extentions;
namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public IQueryable<TargetAchievementView> TargetAchievementViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count, 
            ActionViewFilters Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;
            if (Filters == null)
                Filters = new Models.ActionViewFilters() { ItemCode = "" };

            var res = this.TargetAchievementView.Select(x=>x);
            //if (!String.IsNullOrEmpty(Filters.ItemCode)) {
            //    res = res.Where(x => x.Item == Filters.ItemCode);
            //}

            if (Filters.StartDate != default(DateTime))
            {
                res = res.Where(x => x.fromdate == Filters.StartDate);
            }
            if (Filters.EndDate != default(DateTime))
            {
                res = res.Where(x => x.todate == Filters.EndDate);

            }

            //if (Filters.StartDate.Date != DateTime.MinValue.Date)
            //    if (Filters.EndDate.Date != DateTime.MinValue.Date)
            //        res = res.Where(x => x.CreateTime >= Filters.StartDate && x.CreateTime < Filters.EndDate);
            //    else
            //        res = res.Where(x => x.CreateTime > Filters.StartDate);

            if (!string.IsNullOrEmpty(Filters.UserName))
                res = res.Where(x => x.UserName == Filters.UserName);

            //if (!string.IsNullOrEmpty(Filters.BrandID))
            //    res = res.Where(x => x.BrandId.ToString() == Filters.BrandID);


            if (!string.IsNullOrEmpty(Filters.Location))
            {
                var temp = this.getLocationDetail(Filters.Location).description;
                res = res.Where(x => x.Location.ToString() == temp);
            }

            count = res.Count();

            if (sort == null || sort == "")
            {
                return res.OrderByDescending(x => x.fromdate)
                    .Skip(skippingRows).Take(pageSize);
            }
            else {
                switch (sortdir)
                {
                    case "DESC":
                        return res.OrderByDescending(sort)
                            .Skip(skippingRows).Take(pageSize);
                    default:
                        return res.OrderBy(sort)
                            .Skip(skippingRows).Take(pageSize);
                }
            }
            /*
            switch (sort)
            {
                case "CreateTime":
                    count = this.DailyUpateView.Count();
                    if (sortdir == "ASC")
                        return this.DailyUpateView.OrderBy(x => x.CreateTime)
                            .Skip(skippingRows).Take(pageSize);
                    return this.DailyUpateView.OrderByDescending(x => x.CreateTime)
                        .Skip(skippingRows).Take(pageSize);

                default:
                    count = this.DailyUpateView.Count();
                    return this.DailyUpateView.OrderBy(x => x.UserId)
                        .Skip(skippingRows).Take(pageSize);
            }
            */
        }


        public IQueryable<TargetAchievementView> TargetAchievementViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
        {
            int skippingRows = (pageNumber - 1) * pageSize;

            switch (sort)
            {
                case "CreateTime":
                    count = this.TargetAchievementView.Count();
                    if (sortdir == "ASC")
                        return this.TargetAchievementView.OrderBy(x => x.fromdate)
                            .Skip(skippingRows).Take(pageSize);
                    return this.TargetAchievementView.OrderByDescending(x => x.fromdate)
                        .Skip(skippingRows).Take(pageSize);
                default:
                    count = this.TargetAchievementView.Count();
                    return this.TargetAchievementView.OrderBy(x => x.UserName)
                        .Skip(skippingRows).Take(pageSize);
            }
        }

        internal object GetTargetAchievementViewPagingExport(ActionViewFilters Filters)
        {
            // var res = this.TargetAchievementView.Select(x => x);


            var res = this.TargetAchievementView.Select(x => new { Name = x.Name, USER_NAME = x.UserName, Location = x.Location, FromDate = x.fromdate, ToDate = x.todate, Target = x.target_amt, Achieved = x.achieved_amt });
      

            if (Filters.StartDate != default(DateTime))
            {
                res = res.Where(x => x.FromDate == Filters.StartDate);
            }
            if (Filters.EndDate != default(DateTime))
            {
                res = res.Where(x => x.ToDate == Filters.EndDate);

            }

            if (!string.IsNullOrEmpty(Filters.UserName))
                res = res.Where(x => x.USER_NAME == Filters.UserName);

       

            if (!string.IsNullOrEmpty(Filters.Location))
            {
                var temp = this.getLocationDetail(Filters.Location).description;
                res = res.Where(x => x.Location.ToString() == temp);
            }

            //if (Filters == null)
            //    Filters = new Models.ActionViewFilters() { ItemCode = "" };
            //var res = from x in this.TargetAchievementView
            //              where ( x.UserName==Filters.UserName || x.Location==Filters.Location)
            //          select new { Name=x.Name, USER_NAME = x.UserName,Location=x.Location,FromDate=x.fromdate,ToDate=x.todate,Target=x.target_amt, Achieved=x.achieved_amt };
            return res.ToList();
        }
    }
}
