
using System;
using System.Data.Entity;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        public virtual LocationDetail getLocationDetail(string locationId)
        {
            const string SELECT_LOCATION = @"select location_id, 
                                                description
                                                from [location_m] 
                                            where 
                                                location_id = @location_id";

            var user_name = locationId != null ?    
                  new SqlParameter("@location_id", locationId) :
                  new SqlParameter("@location_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var locations = this.Database.SqlQuery<LocationDetail>(SELECT_LOCATION, user_name).ToList();
            LocationDetail detail = locations.Count > 0 ? locations[0] : null;
            return detail;
        }

        public virtual List<LocationDetail> getLocationDetails(string search, int Page, out int RowCount)
        {
            List<LocationDetail> items = new List<LocationDetail>();
              var user_name = search != null ?
                    new SqlParameter("@filter", search) :
                    new SqlParameter("@filter", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            int? page = null;
            var page_size = page != null ?
                new SqlParameter("@page_size", page) :
                new SqlParameter("@page_size", System.Data.SqlDbType.BigInt) { Value = 20 };

            int? page_num = Page;
            var page_number = page_num != null ?
                new SqlParameter("@page_number", page_num) :
                new SqlParameter("@page_number", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };

            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
            row_count.Direction = System.Data.ParameterDirection.Output;
            try
            {
                items = this.Database.SqlQuery<LocationDetail>(
                                                "[getLocationDetails] @filter ,@page_number ,@page_size ,@row_count OUTPUT", user_name, page_number, page_size, row_count)
                                                .ToList();
                int.TryParse(row_count.Value.ToString(), out RowCount);
            }
            catch (Exception ex) {
                RowCount = 0;
            }
            return items;
        }
    }
}
