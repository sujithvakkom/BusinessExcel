using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessExcel.Controllers
{
    public class MobileController : Controller
    {
        //
        // GET: /Mobile/
        #region ACHIEVEMENT_CMD
        public const string ACHIEVEMENT_CMD =
            @"SELECT usr.user_id ,
       usr.user_name ,
       sc_salesmanage_merchant.month_year ( r.start_date
                                          ) AS         Month ,
       c.description AS                                Category ,
       isnull(MAX(ut.value) , 0) AS              Target_amt ,
       isnull(SUM(ach.value) , 0) AS Achieved_Amt ,
       ut.has_bonus AS                                 has_bonus ,
       MIN(r.start_date) AS                            start_date ,
       MAX(r.end_date) AS                              end_date ,
       SUM(tar.base_incentive) AS                      Base_Incentive
FROM [db_salesmanage_user].[roster] AS r INNER JOIN [db_salesmanage_user].[target_m] AS tar ON r.[roster_id] = tar.[roster_id]
                                         INNER JOIN [db_salesmanage_user].[user_target] AS ut ON tar.target_id = ut.target_id
                                         INNER JOIN [sc_salesmanage_user].[user_m] AS usr ON tar.user_id = usr.user_id
                                         INNER JOIN sc_salesmanage_merchant.category AS c ON ut.category_id = c.category_id
                                         LEFT JOIN [sc_salesmanage_merchant].[daily_update_v] AS ach ON ach.user_id = ut.user_id
                                                                                                        AND
                                                                                                        ach.create_time BETWEEN r.start_date AND r.end_date
                                                                                                        AND
                                                                                                        ut.category_id = ach.category_id
																										WHERE usr.user_name= @user_name
GROUP BY usr.user_id ,
         usr.user_name ,
         sc_salesmanage_merchant.month_year ( r.start_date
                                            ) ,
         ut.has_bonus ,
         c.description
ORDER BY sc_salesmanage_merchant.month_year ( r.start_date
                                            ) DESC ,
         c.description";
        #endregion

        public const string MOBILE = @"Mobile";
        public const string TARGET = @"Target";
        public ActionResult Target(String UserName, String Date)
        {
            ViewBag.UserName = UserName;
            return View();
        }

    }
}
