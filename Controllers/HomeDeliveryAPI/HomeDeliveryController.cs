using BusinessExcel.Helpers;
using DataProvider;
using DataProvider.Entities;
using DataProvider.Entities.DeliveryJob;
using NotificationProvider;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeDelivery.Controllers.API
{
    public class HomeDeliveryController : ApiController
    {
        [HttpGet()]
        public List<delivery> GetDelivery(string receipt, int status)
        {
            List<delivery> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.GetDelivery(receipt, status, null);
            }
            return result;
        }
        [HttpGet]
        public List<delivery> GetDelivery(string receipt, int status, string driver)
        {
            List<delivery> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.GetDelivery(receipt, status, driver);
            }
            return result;
        }

        [HttpPost]
        public string UpdateDelivery(string receipt, int status, string driver)
        {
            string result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.UpdateDelivery(receipt, status, driver);
            }
            return result;
        }

        [HttpPost]
        public string UpdateLog(int status, string driver, string remark, int headerId,int? detailsId)
        {
            string result = null;
            if (detailsId == -1) detailsId = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.UpdateLog( status, driver, remark, headerId,detailsId);
            }
            return result;
        }

        [HttpGet]
        public UserDetail GetAuthFor(string UserName, string Password)
        {
            UserDetail result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getAuthUserDetail(UserName, Password);
                if (result != null)
                    result.session = db.UpdateUserLogin(result, AppConfigSettings.WebConfigConnectionSting("GSLSR"));
            }
            return result;
        }

        [HttpGet]
        public UserDetail GetUserProfileFor(string UserName, string Session)
        {
            UserDetail result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getUserDetail(UserName,Session);
            }
            return result;
        }

        private void sendNitification(DeliveryHeader DeliveryJob)
        {
            try
            {
                using (SMSProvider provider = new SMSProvider("20076790", "4xizrx", "Grand Stores"))
                {
                    using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                    {
                        var delivery = db.getDelivery(DeliveryJob.OrderNumber, DeliveryJob.VehicleCode, DeliveryJob.Status);
                        var messageLog = db.GetDeliveryNotifications(DeliveryJob.OrderNumber);
                        if (messageLog == null || messageLog.Count == 0 || !messageLog.Exists(m => m.CratedOn.Date == DateTime.Now.Date && m.Status == delivery.Status))
                        {
                            string message = "";
                            #region MessageCreate
                            switch (delivery.Status)
                            {
                                case 9://   Loaded
                                    message = @"Dear valued customer, Thank you for choosing Grand Stores.

Your order will be delivered today.

For more info, Kindly contact 04 - 8800999 / 0547939909 / 0547939900.";
                                    break;

                                case 10://  On Hold
                                    message = "";
                                    break;

                                case 11://  Delivered
                                    message = @"Dear valued customer, Thank you for choosing Grand Stores.

Your order  is delivered, for any queries kindly contact 04 - 8800999 / 0547939909 / 0547939900.";
                                    break;

                                case 12://  Delivery Cancelled
                                    message = "";
                                    break;

                                case 13://  Closed
                                    message = "";
                                    break;

                                case 20://  Pending
                                    message = "";
                                    break;

                                case 14://  Rescheduled
                                    message = @"Dear valued customer, Thank you for choosing Grand Stores.

We tried to contact you for your scheduled delivery today but unfortunately there was no response from your side.

For  rescheduling kindly contact 04 - 8800999 / 0547939909 / 0547939900.";
                                    break;

                                case 15://  Processing
                                    message = "";
                                    break;

                                case 99://  Order Cancelled
                                    message = @"Dear valued customer, Thank you for choosing Grand Stores.

Your order was scheduled for delivery on 00 / 00 / 2000, which has been cancelled as per your request.

For any queries, kindly contact 04 - 8800999 / 0547939909 / 0547939900.";
                                    break;

                                case 16://  Rejected
                                    message = "";
                                    break;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(message))
                            {
                                var smsResopnce = provider.SMSSend(delivery.Phone, message);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
