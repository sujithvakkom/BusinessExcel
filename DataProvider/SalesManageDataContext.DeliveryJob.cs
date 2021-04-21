using DataProvider.Entities;
using DataProvider.Entities.DeliveryJob;
using DataProvider.Properties;
using System;
using System.Collections.Generic;
using System.Data;
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
        public virtual List<DeliveryHeader> getMobileOrders(string EmpID, string TransType)
        {
            try
            {
                var result =
                            this.Database.SqlQuery<DeliveryHeader>(Resources.MOBILE_ORDER,
                            new SqlParameter("@USER_NAME", EmpID) { SqlDbType = SqlDbType.NVarChar },
                            new SqlParameter("@TRANSTYPE", TransType)).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public virtual List<DeliveryLine> getMobileOrderDetails(string Receipt)
        {
            var result =
                        this.Database.SqlQuery<DeliveryLine>(Resources.MOBILE_ORDER_DETAIL,
                        new SqlParameter("@RECEIPTID", Receipt)).ToList();

            return result;
        }


        public virtual DeliveryHeader getDelivery(string OrderNumber, string VehicleCode, int Status = 8)
        {
            try
            {
                var result =
                            this.Database.SqlQuery<DeliveryHeader>(Resources.DELIVERY_HEADER,
                            new SqlParameter("@ORDER_NUMBER", OrderNumber),
                            new SqlParameter("@VEHICLE_CODE", VehicleCode),
                            new SqlParameter("@INT_STATUS", Status),
                            new SqlParameter("@NOTIFIED_DRIVER", DBNull.Value)
                            ).First();

                if (result != null)
                {
                    result.DeliveryLines = this.Database.SqlQuery<DeliveryLine>(Resources.DELIVERY_LINES,
                            new SqlParameter("@HEADER_ID", result.HeaderId),
                            new SqlParameter("@VEHICLE_CODE", VehicleCode),
                            new SqlParameter("@INT_STATUS", Status)).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<DeliveryHeader> getDeliveries(string VehicleCode, int Status = 8, int isNotified = -1)
        {
            var result =
                        this.Database.SqlQuery<DeliveryHeader>(Resources.DELIVERY_HEADER,
                        new SqlParameter("@ORDER_NUMBER", DBNull.Value),
                        new SqlParameter("@VEHICLE_CODE", VehicleCode),
                        new SqlParameter("@INT_STATUS", Status),
                        isNotified == -1 ? new SqlParameter("@NOTIFIED_DRIVER", DBNull.Value) : new SqlParameter("@NOTIFIED_DRIVER", isNotified)
                        ).ToList();

            if (result != null)
            {
                foreach (var x in result)
                    x.DeliveryLines = this.Database.SqlQuery<DeliveryLine>(Resources.DELIVERY_LINES,
                            new SqlParameter("@HEADER_ID", x.HeaderId),
                            new SqlParameter("@VEHICLE_CODE", VehicleCode),
                            new SqlParameter("@INT_STATUS", Status)).ToList();
            }
            return result;
        }

        public DeliveryHeader UpdateDelivery(DeliveryHeader DeliveryJob)
        {
            var tran = this.Database.BeginTransaction();
            try
            {
                string statusDesc = "";
                foreach (var line in DeliveryJob.DeliveryLines)
                {
                    this.Database.ExecuteSqlCommand(
                        Resources.UPDATE_DELIVER_LINE,
                        new SqlParameter("@id", line.LineId),
                        new SqlParameter("@INT_STATUS", line.Status),
                        new SqlParameter("@REMARKS", line.Remarks == null ? "" : line.Remarks),
                        new SqlParameter("@DT_ESTIMATED", line.ScheduledDate)
                        );

                    statusDesc = Util.getStatusDes(line.Status);
                    this.Database.ExecuteSqlCommand(
                        Resources.CMD_UPDATE_LOG,
                        new SqlParameter("@header_id", DeliveryJob.HeaderId),
                        new SqlParameter("@details_id", line.LineId),
                        new SqlParameter("@status", statusDesc),
                        new SqlParameter("@remark", line.Remarks == null ? statusDesc : line.Remarks),
                        new SqlParameter("@user_name", string.IsNullOrEmpty(line.VehicleCode) ? DeliveryJob.DriverName : line.VehicleCode)
                    );
                }
                tran.Commit();

                return DeliveryJob;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                tran.Rollback();
                return null;
            }
        }

        public string AddDelivery(DeliveryHeader DeliveryJob)
        {
            var tran = this.Database.BeginTransaction();
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("LineId", typeof(Int32));
                table.Columns.Add("SaleDate", typeof(DateTime));
                table.Columns.Add("LineNumber", typeof(Decimal));
                table.Columns.Add("ItemCode", typeof(String));
                table.Columns.Add("Description", typeof(String));
                table.Columns.Add("OrderQuantity", typeof(Decimal));
                table.Columns.Add("SelleingPrice", typeof(Decimal));
                table.Columns.Add("Status", typeof(Int32));
                table.Columns.Add("ScheduledDate", typeof(DateTime));
                table.Columns.Add("TimeSlot", typeof(String));
                table.Columns.Add("DriverName", typeof(String));
                table.Columns.Add("VehicleCode", typeof(String));
                table.Columns.Add("GPS", typeof(String));
                table.Columns.Add("MapURL", typeof(String));
                table.Columns.Add("Remarks", typeof(String));
                table.Columns.Add("Emirate", typeof(String));

                foreach (var lin in DeliveryJob.DeliveryLines)
                {
                    table.Rows.Add(
                        lin.LineId, lin.SaleDate, lin.LineNumber, lin.ItemCode, lin.Description, 
                        lin.OrderQuantity * (DeliveryJob.saleType == "Return" ? -1 : 1), 
                        lin.SelleingPrice * (DeliveryJob.saleType == "Return" ? -1 : 1), 
                        lin.Status, lin.ScheduledDate, lin.TimeSlot, lin.DriverName, lin.VehicleCode, lin.GPS,
                        lin.MapURL, lin.Remarks, lin.Emirate
                        );
                }
                /*
                [dbo].[AddDeliveryJob] 
   @CustomerName
  ,@SaleDate
  ,@PreferedDate
  ,@Phone
  ,@DeliveryAddress
  ,@attachmentName
  ,@input_type
  ,@Remarks
  ,@username
  ,@lines
  ,@RECEIPTID_OUT OUTPUT
  ,@HEADER_ID OUTPUT
                 */

                var lines =
                        new SqlParameter("@lines", table);
                lines.SqlDbType = SqlDbType.Structured;
                lines.TypeName = "DELIVERYJOBLINE";

                var ReceiptId =
                        new SqlParameter("@RECEIPTID_OUT", SqlDbType.NVarChar, 20);
                ReceiptId.Direction = ParameterDirection.Output;

                var HeaderId =
                        new SqlParameter("@HEADER_ID", SqlDbType.Int);
                HeaderId.Direction = ParameterDirection.Output;

                this.Database.ExecuteSqlCommand(
                        Resources.ADD_DELIVERY,
                        ReceiptId,
                        HeaderId,
                        new SqlParameter("@CustomerName", String.IsNullOrEmpty(DeliveryJob.CustomerName) ? "" : DeliveryJob.CustomerName),
                        new SqlParameter("@SaleDate", DeliveryJob.SaleDate),
                        new SqlParameter("@PreferedDate", DeliveryJob.ScheduledDate == null ? new DateTime(1999, 1, 1) : DeliveryJob.ScheduledDate),
                        new SqlParameter("@Phone", String.IsNullOrEmpty(DeliveryJob.Phone) ? "" : DeliveryJob.Phone),
                        new SqlParameter("@DeliveryAddress", String.IsNullOrEmpty(DeliveryJob.DeliveryAddress) ? "" : DeliveryJob.DeliveryAddress),
                        new SqlParameter("@attachmentName", String.IsNullOrEmpty(DeliveryJob.attachmentName) ? "" : DeliveryJob.attachmentName),
                        new SqlParameter("@input_type", "MOB"),
                        new SqlParameter("@Remarks", String.IsNullOrEmpty(DeliveryJob.Remarks) ? "" : DeliveryJob.Remarks),
                        new SqlParameter("@username", DeliveryJob.UserName),
                        new SqlParameter("@retailer", String.IsNullOrEmpty(DeliveryJob.Retailer) ? "" : DeliveryJob.Retailer),
                        lines
                        );
                tran.Commit();
                try
                {
                    this.UpdateLog(
                        DeliveryJob.Status,
                        DeliveryJob.UserName,
                        "New mobile order.",
                        int.Parse(HeaderId.Value.ToString()),
                        null);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                return ReceiptId.Value.ToString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                tran.Rollback();
                return null;
            }
        }

        public List<DeliveryStatusView> GetDeliveryStatusView(string VehicleCode, int Status = 8)
        {
            var result =
                        this.Database.SqlQuery<DeliveryStatusView>(Resources.GET_DELIVERY_STATUS_VIEW,
                        new SqlParameter("@VEHICLE_CODE", VehicleCode),
                        new SqlParameter("@INT_STATUS", Status)
                        ).ToList();
            return result;
        }

        public void UpdateNotfied(DeliveryHeader Delivery, int Notified = 0)
        {
            foreach (var line in Delivery.DeliveryLines)
            {
                this.Database.ExecuteSqlCommand(
                    Resources.UPDATE_NOTIFIED_DRIVER,
                    new SqlParameter("@NOTIFIED_DRIVER", Notified),
                    new SqlParameter("@id", line.LineId));
            }
        }

        public void ErrorLog(string ErrorMessage, string Source)
        {

            var tran = this.Database.BeginTransaction();
            try
            {
                string statusDesc = "";
                this.Database.ExecuteSqlCommand(
                    Resources.INSERT_ERROR,
                    new SqlParameter("@Source", Source),
                    new SqlParameter("@Message", ErrorMessage)
                    );

                tran.Commit();

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                tran.Rollback();
            }
        }

        public List<DeliveryNotification> GetDeliveryNotifications(string OrderNumber)
        {
            var result = this.Database.SqlQuery<DeliveryNotification>(Resources.SELECT_MESSAGE_LOG,
                new SqlParameter("@OrderNumber", string.IsNullOrEmpty(OrderNumber) ? (object)DBNull.Value : (object)OrderNumber)).ToList();
            return result;
        }
        public void AddDeliveryNotifications(int HeaderId, string PhoneNumber, string Message, string ErrorMessage, int Status, string UserName)
        {
            this.Database.ExecuteSqlCommand(Resources.INSERT_MESSAGE_LOG,
                    new SqlParameter("@HEADER_ID", HeaderId),
                    new SqlParameter("@PHONE_NUMBER", PhoneNumber),
                    new SqlParameter("@MESSAGE", Message),
                    new SqlParameter("@ERROR_MESSAGE", ErrorMessage),
                    new SqlParameter("@DELIVERY_STATUS", Status),
                    new SqlParameter("@USER_NAME", UserName)
                );
        }
    }
}