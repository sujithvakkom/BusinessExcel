
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

using BusinessExcel.Extentions;
using BusinessExcel.Models;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        
        public virtual ItemDetails getItemDetails(string serarch)
        {
            if (!String.IsNullOrEmpty(serarch))
            {
                var item_code = serarch != null ?
                    new SqlParameter("@item_code", serarch) :
                    new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

                int? page = null;
                var page_size = page != null ?
                    new SqlParameter("@page_size", page) :
                    new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

                int? page_num = null;
                var page_number = page_num != null ?
                    new SqlParameter("@page_number", page_num) :
                    new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

                int? row = null;
                var row_count = row != null ?
                    new SqlParameter("@row_count", row) :
                    new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
                row_count.Direction = System.Data.ParameterDirection.Output;

                var items = this.Database.SqlQuery<ItemDetails>(
                                                "[sc_salesmanage_merchant].[getItemDetails]  @item_code ,@page_number ,@page_size ,@row_count OUTPUT", item_code, page_number, page_size, row_count)
                                                .ToList();

                return items[0];
            }
            return null;
        }


        public virtual List<ItemDetails> getItemDetails(string search, int Page, out int RowCount)
        {
            List<ItemDetails> items = new List<ItemDetails>();

            var item_codePar = search != null ?
                new SqlParameter("@item_code", search) :
                new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar);
            item_codePar.Value = DBNull.Value;

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt);
            page_size.Value = DBNull.Value;

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt);
            page_size.Value = DBNull.Value;

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt);
            row_count.Value = DBNull.Value;
            row_count.Direction = System.Data.ParameterDirection.Output;
            //db.getItemDetailsImport(null, null, null);
            try
            {
                items = this.Database.SqlQuery<ItemDetails>(
                                                "[sc_salesmanage_merchant].[getItemDetails]  @item_code ,@page_number ,@page_size ,@row_count OUTPUT", item_codePar, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return items;
        }

        public virtual List< ItemDetails> getItemModelCtgDetails(string search, int Page_num,  int page_sz)
        {

            var item_code = search !=null ?
                new SqlParameter("@item_code", search) :
                new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var page_number = Page_num > 0 ?
                new SqlParameter("@page_number", Page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            var page_size = page_sz > 0 ?
                new SqlParameter("@page_size", page_sz) :
                new SqlParameter("@page_size", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(item_code);
            parameterList.Add(page_number);
            parameterList.Add(page_size);


            try
            {
                var res = this.Database.SqlQuery<ItemDetails>("[db_salesmanage_user].[getAllItemDetails]  @item_code ,@page_number ,@page_size", parameterList.ToArray()).ToList();

                return res;
            }
            catch
            {

            }
            
            
            return null;
        }

        public IQueryable<ItemDetailsView> ItemDetailsPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count,
          ItemDetailsView Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;

            
            var item_code =   Filters.item_code  !=null ?
           new SqlParameter("@item_code", Filters.item_code) :
           new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

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
            parameterList.Add(item_code);
            parameterList.Add(page_number);
            parameterList.Add(page_size);
            parameterList.Add(row_count);

            var res = this.Database.SqlQuery<ItemDetailsView>("[db_salesmanage_user].[getAllItemDetails] @item_code,@page_number,@page_size,@row_count OUTPUT", parameterList.ToArray()).ToList().AsQueryable();
            int.TryParse(row_count.Value.ToString(), out count);

           // count = res.Count();
            // return res.Skip(skippingRows).Take(pageSize);
            if (sort == null || sort == "")
            {
                return res.OrderByDescending(x => x.item_code);
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

        }

        //public IQueryable<ItemDetailsView> ItemDetailsPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
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

    }
}
