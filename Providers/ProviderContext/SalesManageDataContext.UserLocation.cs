
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using System.Data.SqlClient;
using System.Data;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {
        private readonly string cmdUserLoction = @"[sc_salesmanage_merchant].[update_checkin] 
   @user_id
  ,@latitude
  ,@longitude
  ,@type";
        public string PutUserLoction(UserLocation UserLocation)
        {
            string result = "Failed";

            var userID =
                UserLocation.UserID == null ?
                new SqlParameter("@user_id", SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@user_id", UserLocation.UserID);

            var latitude =
                UserLocation.Latitude == null ?
                new SqlParameter("@latitude", SqlDbType.Decimal) { Value = DBNull.Value } :
                new SqlParameter("@latitude", UserLocation.Latitude);

            var longitude =
                UserLocation.Longitude == null ?
                new SqlParameter("@latitude", SqlDbType.Decimal) { Value = DBNull.Value } :
                new SqlParameter("@latitude", UserLocation.Longitude);

            var type =
                UserLocation.Type == null ?
                new SqlParameter("@user_id", SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@user_id", UserLocation.Type);

            try
            {
                int r = this.Database.ExecuteSqlCommand(cmd,
                    userID,
                    latitude,
                    longitude,
                    type);
                if (r > 1) result = "Success";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
