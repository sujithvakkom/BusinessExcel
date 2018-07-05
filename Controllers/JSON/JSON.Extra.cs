using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BusinessExcel.Controllers.JSON
{
    public partial class JSONController : Controller
    {
        private object ParseJSONString(string JSONSttring)
        {
            if (JSONSttring == null) return null;
            JavaScriptSerializer JSON = new JavaScriptSerializer();
            object result = JSON.DeserializeObject(JSONSttring);
            return result;
        } 
    }
}