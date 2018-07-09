
using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessExcel.Providers.ProviderContext.Entities;
using BusinessExcel.Models;
using BusinessExcel.Extentions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;

namespace BusinessExcel.Providers.ProviderContext
{

    public partial class SalesManageDataContext : DbContext
    {

        public IQueryable<TargetAchievementView> TargetAchievementViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count,
            TargetAchievementView Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;
          
             //var res = this.TargetAchievementView.Select(x=>x);

            var res = this.Database.SqlQuery<TargetAchievementView>("[db_salesmanage_user].[User_Target_Achieved_Data]").ToList().AsQueryable();

            if (Filters.user_id !=null)
            {
                res = res.Where(x => x.user_id == Filters.user_id);
             
            }

            if(Filters.Month !=null)
            {
                int month = DateTime.ParseExact(Filters.Month, "MMM", CultureInfo.InvariantCulture).Month;

                 res = res.Where(x =>  month<= x.start_date.Value.Month && month >= x.start_date.Value.Month);
            }

          
            count = res.Count();

            if (sort == null || sort == "")
            {
                return res.OrderByDescending(x => x.Target_amt)
                    .Skip(skippingRows).Take(pageSize);
            }
            else {
                switch (sortdir)
                {
                    case "DESC":
                        return res.OrderByDescending(sort)
                            .Skip(skippingRows).Take(pageSize);
                    default:
                        return res.OrderBy(sort)
                            .Skip(skippingRows).Take(pageSize);
                }
            }
            /*
            switch (sort)
            {
                case "CreateTime":
                    count = this.DailyUpateView.Count();
                    if (sortdir == "ASC")
                        return this.DailyUpateView.OrderBy(x => x.CreateTime)
                            .Skip(skippingRows).Take(pageSize);
                    return this.DailyUpateView.OrderByDescending(x => x.CreateTime)
                        .Skip(skippingRows).Take(pageSize);

                default:
                    count = this.DailyUpateView.Count();
                    return this.DailyUpateView.OrderBy(x => x.UserId)
                        .Skip(skippingRows).Take(pageSize);
            }
            */
        }


        public IQueryable<TargetAchievementView> TargetAchievementViewPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count)
        {
            int skippingRows = (pageNumber - 1) * pageSize;

            //var res = this.TargetAchievementView.Select(x => x);

            var res = this.Database.SqlQuery<TargetAchievementView>("[db_salesmanage_user].[User_Target_Achieved_Data]").ToList().AsQueryable();

            switch (sort)
            {
                case "CreateTime":
                    count = this.TargetAchievementView.Count();
                    if (sortdir == "ASC")
                        return this.TargetAchievementView.OrderBy(x => x.Target_amt)
                            .Skip(skippingRows).Take(pageSize);
                    return this.TargetAchievementView.OrderByDescending(x => x.Target_amt)
                        .Skip(skippingRows).Take(pageSize);
                default:
                    count = this.TargetAchievementView.Count();
                    return this.TargetAchievementView.OrderBy(x => x.Target_amt)
                        .Skip(skippingRows).Take(pageSize);
            }
        }


        public virtual UserTargetDetailsView getUserTargetDetails(UserTargetDetailsView users )
        {
           

            if (users.UserID <=0)
            {
                users.UserID = 228;
            }

            if (users.UserName != null)
                users.UserID = getUserID(users.UserName);

            users.start_date = Convert.ToDateTime("01-jul-2018");
                var res = this.Database.SqlQuery<UserTargetDetailsView>("[db_salesmanage_user].[getUserTargetDetails]").ToList().AsQueryable();

          //  var res = this.DailyUpateView.Select(x => x);
            if (users.UserID != 0)
            {
                res = res.Where(x => x.UserID == users.UserID && users.start_date<= x.start_date && users.start_date >= users.start_date);
            }


                
               //if(users.Location_Id !=0)
               // {
                    
               //     res = res.Where(x =>x.Location_Id==users.Location_Id);
               // }

            return res.FirstOrDefault();
        }

        public virtual IEnumerable< UserTargetDetailsView> getAllUserTargetDetails(UserTargetDetailsView users)
        {



            var res = this.Database.SqlQuery<UserTargetDetailsView>("[db_salesmanage_user].[getUserTargetDetails]").ToList().AsQueryable();

            //  var res = this.DailyUpateView.Select(x => x);
            if (users.UserID != 0)
            {
                res = res.Where(x => x.UserID == users.UserID);
            }



            if (users.Location_Id != 0)
            {

                res = res.Where(x => x.Location_Id == users.Location_Id);
            }

            return res;
        }


        public List<TargetMaster> TargetMasterViewPage(int user_Id,int roster_id)
        {
    

            var res = this.TargetMaster.Select(x => x);


            if (user_Id >0)
            {
                res = res.Where(x => x.UserID == user_Id && x.RosterID ==roster_id);
            }


            return res.ToList();
      
        }

    }
}
