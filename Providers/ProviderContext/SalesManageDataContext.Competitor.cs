
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
        
        public IQueryable<CompetitorViewModel> CompetitorDetailsPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count,
          CompetitorViewModel Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;

            int VId = getViewer_Id();

            var viewer_id = VId > 0 ?
              new SqlParameter("@viewer_id", VId) :
              new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };

            var user_name =   Filters.user_name  !=null ?
           new SqlParameter("@user_name", Filters.user_name) :
           new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

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
            parameterList.Add(user_name);
            parameterList.Add(page_number);
            parameterList.Add(page_size);
            parameterList.Add(row_count);

            parameterList.Add(viewer_id);

            var res = this.Database.SqlQuery<CompetitorViewModel>("[get_competotr_details] @user_name,@page_number,@page_size,@row_count OUTPUT,@viewer_id", parameterList.ToArray()).ToList().AsQueryable();
            int.TryParse(row_count.Value.ToString(), out count);

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
