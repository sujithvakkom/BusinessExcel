using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessExcel.Providers.ProviderContext.Entities
{
    public class BaseTarget
    {
        public BaseTarget() {
            LineTargets = new List<LineTarget>();
            LineTargets.Add(
                new LineTarget()
                );
            LineTargets.Add(
                new LineTarget()
                );
            LineTargets.Add(
                new LineTarget()
                );
            LineTargets.Add(
                new LineTarget()
                );
            LineTargets.Add(
                new LineTarget()
                );
            LineTargets.Add(
                new LineTarget()
                );
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
        }

        public List<LineTarget> LineTargets { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Total Target")]
        public Decimal TotalTarget { get; set; }

        [Display(Name = "Base Incentive")]
        public Decimal BaseIncentive { get; set; }

        internal void Save()
        {
            using (var db = new SalesManageDataContext())
            {
                db.TargetMaster.Add(
                    new TargetMaster()
                    {
                        BaseIncentive = this.BaseIncentive,
                        Description = String.IsNullOrEmpty(Description) ? null : this.Description,
                        UserID = db.getUserID(this.UserName)
                    });

                db.SaveChanges();
                int i = 0;
                foreach (var tarLin in this.LineTargets) {
                    if (!string.IsNullOrEmpty(tarLin.Catogery)) { 
                    db.TargetDetails.Add(
                        new TargetDetail()
                        {
                            TargetLineID=++i,
                            Achievement = tarLin.Achievement,
                            CategoryID = int.Parse( tarLin.Catogery),
                            HasBonus = tarLin.IsBonusLine,
                            Value = tarLin.Target,
                            TargetID = 1
                        }
                        );
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}