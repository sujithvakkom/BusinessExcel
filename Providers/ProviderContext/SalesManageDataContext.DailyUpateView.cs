
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

        public IQueryable<DailyUpateView> DailyUpateViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count, ActionViewFilters Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;
            if (Filters == null)
                Filters = new Models.ActionViewFilters() { ItemCode = "" };

            var res = from x in this.DailyUpateView
                      where (Filters.ItemCode==null || x.Item == Filters.ItemCode || Filters.ItemCode == "")
                      select x;

            count = res.Count();

            if (sort == null || sort == "")
            {

                return res.OrderBy(x => x.UserId)
                    .Skip(skippingRows).Take(pageSize);
            }
            else {
                switch (sortdir)
                {
                    case "DESC":
                        return res.OrderByDescending(x => x.CreateTime)
                            .Skip(skippingRows).Take(pageSize);
                    default:
                        return res.OrderBy(x => x.CreateTime)
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

        public IQueryable<DailyUpateView> DailyUpateViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
        {
            int skippingRows = (pageNumber - 1) * pageSize;

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
        }

        internal object GetDailyUpateViewPagingExport()
        {
            return this.DailyUpateView.ToList();
        }
    }
}
