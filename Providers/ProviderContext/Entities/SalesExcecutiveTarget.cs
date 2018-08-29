using BusinessExcel.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class SalesExcecutiveTarget:BaseTarget
    {

        public SalesExcecutiveTarget()
        {
            LineTargets = new List<LineTarget>(6) {
                new LineTarget(),
                new LineTarget(),
                new LineTarget(),
                new LineTarget(),
                new LineTarget(),
                new LineTarget()
            };
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            
        }
        internal override int Save(out string Message)
        {
            using (var db = new SalesManageDataContext())
            {
                return db.createUpdateTargetSE(this, out Message);
            }
        }

        internal void init()
        {

            using (var db = new SalesManageDataContext())
            {
                var CategoryTarget = db.getCategoryTarget(UserName,StartDate,EndDate);
                if(CategoryTarget!=null)
                for (int i = 0; i < CategoryTarget.Count; i++)
                {
                    LineTargets[i].Catogery = CategoryTarget[i].category_id.ToString();
                    LineTargets[i].IsBonusLine = CategoryTarget[i].has_bonus;
                    LineTargets[i].Target = CategoryTarget[i].value;
                }
            }
        }
    }
    public class SalesExcecutiveQuarterTarget{
        private decimal global_acc_factor;
        private decimal global_base_incentive_cap;
        private decimal? base_incentive;

        public SalesExcecutiveQuarterTarget() {
        }
        public List<SalesExcecutiveTarget> SalesExcecutiveTargets { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Quarter")]
        public string Quarter_Name { get; set; }

        internal void CreateLines()
        {
            DateTime startDate = Constands.getQuarterStartDate(this.Quarter_Name);
            SalesExcecutiveTargets = new List<Entities.SalesExcecutiveTarget>(3) {
                new SalesExcecutiveTarget()
                {
                    StartDate = startDate ,
                    EndDate = startDate.AddMonths(1).AddDays(-1),
                    UserName = this.UserName
                },
                new SalesExcecutiveTarget() {
                    StartDate = startDate.AddMonths(1) ,
                    EndDate = startDate.AddMonths(2).AddDays(-1),
                    UserName = this.UserName
                },
                new SalesExcecutiveTarget() {
                    StartDate = startDate.AddMonths(2),
                    EndDate = startDate.AddMonths(3).AddDays(-1),
                    UserName = this.UserName
                }
            };
            foreach (var t in SalesExcecutiveTargets)
            {
                t.init();
            }
        }

        internal void Save()
        {
            using (var db = new SalesManageDataContext())
            {
                var globalSettings = db.getGlobalSettings();
                this.global_acc_factor = globalSettings.global_acc_factor;
                this.global_base_incentive_cap = globalSettings.global_base_incentive_cap;
                var sum = (from x in this.SalesExcecutiveTargets
                           group x by x.UserName into g
                           select g.Sum(i => i.TotalTarget)).SingleOrDefault();
                this.base_incentive = sum * global_acc_factor;
                if (this.base_incentive > global_base_incentive_cap) this.base_incentive = global_base_incentive_cap;
                foreach (var x in this.SalesExcecutiveTargets)
                {
                    try
                    {
                        x.BaseIncentive = this.base_incentive * (sum / x.TotalTarget);
                        x.Location = "141";
                    }
                    catch (DivideByZeroException)
                    {
                        x.BaseIncentive = global_base_incentive_cap / 3;
                    }
                }
            }
            foreach (var x in this.SalesExcecutiveTargets)
            {
                x.base_incentive_qtr = this.base_incentive;
                string message = "";
                x.Save(out message);
            }
        }
    }
}