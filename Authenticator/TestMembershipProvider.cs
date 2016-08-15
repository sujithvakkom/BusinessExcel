
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;
using WebMatrix.Data;
using WebMatrix.WebData.Resources;
using BusinessExcel.Models;
using System.Data.Entity;

namespace WebMatrix.WebData
{
    public class TestMembershipProvider : SimpleMembershipProvider
    {
    }
}