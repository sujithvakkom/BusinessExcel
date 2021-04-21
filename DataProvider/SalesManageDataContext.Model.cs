using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{

    public partial class SalesManageDataContext : DbContext
    {
        #region QUERY
        string cmdNormal = "[getModelDetailsNormal]  @filter, @user_name ,@page_number ,@page_size ,@row_count OUTPUT";

        string cmdExtended = "[getModelDetails]  @filter, @user_name ,@page_number ,@page_size ,@row_count OUTPUT";
        string cmdBrands = @"SELECT GROUPID AS BrandID ,
       NAME AS    BrandCode
FROM RETAILGROUP";
        #endregion
        public List<ItemModel> getModelDetailsExtended(string search, int Page, out int RowCount, string UserName)
        {

            List<ItemModel> items = new List<ItemModel>();

            var item_codePar = search != null ?
                new SqlParameter("@filter", search) :
                new SqlParameter("@filter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var userNamePar = UserName != null ?
                new SqlParameter("@user_name", UserName) :
                new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;
            //db.getItemDetailsImport(null, null, null);
            try
            {
                items = this.Database.SqlQuery<ItemModel>(
                                                cmdExtended,
                                                item_codePar,
                                                userNamePar,
                                                page_number,
                                                page_size,
                                                row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return items;
        }

        public List<ItemModel> getModelDetails(string search, int Page, out int RowCount, string UserName)
        {

            List<ItemModel> items = new List<ItemModel>();

            var item_codePar = search != null ?
                new SqlParameter("@filter", search) :
                new SqlParameter("@filter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var userNamePar = UserName != null ?
                new SqlParameter("@user_name", UserName) :
                new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;
            //db.getItemDetailsImport(null, null, null);
            try
            {
                items = this.Database.SqlQuery<ItemModel>(
                                                cmdNormal,
                                                item_codePar,
                                                userNamePar,
                                                page_number,
                                                page_size,
                                                row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return items;
        }

        public List<Brand> getBrands() {
            return this.Database.SqlQuery<Brand>(cmdBrands).ToList();
        }
    }
}