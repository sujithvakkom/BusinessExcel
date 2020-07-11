
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using BusinessExcel.Extentions;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public IQueryable<CheckinViewModel> CheckinDetailsPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count,
           CheckinViewModel Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;


            var user_name = Filters.user_name != null ?
           new SqlParameter("@user_name", Filters.user_name) :
           new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var page_number = pageNumber > 0 ?
               new SqlParameter("@page_number", pageNumber) :
               new SqlParameter("@page_number", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            var page_size = pageSize > 0 ?
                new SqlParameter("@page_size", pageSize) :
                new SqlParameter("@page_size", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            int VId = getViewer_Id();
            var viewer_id =
                new SqlParameter("@viewer_id", VId);

            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(user_name);
            parameterList.Add(page_number);
            parameterList.Add(page_size);
            parameterList.Add(viewer_id);

            var res = this.Database.SqlQuery<CheckinViewModel>(@"EXECUTE [sc_salesmanage_merchant].[get_user_checkin_details] 
   @user_name
  , @page_number
  , @page_size
  , @viewer_id", parameterList.ToArray()).ToList().AsQueryable();

            count = (res.FirstOrDefault().row_count)??0;

            // count = res.Count();
            // return res.Skip(skippingRows).Take(pageSize);
            if (sort == null || sort == "")
            {
                return res.OrderByDescending(x => x.user_name);
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
    }
}
