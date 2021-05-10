using BusinessExcel.Providers.ProviderContext.Entities;
using BEProvider = BusinessExcel.Providers.ProviderContext;
using DataProvider;
using DataProvider.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;

namespace HomeDelivery.Controllers.API
{
    public class MobileTargetAPIController : ApiController
    {
        [HttpGet()]
        public List<UserTargetDetailsView> UserTarget(string UserId)
        {
            List<UserTargetDetailsView> result = null;
            using (SalesManageDataContext db = new SalesManageDataContext("SALES_MANAGER"))
            {
                result = db.getUserTargetDetails(UserId);
            }
            decimal TotalTarget = 0;
            decimal TotalAcheeviment = 0;
            decimal TolalPercentage = 0;
            decimal TolalIncentive = 0;
            decimal TolalAcc = 0;
            decimal TolalEntered = 0;

            foreach (var tar in result)
            {
                TotalTarget = 0;
                TotalAcheeviment = 0;
                TolalPercentage = 0;
                TolalIncentive = 0;
                TolalAcc = 0;
                TolalEntered = 0;
                tar.TotalIncAmt = 0;
                tar.TotalAmountAcc = 0;
                foreach (var line in tar.Lines)
                {
                    try
                    {
                        GlobalSettings globalSettings;
                        CategoryDetail categoryDetails;
                        CategoryDetail catDetails;
                        int count;
                        using (BEProvider.SalesManageDataContext db = new BEProvider.SalesManageDataContext())
                        {
                            catDetails = db.getCategoryDetails(line.Catogery);
                            globalSettings = db.getGlobalSettings();
                            var temp = db.getCategorySettingsDetails(catDetails.category_id, 1, out count);
                            categoryDetails = count > 0 ? temp[0] : null;
                        }

                        if (catDetails != null &&
                            categoryDetails != null &&
                            categoryDetails.base_incentive != null && (line.Target ?? 0) != 0 &&
                            globalSettings.min_line_achievement < ((line.Achievement ?? 0) / line.Target))
                        {
                            line.TotalIncAmt = (line.Target ?? 0) == 0 ? 0 : (categoryDetails.base_incentive * ((line.Achievement ?? 0) / line.Target));
                            line.TotalAmountAcc = globalSettings.AccelerationFactor * 100;
                        }
                        else if (categoryDetails != null && globalSettings.min_line_achievement > (line.Achievement ?? 0 / line.Target ?? -1))
                        {
                            line.TotalAmountAcc = globalSettings.DeAccelerationFactor * 100 * -1;
                        }
                        TolalIncentive += line.TotalIncAmt ?? 0;
                        TolalAcc += line.TotalAmountAcc ?? 0;
                        //TolalEntered += line.TotalEnteredIncAmt ?? 0;
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                tar.TotalIncAmt = TolalIncentive;
                tar.TotalAmountAcc = TolalAcc;
            }

            return result;
        }
    }
}
