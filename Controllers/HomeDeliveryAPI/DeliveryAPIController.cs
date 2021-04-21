using BusinessExcel.Helpers;
using DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HomeDelivery.Controllers.API
{
    public class DeliveryAPIController : ApiController
    {
        [HttpPost]

        public string AddDelivery(string itemcode, string customerName, string customerPhone, string customerEmirate, string pONumber, int quantity, string userName, string address, string remarks,
            DateTime saleDate, DateTime preferedDate)
        {
            string result = null; string filePath = "";

            Dictionary<string, object> dict = new Dictionary<string, object>();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, dict);
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            response = Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            response= Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                             filePath = string.Concat(HttpContext.Current.Server.MapPath("~/delivery_attachments/"), postedFile.FileName + extension);
                            //var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);
                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    response = Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                response = Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                response = Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.AddDelivery(itemcode, customerName, customerPhone,
                    customerEmirate, pONumber, quantity, userName, address ,remarks, saleDate,preferedDate);
                if (!string.IsNullOrEmpty(filePath))
                    db.UpdateAttachment(result, filePath);
            }

            return result;
        }
    }
}
