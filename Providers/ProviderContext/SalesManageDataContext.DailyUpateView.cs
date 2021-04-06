
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
        /// <summary>
        /// Retrive the paged Merchad action
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sort"></param>
        /// <param name="sortdir"></param>
        /// <param name="count"></param>
        /// <param name="Filters">Filter</param>
        /// <returns></returns>
        public IQueryable<DailyUpateView> DailyUpateViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count,ActionViewFilters Filters)
        {
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
            if (!string.IsNullOrEmpty(Filters.UserName))
                Filters.UserID = this.getUserID(Filters.UserName).ToString();
            var user_id = !string.IsNullOrEmpty(Filters.UserID) ?
                new SqlParameter("@user_id", Filters.UserID) :
                new SqlParameter("@user_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };

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

            List<SqlParameter> parameterList = new List<SqlParameter>();
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

            var res = this.Database.SqlQuery<DailyUpateView>(
                @"
[get_actions]
 @item_code ,
 @start_date ,
 @end_date ,
 @user_id ,
 @brand_id ,
 @location_id ,
 @page_number ,
 @page_size ,
 @row_count OUTPUT ,
 @viewer_id",
                parameterList.ToArray()).ToList().AsQueryable();

            int.TryParse(row_count.Value.ToString(), out count);
            return res;
        }
    }
}