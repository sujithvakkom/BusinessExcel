
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Models;
using BusinessExcel.Extentions;
using System.Data.SqlClient;
using System.Collections.Generic;



namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public IQueryable<DailyUpateView> DailyUpateViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count, 
            ActionViewFilters Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;


            int VId = getViewer_Id();

            var viewer_id = VId > 0 ?
              new SqlParameter("@viewer_id", VId) :
              new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            var item_code = Filters.ItemCode != null ?
           new SqlParameter("@item_code", Filters.ItemCode) :
           new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var start_date = Filters.StartDate != default(DateTime) ?
         new SqlParameter("@start_date", Filters.StartDate) :
         new SqlParameter("@start_date", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var end_date = Filters.EndDate != default(DateTime) ?
         new SqlParameter("@end_date", Filters.EndDate) :
         new SqlParameter("@end_date", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

          var user_id = Filters.UserID != null ?
         new SqlParameter("@user_id", Filters.UserID) :
         new SqlParameter("@user_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

                    var brand_id = Filters.BrandID != null ?
        new SqlParameter("@brand_id", Filters.BrandID) :
        new SqlParameter("@brand_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


                    var location_id = Filters.LocationID != null ?
        new SqlParameter("@location_id ", Filters.LocationID) :
        new SqlParameter("@location_id ", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };




            var page_number = pageNumber > 0 ?
               new SqlParameter("@page_number", pageNumber) :
               new SqlParameter("@page_number", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            var page_size = pageSize > 0 ?
                new SqlParameter("@page_size", pageSize) :
                new SqlParameter("@page_size", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;

            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(viewer_id);
            parameterList.Add(item_code);
            parameterList.Add(start_date);
            parameterList.Add(end_date);
            parameterList.Add(user_id);
            parameterList.Add(brand_id);
            parameterList.Add(location_id);

            parameterList.Add(page_number);
            parameterList.Add(page_size);
            parameterList.Add(row_count);

            var res = this.Database.SqlQuery<DailyUpateView>("[db_salesmanage_user].[get_actions] @item_code,@start_date,@end_date,@user_id,@brand_id,@location_id,@page_number,@page_size,@row_count OUTPUT, @viewer_id", parameterList.ToArray()).ToList().AsQueryable();
            int.TryParse(row_count.Value.ToString(), out count);

            // count = res.Count();
            // return res.Skip(skippingRows).Take(pageSize);
            if (sort == null || sort == "")
            {
                return res.OrderByDescending(x => x.Item);
            }
            else
            {
                switch (sortdir)
                {
                    case "DESC":
                        return res.OrderByDescending(sort);
                    default:
                        return res.OrderBy(sort);
                }
            }




            //if (Filters == null)
            //    Filters = new Models.ActionViewFilters() { ItemCode = "" };

            //var res = this.DailyUpateView.Select(x=>x);
            //if (!String.IsNullOrEmpty(Filters.ItemCode)) {
            //    res = res.Where(x => x.Item == Filters.ItemCode);
            //}
            //if (Filters.StartDate.Date != DateTime.MinValue.Date)
            //    if(Filters.EndDate.Date != DateTime.MinValue.Date)
            //        res = res.Where(x => x.CreateTime >= Filters.StartDate && x.CreateTime < Filters.EndDate);
            //    else
            //        res = res.Where(x => x.CreateTime > Filters.StartDate);
            //if (!string.IsNullOrEmpty(Filters.UserName))
            //    res = res.Where(x => x.UserName == Filters.UserName);

            //if (!string.IsNullOrEmpty(Filters.BrandID))
            //    res = res.Where(x => x.BrandId.ToString() == Filters.BrandID);


            //if (!string.IsNullOrEmpty(Filters.Location))
            //{
            //    var temp = this.getLocationDetail(Filters.Location).description;
            //    res = res.Where(x => x.Location.ToString() == temp);
            //}

            //count = res.Count();

            //if (sort == null || sort == "")
            //{
            //    return res.OrderByDescending(x => x.CreateTime)
            //        .Skip(skippingRows).Take(pageSize);
            //}
            //else {
            //    switch (sortdir)
            //    {
            //        case "DESC":
            //            return res.OrderByDescending(sort)
            //                .Skip(skippingRows).Take(pageSize);
            //        default:
            //            return res.OrderBy(sort)
            //                .Skip(skippingRows).Take(pageSize);
            //    }
            //}

        }


        //public IQueryable<DailyUpateView> DailyUpateViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
        //{
        //    int skippingRows = (pageNumber - 1) * pageSize;

        //    switch (sort)
        //    {
        //        case "CreateTime":
        //            count = this.DailyUpateView.Count();
        //            if (sortdir == "ASC")
        //                return this.DailyUpateView.OrderBy(x => x.CreateTime)
        //                    .Skip(skippingRows).Take(pageSize);
        //            return this.DailyUpateView.OrderByDescending(x => x.CreateTime)
        //                .Skip(skippingRows).Take(pageSize);
        //        default:
        //            count = this.DailyUpateView.Count();
        //            return this.DailyUpateView.OrderBy(x => x.UserId)
        //                .Skip(skippingRows).Take(pageSize);
        //    }
        //}

        internal object GetDailyUpateViewPagingExport(ActionViewFilters Filters)
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
