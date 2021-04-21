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
        const String _MOBILE="MOBILE";
        public List<delivery> GetDelivery(string ID, int? status = null, string driver = null)
        {

            string SELECT_USER = @"
SELECT [id] ,
       [ROWID] ,
       [TRANS_TYPE] ,
       [RECEIPTID] ,
       [TRANSACTIONID] ,
       [TRANSDATE] ,
       [LINENUM] ,
       [ITEMID] ,
       [DESCRIPTION] ,
       [QUANTITY] ,
       [SEIINGPRICE] ,
       [COMMENT] ,
       [DELIVARYTYPE] ,
       [ACCOUNTNUM] ,
       [NAME] ,
       [STREET] ,
       [ADDRESS] ,
       [DELIVERY_ADDRESS] ,
       [EMIRATE] ,
       [GPS] ,
       [REQ_NUMBER] ,
       [INT_STATUS] ,
       [DT_STATUS] ,
       [DT_SCHEDULED] ,
       [REMARKS] ,
       [DT_ESTIMATED] ,
       [LPO_NUMBER] ,
       [TIME_SLOT] ,
       [SCHEDULED_ASPER] ,
       [DRIVER_NAME] ,
       [FAILED_REASON] ,
       [CUST_NO] ,
       [SITE_NO] ,
       [EMP_ID] ,
       [CREATED_ON] ,
       [WARRANTY] ,
       [RECEIPT_CREATED] ,
       [ORA_REQ_NUMBER] ,
       [ORA_ORDER_NO] ,
       [ORA_ORDER_STATUS] ,
       [ORA_SHIPMENT_STATUS] ,
       [SHIPMENT_NUMBER] ,
       [RECEIPT_NUMBER] ,
       [REQUISITION_HEADER_ID] ,
       [REQUISITION_LINE_ID] ,
       [VEHICLE_CODE] ,
       [GPS_CORDINATES] ,
       [MAP_URL]
FROM [delivery_1]
  where " + ((string.IsNullOrEmpty(driver) == true && status == 4) ?
@"[RECEIPTID]+cast(cast(LINENUM as int)as nvarchar) = @receipt" :
(string.IsNullOrEmpty(driver) != true ?
(String.IsNullOrEmpty(ID) ? @"" : @"[RECEIPTID]+cast(cast(LINENUM as int)as nvarchar) = @receipt AND ")
: @"") +
"[INT_STATUS]=@status_code AND [VEHICLE_CODE]=@driver");

            var _receipt = !string.IsNullOrEmpty(ID) ?
                  new SqlParameter("@receipt", ID) :
                  new SqlParameter("@receipt", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var _status_code = status != null ?
                  new SqlParameter("@status_code", status) :
                  new SqlParameter("@status_code", 4);

            var _driver = !string.IsNullOrEmpty(driver) ?
                  new SqlParameter("@driver", driver) :
                  new SqlParameter("@driver", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            var delivery = this.Database.SqlQuery<delivery>(SELECT_USER, _receipt, _status_code, _driver).ToList();

            return delivery;
        }

        public string UpdateDelivery(string receipt, int status, string driver)
        {
            const string CMD_UPDATE_STATUS = @"update [delivery_1]
set [INT_STATUS] = @status_code , [DRIVER_NAME]=@driver
  where [RECEIPTID]+cast(cast(LINENUM as int)as nvarchar)= @receipt ";
            const string CMD_UPDATE_LOG = @"INSERT INTO [delivery_log_1] ( [RECEIPTID] ,
                                   [STATUS] ,
                                   [STATUS_DATE] ,
                                   [REMARKS] ,
                                   [LOG_DATE] ,
                                   [USER_NAME],
                                   [delivery_id]
                                 )
VALUES ( [GetReceiptID]( @receipt )  ,
         @status_desc ,
         GETDATE() ,
         @status_desc1 +': '+[GetReceiptVehicle] ( @receipt1 ) ,
         GETDATE() ,
         'Dispatcher',
         [GetReceiptLineID]( @receipt2 ) 
       )";

            var _receipt = !string.IsNullOrEmpty(receipt) ?
                  new SqlParameter("@receipt", receipt) :
                  new SqlParameter("@receipt", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _receipt_v = !string.IsNullOrEmpty(receipt) ?
                  new SqlParameter("@receipt_v", receipt) :
                  new SqlParameter("@receipt_v", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _driver = !string.IsNullOrEmpty(driver) ?
                  new SqlParameter("@driver", driver) :
                  new SqlParameter("@driver", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };
            var _status_code =
                  new SqlParameter("@status_code", status);
            string statusDesc = "";
            switch (status)
            {
                case 3:
                    statusDesc = "Loaded";
                    break;
                case 5:
                    statusDesc = "Delivered";
                    break;
                default:
                    statusDesc = "Missing";
                    break;
            }
            var _status =
                  new SqlParameter("@status_desc", statusDesc);
            try
            {
                this.Database.ExecuteSqlCommand(CMD_UPDATE_STATUS, _status_code, _receipt, _driver);

                _receipt = !string.IsNullOrEmpty(receipt) ?
                      new SqlParameter("@receipt", receipt) :
                      new SqlParameter("@receipt", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

                this.Database.ExecuteSqlCommand(CMD_UPDATE_LOG,
                    !string.IsNullOrEmpty(receipt) ?
                      new SqlParameter("@receipt", receipt) :
                      new SqlParameter("@receipt", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value },
                    new SqlParameter("@status_desc", statusDesc),
                    new SqlParameter("@status_desc1", statusDesc),
                    !string.IsNullOrEmpty(receipt) ?
                      new SqlParameter("@receipt1", receipt) :
                      new SqlParameter("@receipt1", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value },
                    !string.IsNullOrEmpty(receipt) ?
                      new SqlParameter("@receipt2", receipt) :
                      new SqlParameter("@receipt2", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value }
                    );
                return receipt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string AddDelivery(string itemcode, string customerName, string customerPhone, 
            string customerEmirate, string pONumber, int quantity, string userName, string address, string remarks,
            DateTime saleDate,  DateTime preferedDate
            )
        {
            const string CMD_ADD_DELIVERY = @"INSERT INTO delivery_1 ( ROWID ,
                         TRANS_TYPE ,
                         RECEIPTID ,
                         TRANSACTIONID ,
                         TRANSDATE ,
                         DT_ESTIMATED,
                         LINENUM ,
                         ITEMID ,
                         DESCRIPTION ,
                         QUANTITY ,
                         LPO_NUMBER ,
                         REQ_NUMBER ,
                         ORA_REQ_NUMBER ,
                         ORA_ORDER_NO ,
                         INT_STATUS ,
                         NAME ,
                         STREET ,
                         EMP_ID,
                         [ADDRESS] ,
                         REMARKS
                       )
VALUES ( @ROWID ,
         @TRANS_TYPE ,
         @RECEIPTID ,
         @TRANSACTIONID ,
         @TRANSDATE ,
         @DT_ESTIMATED ,
         @LINENUM ,
         @ITEMID ,
         @DESCRIPTION ,
         @QUANTITY ,
         @LPO_NUMBER ,
         @REQ_NUMBER ,
         @ORA_REQ_NUMBER ,
         @ORA_ORDER_NO ,
         @INT_STATUS ,
         @NAME ,
         @STREET ,
         @EMP_ID,
         @ADDRESS,
         @REMARKS
       )";
            const string CMD_UPDATE_LOG = @"INSERT INTO [delivery_log_1] ( [RECEIPTID] ,
                                   [STATUS] ,
                                   [STATUS_DATE] ,
                                   [LOG_DATE] ,
                                   [USER_NAME],
                                   [delivery_id]
                                 )
VALUES ( [dbo].[GetReceiptID]( @receipt )  ,
         @status_desc ,
         GETDATE() ,
         GETDATE() ,
         'Merchand',
         @delivery_id
       )";
            /*
             INT_STATUS : 20
             TRANS_TYPE : MOBILE
             */
            var _nextid = GetNextRowid();
            var _ROWID = new SqlParameter("@ROWID", _nextid);
            var _TRANS_TYPE = new SqlParameter("@TRANS_TYPE", _MOBILE);
            var _RECEIPTID = new SqlParameter("@RECEIPTID", ("ORACLE_" + _nextid));
            var _TRANSACTIONID = new SqlParameter("@TRANSACTIONID", "ORACLE");
            var _TRANSDATE = new SqlParameter("@TRANSDATE", saleDate);
            var _DT_ESTIMATED = new SqlParameter("@DT_ESTIMATED", preferedDate);
            var _LINENUM = new SqlParameter("@LINENUM", 1);
            var _ITEMID = new SqlParameter("@ITEMID", itemcode);
            var _DESCRIPTION = new SqlParameter("@DESCRIPTION", GetItemDescription(itemcode));
            var _QUANTITY = new SqlParameter("@QUANTITY", quantity);
            var _LPO_NUMBER = new SqlParameter("@LPO_NUMBER", pONumber);
            var _REQ_NUMBER = new SqlParameter("@REQ_NUMBER", "");
            var _ORA_REQ_NUMBER = new SqlParameter("@ORA_REQ_NUMBER", "");
            var _ORA_ORDER_NO = new SqlParameter("@ORA_ORDER_NO", "");
            var _INT_STATUS = new SqlParameter("@INT_STATUS", 20);
            var _CUST_NO = string.IsNullOrEmpty(customerName) ?
                new SqlParameter("@NAME", "") :
                new SqlParameter("@NAME", customerName);
            var _SITE_NO = string.IsNullOrEmpty(customerPhone) ?
                new SqlParameter("@STREET", "") :
                new SqlParameter("@STREET", customerPhone);
            var _EMP_ID = string.IsNullOrEmpty(userName) ?
                new SqlParameter("@EMP_ID", "") :
                new SqlParameter("@EMP_ID", userName);
            var _REMARKS = string.IsNullOrEmpty(remarks) ?
                new SqlParameter("@REMARKS", "") :
                new SqlParameter("@REMARKS", remarks);
            var _ADDRESS = string.IsNullOrEmpty(address) ?
                new SqlParameter("@ADDRESS", "") :
                new SqlParameter("@ADDRESS", address);

            string statusDesc = "";
            switch (2)
            {
                case 2:
                    statusDesc = "New Order";
                    break;
                case 3:
                    statusDesc = "Loaded";
                    break;
                case 5:
                    statusDesc = "Delivered";
                    break;
                default:
                    statusDesc = "Missing";
                    break;
            }
            var _status =
                  new SqlParameter("@status_desc", statusDesc);
            try
            {
                this.Database.ExecuteSqlCommand(CMD_ADD_DELIVERY,
                    _ROWID,
_TRANS_TYPE,
_RECEIPTID,
_TRANSACTIONID,
_TRANSDATE,
_DT_ESTIMATED,
_LINENUM,
_ITEMID,
_DESCRIPTION,
_QUANTITY,
_LPO_NUMBER,
_REQ_NUMBER,
_ORA_REQ_NUMBER,
_ORA_ORDER_NO,
_INT_STATUS,
_CUST_NO,
_SITE_NO,
_EMP_ID,
_ADDRESS,
_REMARKS
                    );



                this.Database.ExecuteSqlCommand(CMD_UPDATE_LOG,
                    !string.IsNullOrEmpty("ORACLE_" + _nextid) ?
                      new SqlParameter("@receipt", "ORACLE_" + _nextid+"1") :
                      new SqlParameter("@receipt", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value },

                    new SqlParameter("@status_desc", statusDesc),

                    !string.IsNullOrEmpty("ORACLE_" + _nextid) ?
                      new SqlParameter("@delivery_id", 1) :
                      new SqlParameter("@receipt2", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value }
                    );
                return "ORACLE_" + _nextid;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateLog( int status, string driver, string remark, int headerID,int? detailsId)
        {
            /*
             INSERT INTO [delivery_log_1] ( HEADER_ID ,
                               DETAILS_ID ,
                               LOG_DATE ,
                               STATUS ,
                               REMARKS ,
                               STATUS_DATE ,
                               USER_NAME
                             )
            VALUES ( @header_id ,
                     @details_id ,
                     getdate() ,
                     @status ,
                     @remark ,
                     getdate() ,
                     @user_name
                   )
             */
            string statusDesc = Util.getStatusDes(status);
            try
            {
                this.Database.ExecuteSqlCommand(
                    Properties.Resources.CMD_UPDATE_LOG,
                    new SqlParameter("@header_id", headerID),
                    new SqlParameter("@details_id", detailsId==null?DBNull.Value :(object) detailsId),
                    new SqlParameter("@status", String.IsNullOrEmpty(statusDesc) ? DBNull.Value : (object)statusDesc),
                    new SqlParameter("@remark", String.IsNullOrEmpty(remark) ? DBNull.Value : (object)remark),
                    new SqlParameter("@user_name", String.IsNullOrEmpty(driver) ? DBNull.Value : (object)driver)
                );
                return statusDesc;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string UpdateAttachment(string receiptid, string file_name)
        {
            const string CMD_UPDATE_LOG = @"insert into delivery_attachments(receiptid, file_name) values(@receiptid, @file_name)";

            try
            {

                this.Database.ExecuteSqlCommand(CMD_UPDATE_LOG,
                    !string.IsNullOrEmpty(receiptid) ?
                    new SqlParameter("@receiptid", receiptid) :
                    new SqlParameter("@receipt", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value },
                    !string.IsNullOrEmpty(receiptid) ?
                    new SqlParameter("@file_name", file_name) :
                    new SqlParameter("@file_name", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value }
                    );
                return receiptid;
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected int? GetNextRowid()
        {
            string sql = @"Select ISNULL( MAX(ROWID),0) + 1 As ROWID FROM delivery_1 WHERE TRANS_TYPE='"+ _MOBILE + "'";

            var nextID = this.Database.SqlQuery<int>(sql).ToList();
            if (nextID != null) return nextID[0];
            else return null;

        }
        protected String GetItemDescription(string itemcode)
        {
            string sql = @"select ITEMNAME from [dbo].[INVENTTABLE] where ITEMID = @itemcode";

            var itemDesc = this.Database.SqlQuery<string>(sql,
                !string.IsNullOrEmpty(itemcode) ?
                    new SqlParameter("@itemcode", itemcode) :
                    new SqlParameter("@itemcode", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value }
                ).ToList();
            if (itemDesc != null) return itemDesc[0];
            else return "";

        }
    }
}
