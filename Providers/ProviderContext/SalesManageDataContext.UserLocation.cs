
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
        private readonly string cmdPutUserLoction = @"[update_checkin] 
   @user_id
  ,@latitude
  ,@longitude
  ,@type
  ,@address";
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
                new SqlParameter("@longitude", SqlDbType.Decimal) { Value = DBNull.Value } :
                new SqlParameter("@longitude", UserLocation.Longitude);

            var type =
                UserLocation.Type == null ?
                new SqlParameter("@type", SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@type", UserLocation.Type);

            var address =
                UserLocation.Address == null ?
                new SqlParameter("@address", SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@address", UserLocation.Address);

            try
            {
                int r = this.Database.ExecuteSqlCommand(cmdPutUserLoction,
                    userID,
                    latitude,
                    longitude,
                    type,
                    address);
                if (r > 1) result = "Success";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private readonly string cmdGetUserLoction = @"SELECT top(1) user_id,
       checkin_time ,
       latitude,
       longitude,
       type,
       address
       from [merchant_checkin_m] 
       where user_id = @user_id";
        public UserLocation GetUserLoction(int? UserId, DateTime? When = null) {
            UserLocation result = null;

            var userID =
                UserId == null ?
                new SqlParameter("@user_id", SqlDbType.Int) { Value = DBNull.Value } :
                new SqlParameter("@user_id", UserId);

            try
            {
                result = this.Database.SqlQuery<UserLocation>(cmdGetUserLoction, userID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
