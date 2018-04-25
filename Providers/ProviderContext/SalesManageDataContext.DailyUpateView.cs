
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public IQueryable<DailyUpateView> DailyUpateViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
        {
            int skippingRows = (pageNumber - 1) * pageSize;
            
            switch (sort)
            {
                case "CreateTime":
                    count = this.DailyUpateView.Count();
                    if(sortdir == "ASC")
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
    }
}
