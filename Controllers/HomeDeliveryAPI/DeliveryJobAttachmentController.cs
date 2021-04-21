using BusinessExcel.Helpers;
using DataProvider;
using DataProvider.Entities.DeliveryJob;
using Newtonsoft.Json;
using NotificationProvider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace HomeDelivery.Controllers.API
{
    public class DeliveryJobAttachmentController : ApiController
    {
        [HttpPost]
        public async Task<String> AddDeliveryJobWithAttachment()
        {
            //[FromBody]
            DeliveryHeader DeliveryHeader = new DeliveryHeader();
            string Receipt = "";

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    db.ErrorLog("Unsupported Media Type", "AddDeliveryJobWithAttachment");
                }
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            else
            {
                try
                {
                    string root = HttpContext.Current.Server.MapPath("~/App_Data");
                    var provider = new MultipartFormDataStreamProvider(root);
                    await Request.Content.ReadAsMultipartAsync(provider);
                    foreach (var key in provider.FormData.AllKeys)
                    {
                        foreach (var val in provider.FormData.GetValues(key))
                        {
                            DeliveryHeader = JsonConvert.DeserializeAnonymousType<DeliveryHeader>(val, new DeliveryHeader());
                            Trace.WriteLine(string.Format("{0}: {1}", key, val));
                        }
                    }
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
                            DeliveryHeader.attachmentName = filePath;
                        }
                    }
                    using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                    {
                        Receipt = db.AddDelivery(DeliveryHeader);
                    }
                    string UserFullName = string.Empty;
                    using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                    {
                        var user = db.getUserDetail(DeliveryHeader.UserName);
                        if (user != null) { UserFullName = user.full_name; }
                    }
                    if (Receipt != null && DeliveryHeader.Status == 1)
                    {
                        try
                        {
                            using (var emailProvider = new EmailProvider(
                                "oraclepos@grandstores.ae",
                                "Pass@1234",
                                "mail.grandstores.ae",
                                25
                                ))
                            {
                                emailProvider.CreateMessage(
                                    From: "oraclepos@grandstores.ae",
                                    Subject: "Please process the home delivery order : " + Receipt + ".",
                                    Body: DeliveryHeader.getHtml(Receipt, UserFullName),
                                    IsBodyHtml: true,
                                    To: DeliveryHeader.GetEmailReceipients()
                                    );
                                emailProvider.AddAttachments(DeliveryHeader.attachmentName);
                                emailProvider.Send();
                            }
                        }
                        catch (Exception ex)
                        {
                            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                            {
                                db.ErrorLog(ex.Message, "AddDeliveryJobWithAttachment");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                    {
                        db.ErrorLog(ex.Message, "AddDeliveryJobWithAttachment");
                    }
                }
            }
            return Receipt;
        }

        [HttpPost()]
        public async Task<DeliveryHeader> SetDeliveryJobsWithAttachment(DeliveryHeader DeliveryJob)
        {
            #region LOG
            /*
            string requestName = @"api/Transaction/AddTransaction";
            using (DataProvider provider = new DataProvider(ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString))
            {
                var jsonSerial = new JavaScriptSerializer();
                string parameter = jsonSerial.Serialize(transaction);
                provider.Log(requestName, parameter);
            }
            */
            #endregion
            List<DeliveryHeader> result = null;
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        DeliveryHeader temp = new DeliveryHeader();
                        DeliveryJob = JsonConvert.DeserializeAnonymousType<DeliveryHeader>(val, temp);
                        Trace.WriteLine(string.Format("{0}: {1}", key, val));
                    }
                }


                var httpRequest = HttpContext.Current.Request;

            }
            catch (System.Exception e)
            {
                try
                {
                    using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                    {
                        db.ErrorLog(e.Message, "SetDeliveryJobsWithAttachment");
                    }
                }
                catch (Exception x)
                {
                    Debug.Assert(true, x.Message);
                }
                Debug.Print(e.Message);
            }
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                DeliveryJob = db.UpdateDelivery(DeliveryJob);
            }
            return DeliveryJob;
        }

        [HttpPost]
        public string SaveAttachment()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/delivery_attachments/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        return filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    db.ErrorLog(ex.Message, "SaveAttachment");
                }
            }
            return string.Empty;
        }
        [HttpPost]
        public string SaveAttachmentByte()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/delivery_attachments/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        return filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    db.ErrorLog(ex.Message, "SaveAttachment");
                }
                return "file";
            }
            return string.Empty;
        }

        [HttpPost]
        public string SaveSales(DeliveryHeader DeliveryHeader)
        {
            #region LOG   
            try
            {
                string requestName = @"DeliveryJobAttachment/SaveSales";
                using (SalesManageDataContext provider = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    var jsonSerial = new JavaScriptSerializer();
                    string parameter = jsonSerial.Serialize(DeliveryHeader);
                    provider.Log(requestName, parameter);
                }
            }
            catch (Exception) { }
            #endregion
            string Receipt = string.Empty;
            try
            {
                using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    Receipt = db.AddDelivery(DeliveryHeader);
                }
                using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("SALES_MANAGER")))
                {
                    db.AddSales(DeliveryHeader);
                }
                if (Receipt != null && DeliveryHeader.Status == 1)
                {
                    try
                    {
                        using (var emailProvider = new EmailProvider(
                            "oraclepos@grandstores.ae",
                            "Pass@1234",
                            "mail.grandstores.ae",
                            25
                            ))
                        {
                            var emailTo = DeliveryHeader.GetEmailReceipients();
                            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                            {
                                db.ErrorLog("Start sending mail", "SaveSalesEmail");
                            }
                            string UserFullName = string.Empty;
                            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                            {
                                var user = db.getUserDetail(DeliveryHeader.UserName);
                                if (user != null) { UserFullName = user.full_name; }
                            }
                            emailProvider.CreateMessage(
                                From: "oraclepos@grandstores.ae",
                                Subject: "Please process the home delivery order : " + Receipt + ".",
                                Body: DeliveryHeader.getHtml(Receipt, UserFullName),
                                IsBodyHtml: true,
                                To: emailTo
                                );
                            emailProvider.AddAttachments(DeliveryHeader.attachmentName);
                            emailProvider.SendAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                        {
                            db.ErrorLog(ex.StackTrace, "SaveSalesEmail");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
                {
                    db.ErrorLog(ex.Message, "SaveSales");
                }
            }

            return Receipt;
        }
    }
}
