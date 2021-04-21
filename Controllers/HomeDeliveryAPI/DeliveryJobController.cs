using BusinessExcel.Helpers;
using DataProvider;
using DataProvider.Entities.DeliveryJob;
using NotificationProvider;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace HomeDelivery.Controllers.API
{
    public class DeliveryJobController : ApiController
    {
        //string OrderNumber, string VehicleCode, int Status = 8

        [HttpGet()]
        public List<DeliveryHeader> GetMobileOrders(string EmpID, string TransType)
        {
            List<DeliveryHeader> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getMobileOrders(EmpID, TransType);
            }
            return result;
        }
        //string OrderNumber, string VehicleCode, int Status = 8

        [HttpGet()]
        public List<DeliveryLine> GetMobileOrderDetails(string Receipt)
        {
            List<DeliveryLine> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getMobileOrderDetails(Receipt);
            }
            return result;
        }
        //string OrderNumber, string VehicleCode, int Status = 8


        //string OrderNumber, string VehicleCode, int Status = 8

        [HttpGet()]
        public DeliveryHeader GetDeliveryJob(string OrderNumber, string VehicleCode, int Status = 8)
        {
            DeliveryHeader result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getDelivery(OrderNumber, VehicleCode, Status);
            }
            return result;
        }
        //string OrderNumber, string VehicleCode, int Status = 8

        [HttpGet()]
        public List<DeliveryHeader> GetDeliveryJobs(string VehicleCode, int Status = 8)
        {
            List<DeliveryHeader> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getDeliveries(VehicleCode, Status);
            }
            return result;
        }
        //string OrderNumber, string VehicleCode, int Status = 8

        [HttpPost()]
        public DeliveryHeader SetDeliveryJobs(DeliveryHeader DeliveryJob)
        {
            #region LOG   
            try
            {
                string requestName = @"DeliveryJob/SetDeliveryJobs";
                using (SalesManageDataContext provider = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    var jsonSerial = new JavaScriptSerializer();
                    string parameter = jsonSerial.Serialize(DeliveryJob);
                    provider.Log(requestName, parameter);
                }
            }
            catch (Exception) { }
            #endregion
            List<DeliveryHeader> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                DeliveryJob = db.UpdateDelivery(DeliveryJob);
            }
            try
            {
                Task.Run(() => sendNitification(DeliveryJob));
            }
            catch (Exception) { }
            return DeliveryJob;
        }

        private void sendNitification(DeliveryHeader DeliveryJob)
        {
            #region LOG   
            try
            {
                string requestName = @"DeliveryJob/sendNitification";
                using (SalesManageDataContext provider = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    var jsonSerial = new JavaScriptSerializer();
                    string parameter = jsonSerial.Serialize(DeliveryJob);
                    provider.Log(requestName, parameter);
                }
            }
            catch (Exception) { }
            #endregion
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
                                string FormatedPhone = delivery.Phone.Replace(" ", "");
                                if (FormatedPhone.Length == 10)
                                    FormatedPhone = delivery.Phone.TrimStart(new char[] { '0' });
                                if (FormatedPhone.Length == 9)
                                    FormatedPhone = "+971" + FormatedPhone;
                                var smsResopnce = provider.SMSSend(FormatedPhone, message);
                                db.AddDeliveryNotifications(delivery.HeaderId, FormatedPhone, message, smsResopnce, delivery.Status, delivery.DriverName);
                            }
                            else
                                db.ErrorLog(
                                    string.Format("Delivery {0}, Status {1} InvalidMesssage", delivery.HeaderId, delivery.Status),
                                    "Send SMS");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    db.ErrorLog(ex.Message, "Send SMS");
                }
            }
        }

        [HttpPost()]
        public String AddDeliveryJob(DeliveryHeader DeliveryJob)
        {
            string Receipt = "";
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                Receipt = db.AddDelivery(DeliveryJob);
            }

            return Receipt;
        }
        
        [HttpPost()]
        public HttpResponseMessage AddDeliveryJobWithAttachment(DeliveryHeader DeliveryJob)
        {
            string Receipt = "";
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/delivery_attachments/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                    DeliveryJob.attachmentName = filePath;
                    using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                    {
                        Receipt = db.AddDelivery(DeliveryJob);
                    }
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                result.Content.Headers.Add("Receipt", Receipt);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        [HttpGet]
        public List<DeliveryHeader> GetDeliveryJobsNotifications(string VehicleCode, int Status = 8)
        {
            List<DeliveryHeader> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getDeliveries(VehicleCode, Status, 0);
                foreach (var x in result)
                {
                    db.UpdateNotfied(x, 1);
                }
            }
            return result;
        }
    }
}
