
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

        public IQueryable<Roster> RosterUpateViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count, 
            ActionViewFilters Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;
           

            var res = this.Roster.Select(x=>x);
           
          
            if (!string.IsNullOrEmpty(Filters.UserName))
                res = res.Where(x => x.user_name == Filters.UserName);

          

            if (!string.IsNullOrEmpty(Filters.Location))
            {
                var temp = this.getLocationDetail(Filters.Location).description;
                res = res.Where(x => x.location_name.ToString() == temp);
            }

            count = res.Count();
            // return res.Skip(skippingRows).Take(pageSize);
            if (sort == null || sort == "")
            {
                return res.OrderByDescending(x => x.location_name).Skip(skippingRows).Take(pageSize);
            }
            else
            {
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

        }

       

        public IQueryable<Roster> RosterUpateViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
        {
            int skippingRows = (pageNumber - 1) * pageSize;

            switch (sort)
            {
                case "CreateTime":
                    count = this.DailyUpateView.Count();
                    if (sortdir == "ASC")
                        return this.Roster.Skip(skippingRows).Take(pageSize);
                    return this.Roster.Skip(skippingRows).Take(pageSize);
                default:
                    count = this.Roster.Count();
                    return this.Roster.Skip(skippingRows).Take(pageSize);
            }
        }

        internal object GetRosterUpateViewPagingExport(ActionViewFilters Filters)
        {

            if (Filters == null)
                Filters = new Models.ActionViewFilters() { ItemCode = "" };
            var res = from x in this.DailyUpateView
                      where (String.IsNullOrEmpty(Filters.ItemCode) || x.Item == Filters.ItemCode)
                      select x;
            return res.ToList();
        }
    }
}
