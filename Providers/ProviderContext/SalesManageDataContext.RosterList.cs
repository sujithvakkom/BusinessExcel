
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

          

            if (!string.IsNullOrEmpty(Filters.LocationID))
            {
                var temp = this.getLocationDetail(Filters.LocationID).location_id;
                res = res.Where(x => x.location_id == temp.ToString());
            }

            if (Filters.StartDate != default(DateTime))
            {
                res = res.Where(x => x.start_date == Filters.StartDate);
            }
            if (Filters.EndDate != default(DateTime))
            {
                res = res.Where(x => x.end_date == Filters.EndDate);

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
                Filters = new Models.ActionViewFilters() { Location = "" };
            var res = from x in this.Roster
                      where (String.IsNullOrEmpty(Filters.Location) || x.user_name == Filters.UserName)
                      select new { user_name= x.user_name, start_date = x.start_date.Value, end_date=x.end_date.Value, location_id=x.location_id, location_name=x.location_name };
            return res.ToList();
        }
    }
}
