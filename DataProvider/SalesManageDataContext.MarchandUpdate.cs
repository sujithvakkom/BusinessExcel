using DataProvider.Entities;
using DataProvider.Entities.DeliveryJob;
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
        string cmdSaveSalesNew = @"[sc_salesmanage_merchant].[udpate_merchant_daily_update_new]  @user_name, @item_code, @quantity, @value, @create_time, @client_merchant_daily_update_id, @merchant_daily_update_id OUTPUT";
        /*
         
    <string-array name="sale_types">
        <item>Sales</item>
        <item>Return</item>
    </string-array>
             */

        #endregion
        public void AddSales(DeliveryHeader deliveryHeader)
        {
            try
            {
                foreach (var x in deliveryHeader.DeliveryLines)
                {
                    var user_name = string.IsNullOrEmpty(deliveryHeader.UserName) ?
                        new SqlParameter("@user_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                        new SqlParameter("@user_name", deliveryHeader.UserName);
                    var item_code = string.IsNullOrEmpty(x.ItemCode) ?
                        new SqlParameter("@item_code", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value } :
                        new SqlParameter("@item_code", x.ItemCode);
                    var quantity = new SqlParameter("@quantity", (int)x.OrderQuantity * (deliveryHeader.saleType == "Return" ? -1 : 1));
                    var value = new SqlParameter("@value", x.SelleingPrice * (deliveryHeader.saleType== "Return" ? -1:1));
                    var create_time = new SqlParameter("@create_time", deliveryHeader.SaleDate);
                    var client_merchant_daily_update_id = 
                        new SqlParameter("@client_merchant_daily_update_id", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };
                    var merchant_daily_update_id = 
                        new SqlParameter("@merchant_daily_update_id", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output };
                    Database.ExecuteSqlCommand(cmdSaveSalesNew,
                        user_name,
                        item_code,
                        quantity,
                        value,
                        create_time,
                        client_merchant_daily_update_id,
                        merchant_daily_update_id);
                }
            } catch (Exception ex) {
                Debug.Assert(true, ex.Message);
            }
        }
    }
}