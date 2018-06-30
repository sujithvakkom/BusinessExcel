
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

    }
}
