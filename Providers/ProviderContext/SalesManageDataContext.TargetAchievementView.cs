
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
using System.Web.Security;

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

            if (Filters.target_id != null)
            {
                res = res.Where(x => x.target_id == Filters.target_id);



            }
            //if(Filters.Month !=null)
            //{
            //    int month = DateTime.ParseExact(Filters.Month, "MMM", CultureInfo.InvariantCulture).Month;

            //     res = res.Where(x =>  month<= x.start_date.Value.Month && month >= x.start_date.Value.Month);


            //}


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
        //public List<TargetTotalView> LoadTargetTotal(int user_id, int target_id)
        //{


        //    List<TargetTotalView> items = null;
        //    using (var db = new SalesManageDataContext())
        //    {


        //        items = db.getUsertargetTotalDetails(user_id, target_id).ToList();


        //    }

        //    return items;

        //}

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


        public virtual UserTargetDetailsView getUserTargetDetails(UserTargetDetailsView users)
        {

            try
            {

                //int login_member_user_id = (int)Membership.GetUser().ProviderUserKey;

                //int login_user_id = this.getUserID_byMembershipUserId(login_member_user_id);

                int viD = getViewer_Id();

                if (users.UserName != null)
                    users.UserID = getUserID(users.UserName);

                //  users.start_date = Convert.ToDateTime("01-jul-2018");
                //  users.end_date = Convert.ToDateTime("01-Aug-2018");


                var user_id = users.UserID > 0 ?
                    new SqlParameter("@user_id", users.UserID) :
                    new SqlParameter("@user_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };

                var start_date = users.start_date != default(DateTime) ?
                    new SqlParameter("@start_date", users.start_date.Value.ToString("dd/MMM/yyyy")) :
                    new SqlParameter("@start_date", System.Data.SqlDbType.Int) { Value = DBNull.Value };

                var viewer_id = viD > 0 ?
                 new SqlParameter("@viewer_id", viD) :
                 new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };

                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(user_id);
                parameterList.Add(start_date);
                parameterList.Add(viewer_id);


                //userlist = this.Database.SqlQuery<UserTargetDetailsView>("[db_salesmanage_user].[getUserTargetDetails] @user_id,@start_date", parameterList.ToArray()).ToList();

                var res = this.Database.SqlQuery<UserTargetDetailsView>("[db_salesmanage_user].[getUserTargetDetails] @start_date, @user_id,@viewer_id", parameterList.ToArray()).ToList().AsQueryable(); ;

                List<UserTargets> targets = new List<UserTargets>();
                 targets = res.Select(g => new UserTargets()
                {
                    target_id = g.target_id,
                    start_date = g.start_date,
                    end_date = g.end_date,
                    Month = g.Month_Name
                }).ToList();

                if (targets.Count > 0)
                {
                    users = res.Take(1).FirstOrDefault();
                    users.UserTargets = targets;
                }
                else
                {
                    users.UserTargets = targets;
                }
                //List<UserTargets> target = res.OfType<UserTargets>().Select(i => new { i.target_id, i.start_date.Value }).ToList();


                //List<UserTargets> target = res.Select(i => new { i.target_id, i.start_date.Value }).ToList();


                //     var res = this.Database.SqlQuery<UserTargetDetailsView>("[db_salesmanage_user].[getUserTargetDetails]").ToList().AsQueryable();

                //// //res = res.Where(x => x.UserID == users.UserID && users.start_date<= x.start_date && users.start_date >= users.start_date);

                // if (res.Any())
                // {
                //     var target = res.Where(x => x.UserID == users.UserID).OrderByDescending(x => x.start_date).First();



                return users;
              
            }
            catch(Exception ex)
            {
                return users;
            }
        }

        /// <summary>
        /// Returns users target of previous & next three months
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public virtual UserTargetDetailsView getUserTargets(int user_id)
        {
         
            try
            {


                UserTargetDetailsView userDet = new UserTargetDetailsView();

                var UID = user_id > 0 ?
                    new SqlParameter("@user_id", user_id) :
                    new SqlParameter("@user_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };

                System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(UID);
           

                var res = this.Database.SqlQuery<UserTargetDetailsView>("[db_salesmanage_user].[get_user_target_prvs_nxt_three_months]  @user_id", parameterList.ToArray()).ToList().AsQueryable(); ;

                

                //Any targets available  within period
                if (res.Any())
                {
                    userDet = res.Take(1).FirstOrDefault();

                    List<UserTargets> utList = new List<UserTargets>();

                    UserTargets ut = new UserTargets();

                    foreach (UserTargetDetailsView det in res)
                    {


                        ut = new UserTargets();

                        ut.target_id = det.target_id;
                        ut.start_date = det.start_date;
                        ut.end_date = det.end_date;
                        ut.TargetDetailsView = getUsertargetAchievementDetailsView(user_id, det.target_id);
                        ut.TargetTotalView = getUsertargetTotalDetails(det.target_id);
                        utList.Add(ut);
                     
                    }
                    userDet.UserTargets = utList;
                    return userDet;
                }


                return null;

            }
            catch (Exception ex)
            {
                return  null;
            }
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

        public IQueryable<TargetSummaryView> TargetSummaryReportPaging(int pageNumber, int pageSize, string sort, String sortdir, out int count,
         TargetSummaryView Filters)
        {
            int skippingRows = (pageNumber - 1) * pageSize;

            if (Filters.user_name != null)
            {
                Filters.user_id = getUserID(Filters.user_name);
            }
            int VId = getViewer_Id();

            var viewer_id = VId > 0 ?
              new SqlParameter("@viewer_id", VId) :
              new SqlParameter("@viewer_id", System.Data.SqlDbType.Int) { Value = DBNull.Value };

            var user_id_p = Filters.user_id != null ?
           new SqlParameter("@user_id_p", Filters.user_id) :
           new SqlParameter("@user_id_p", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var location_id_p = Filters.location_id != null ?
        new SqlParameter("@location_id_p", Filters.location_id) :
        new SqlParameter("@location_id_p", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };


            var target_id = Filters.target_id != null ?
new SqlParameter("@target_id", Filters.target_id) :
new SqlParameter("@target_id", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var target_status_p = Filters.target_status_id>0 ?
    new SqlParameter("@target_status_p", Filters.target_status_id) :
    new SqlParameter("@target_status_p", System.Data.SqlDbType.NVarChar) { Value = DBNull.Value };

            var page_number = pageNumber > 0 ?
               new SqlParameter("@page_number", pageNumber) :
               new SqlParameter("@page_number", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            var page_size = pageSize > 0 ?
                new SqlParameter("@page_size", pageSize) :
                new SqlParameter("@page_size", System.Data.SqlDbType.Int) { Value = DBNull.Value };


            int? row = null;
            var row_count = row != null ?
                new SqlParameter("@row_count", row) :
                new SqlParameter("@row_count", System.Data.SqlDbType.BigInt) { Value = DBNull.Value };


            row_count.Direction = System.Data.ParameterDirection.Output;

            System.Collections.Generic.List<SqlParameter> parameterList = new List<SqlParameter>();

            parameterList.Add(target_id);
            parameterList.Add(target_status_p);

            parameterList.Add(user_id_p);
            parameterList.Add(location_id_p);
            parameterList.Add(viewer_id);

            parameterList.Add(page_number);
            parameterList.Add(page_size);
            parameterList.Add(row_count);

         

            var res = this.Database.SqlQuery<TargetSummaryView>("[db_salesmanage_user].[Target_Summary_Report] @target_id,@target_status_p,@user_id_p,@location_id_p,@viewer_id,@page_number,@page_size,@row_count OUTPUT", parameterList.ToArray()).ToList().AsQueryable();
            int.TryParse(row_count.Value.ToString(), out count);

            // count = res.Count();
            // return res.Skip(skippingRows).Take(pageSize);
            if (sort == null || sort == "")
            {
                return res.OrderByDescending(x => x.target_id);
            }
            else
            {
                switch (sortdir)
                {
                    case "DESC":
                        return res.OrderByDescending(sort);
                    default:
                        return res.OrderBy(sort);
                }
            }

        }
    }
}
