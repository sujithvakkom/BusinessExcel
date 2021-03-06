﻿using BootstrapHtmlHelper;
using BusinessExcel.Models;
using BusinessExcel.Providers.ProviderContext;
using BusinessExcel.Providers.ProviderContext.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BusinessExcel.Controllers
{
    public partial class TargetController : Controller
    {

        public static string _GETLOCATIONUSERALOCATION = "_GetLocationUserAlocation";

        [HttpPost]
        public ActionResult _GetLocationUserAlocation(string Location, string TargetTemplate)
        {
            return PartialView(_GETLOCATIONUSERALOCATION,TargetTemplate);
        }

    }
}