
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using BusinessExcel.Models;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public IQueryable<UserM> UsersListViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count,
             UserViewFilters Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;


            var res = this.User.Select(x => x);

            //var res   = from x in this.User
            // select new { FirstName = x.FirstName, SecondName = x.SecondName, UserName = x.UserName, Email = x.Email, ContactNumber = x.ContactNumber, EmpCodeLookup = x.EmpCodeLookup != "" ? "GS" : "NON GS" };


            if (!string.IsNullOrEmpty(Filters.first_name))
                res = res.Where(x => x.FirstName+" "+x.SecondName == Filters.first_name);


          
         
           // res = res.Where(x => x. == null :"GS" :"NON GS");

            if (!string.IsNullOrEmpty(Filters.user_name))
            {
                //var temp = this.getLocationDetail(Filters.user_name).description;
                res = res.Where(x => x.UserName == Filters.user_name);
            }

            //if (Filters.create_date != default(DateTime))
            //{
            //    res = res.Where(x => x.CreateDate == Filters.create_date);
            //}
            //if (Filters.end_date != default(DateTime))
            //{
            //    res = res.Where(x => x.EndDate == Filters.end_date);

            //}

            count = res.Count();
            // return res.Skip(skippingRows).Take(pageSize);
            if (sort == null || sort == "")
            {
                return res.OrderByDescending(x => x.FirstName).Skip(skippingRows).Take(pageSize);
            }
            else
            {
                switch (sortdir)
                {
                    case "DESC":
                        return res
                            .Skip(skippingRows).Take(pageSize);
                    default:
                        return res
                            .Skip(skippingRows).Take(pageSize);
                }
            }

        }



        public IQueryable<UserM> UsersListViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
        {
            int skippingRows = (pageNumber - 1) * pageSize;

            switch (sort)
            {
                case "CreateTime":
                    count = this.DailyUpateView.Count();
                    if (sortdir == "ASC")
                        return this.User.Skip(skippingRows).Take(pageSize);
                    return this.User.Skip(skippingRows).Take(pageSize);
                default:
                    count = this.User.Count();
                    return this.User.Skip(skippingRows).Take(pageSize);
            }
        }

        //UserListView

        public virtual List<UserListView> getUserListView(string search, int Page, out int RowCount, string UserType=null)
        {
            List<UserListView> category = new List<UserListView>();

            var first_name = string.IsNullOrEmpty(search) ?
                new SqlParameter("@first_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@first_name", search);

            var user_type = string.IsNullOrEmpty(UserType) ?
                new SqlParameter("@user_type", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                new SqlParameter("@user_type", UserType);
            //filter.Value = DBNull.Value;

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = 10 };
            //page_size.Value = DBNull.Value;

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            //page_size.Value = DBNull.Value;

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) { Direction = System.Data.ParameterDirection.Output } :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value, Direction = System.Data.ParameterDirection.Output };
            //db.getItemDetailsImport(null, null, null);
            try
            {
                category = this.Database.SqlQuery<UserListView>(
                                                "[sc_salesmanage_user].[getUsersListView]  @first_name ,@user_type ,@page_number ,@page_size ,@row_count OUTPUT", first_name,user_type, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex)
            {
                RowCount = 0;
            }
            return category;
        }
    }
}
