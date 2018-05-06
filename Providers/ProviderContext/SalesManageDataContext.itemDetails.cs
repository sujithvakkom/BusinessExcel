
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }


        public virtual DbRawSqlQuery<ItemDetails> getItemDetails(string item_code//, 
            //Nullable<int> page_size, ObjectParameter row_count
            )
        {
            var item_codeParameter = item_code != null ?
                new SqlParameter("item_code", item_code) :
                new SqlParameter("item_code",System.Data.SqlDbType.NVarChar);

            //var page_sizeParameter = page_size.HasValue ?
            //    new ObjectParameter("page_size", page_size) :
            //    new ObjectParameter("page_size", typeof(int));

            return this.Database.SqlQuery<ItemDetails>("[sc_salesmanage_merchant].[getItemDetailsTemp] @item_code", item_codeParameter
                //, page_sizeParameter, row_count
                );
        }

        /*
        public IQueryable<ItemDetails> GetItemDetails(int pageNumber, int pageSize, string sort, String sortdir, out int count, 
            ActionViewFilters Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;
            if (Filters == null)
                Filters = new Models.ActionViewFilters() { ItemCode = "" };

            var res = from x in this.ItemDetails
                      where (String.IsNullOrEmpty(Filters.ItemCode) || x.Item == Filters.ItemCode)
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
        /*
    }

    public IQueryable<DailyUpateView> GetItemDetailsViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
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
*/
        //internal object GetDailyUpateViewPagingExport(ActionViewFilters Filters)
        //{

        //    if (Filters == null)
        //        Filters = new Models.ActionViewFilters() { ItemCode = "" };
        //    var res = from x in this.DailyUpateView
        //              where (String.IsNullOrEmpty(Filters.ItemCode) || x.Item == Filters.ItemCode)
        //              select x;
        //    return res.ToList();
        //}
    }
}