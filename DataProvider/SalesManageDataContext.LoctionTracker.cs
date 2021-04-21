using DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{

    public partial class SalesManageDataContext : DbContext
    {

        public string PutUserLoction(LocationTracker userLocation)
        {
            const string CMD_ADD_LOCATION_TRACKER = @"EXECUTE [dbo].[ADD_LOCATION_TRACKER] 
   @VEHICLE_CODE
  ,@CREATED_TIME
  ,@LONGITUDE
  ,@LATITUDE
  ,@ACURACY";

            var _VEHICLE_CODE = !string.IsNullOrEmpty(userLocation.VehicleCode) ?
                  new SqlParameter("@VEHICLE_CODE", userLocation.VehicleCode) :
                  new SqlParameter("@VEHICLE_CODE", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var _CREATED_TIME =
                  new SqlParameter("@CREATED_TIME", userLocation.CreatedTime);

            var _LONGITUDE =
                  new SqlParameter("@LONGITUDE", userLocation.Longitude);

            var _LATITUDE = 
                  new SqlParameter("@LATITUDE", userLocation.Latitude) ;

            var _ACURACY = 
                  new SqlParameter("@ACURACY", userLocation.accuracy) ;
            try
            {

                this.Database.ExecuteSqlCommand(
                    CMD_ADD_LOCATION_TRACKER,
                    _VEHICLE_CODE,
                    _CREATED_TIME,
                    _LONGITUDE,
                    _LATITUDE,
                    _ACURACY);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Success";

        }
    }
}
