using BusinessExcel.Helpers;
using DataProvider;
using System;
using System.Web.Http;

namespace HomeDelivery.Controllers.API
{
    public class BiometricAPIController : ApiController
    {

        [HttpGet]
        public string GetBioMetric(string UserName, string SignatureString, string Session)
        {
            string result = null;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                result = db.getBiometric(UserName, SignatureString);
            }
            return result;
        }

        [HttpPost]
        public string UpdateBioMetric(string UserName, string SignatureString, string Session)
        {
            int result = 0;
            using (SalesManageDataContext db = new SalesManageDataContext(AppConfigSettings.WebConfigConnectionSting("GSLSR")))
            {
                var userDetails = db.getUserDetail(UserName, Session);
                if (string.IsNullOrEmpty(userDetails.signature))
                    try
                    {
                        result = db.updateBioMetric(UserName, SignatureString);
                    }
                    catch (Exception) {
                        return null;
                    }
                else
                    return userDetails.signature;
            }
            return SignatureString;
        }
    }
}