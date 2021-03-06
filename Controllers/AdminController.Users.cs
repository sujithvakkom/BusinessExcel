﻿using BusinessExcel.Models;
using BusinessExcel.Providers;
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
    [Authorize(Roles = BERoleDetails.SYSTEM_ADMINISTRATOR)]
    public partial class AdminController : Controller
    {


        public static string USERSLIST = "UserList";
        public static string NEWUSER = "NewUser";
        public static string INDEXNEWUSER = "IndexNewUser";
        public static string USERSLIST_TITLE = "User List";
        public static string USERSTREE_TITLE = "User Hierarchy";
        public static string USERSTREE = "UserTree";
        public static string SELECTED_FILTED_USER = "SelectedFilteredUser";
        public static string SELECTED_FILTED_ROLE = "SelectedFilteredUserRole";
        public static string SELECTED_FILTED_GROUP = "SelectedFilteredUserGroup";

        public static string SELECTED_FILTED_USER_FIRST_NAME = "SelectedFilteredUserFirstName";
        public static string JSON_USERSLIST = "UserTreeList";


        public static string ADD_CHILDUSER = "AddChild";
        public static string DELETE_CHILDUSER = "DeleteChild";


        public static string USERTREEVIEW = "_UserTreeView";

        public static string NEWUSER_FRM = "_NewUser";
        public static string USERCREATIONMESSAGE = "User created";


        public PartialViewResult IndexNewUser()
        {
            return PartialView(NEWUSER_FRM);
        }


        // [Authorize(Roles = "manager")]
        [HttpPost]
        [Authorize(Roles = "System Administrator")]
        public ActionResult NewUser(CreateUser newUser)
        {

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {

                    using (var db = new SalesManageDataContext())
                    {

                        ViewData[USERCREATIONMESSAGE] = db.CreateUser(newUser);
                        ModelState.Clear();

                    }

                    return PartialView(NEWUSER_FRM, new CreateUser());
                }
                else
                {
                    return PartialView(NEWUSER_FRM, newUser);
                }
            }
            else
            {
                return PartialView(NEWUSER_FRM, new CreateUser());
            }

        }

        [Authorize(Roles = "System Administrator")]
        [HttpGet]
        public ActionResult UserList(string sort, string sortdir, int page = 1, UserViewFilters Filters = null)
        {


            ViewBag.UserUpateViewSort = sort;
            ViewBag.UserUpateViewDir = sortdir;
            ViewBag.UserUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERSLIST_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            //ViewBag.Title = ACTIONS_TITLE;
            int count;
            if (!string.IsNullOrEmpty(Filters.user_name))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER] = db.getUserListView(
                        search: Filters.first_name,
                        UserType: null,
                        Page: page,
                        RowCount: out count
                        );
                }

            if (!string.IsNullOrEmpty(Filters.first_name))
                using (var db = new SalesManageDataContext())
                {
                    ViewData[SELECTED_FILTED_USER_FIRST_NAME] = db.getUserDetailByName(Filters.first_name);
                }

            //if (!string.IsNullOrEmpty(Filters.LocationID))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        ViewData[SELECTED_FILTED_LOCATION] = db.getLocationDetail(Filters.LocationID);
            //    }

            //if (!string.IsNullOrEmpty(Filters.TargetID))
            //    using (var db = new SalesManageDataContext())
            //    {
            //        ViewData[SELECTED_FILTED_TARTET] = db.getTargetDetail(Filters.TargetID);
            //    }



            //if (Request.IsAjaxRequest())
            //{
            //return PartialView(TABLEDAILYUPATEVIEW, Filters);
            return (PartialViewResult)UserUpateView(sort, sortdir, page, Filters);
            //}
            //return View(Filters);





        }
        public static string USERUPDATEVIEW = "UserList";
        ///Report/Actions?sort=CreateTime&sortdir=ASC&page=2
        public PartialViewResult UserUpateView(string sort, string sortdir, int page = 1, UserViewFilters Filters = null)
        {
            ViewBag.UserUpateViewSort = sort;
            ViewBag.UserUpateViewDir = sortdir;
            ViewBag.UserUpateViewPage = page;
            ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERSLIST_TITLE;
            ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
            ViewBag.Title = USERSLIST_TITLE;
            return PartialView(USERUPDATEVIEW, Filters);
        }

        //public static string USERTREEVIEW = "UserTree";
        //public PartialViewResult UserTreeView()
        //{

        //    ViewBag.Title = ConfigurationManager.AppSettings["ApplicationName"] + " | " + USERSLIST_TITLE;
        //    ViewBag.UserProfile = (string)Session[Index.USER_PROFILE_INDEX];
        //    ViewBag.Title = USERSTREE_TITLE;
        //    return PartialView(USERTREEVIEW);
        //}


        public static string USER_TREE_UPDATEVIEW = "UserTreeUpateView";
        public PartialViewResult UserTreeUpateView()
        {
            IEnumerable<UserTree> data = getUserTreeList();
            return PartialView(USERTREEVIEW, data);
        }

        [Authorize(Roles = "System Administrator")]
        [HttpGet]
        public ActionResult UserTree()
        {
            //List<UserTree> categoryList = new List<UserTree>();
            //using (var db = new SalesManageDataContext())
            //{
            //    categoryList = db.getUserTree();
            //}
            //var rootCategory = categoryList.Where(x => x.level_v == 0).FirstOrDefault();
            //IEnumerable<UserTree> data = null;
            //if (categoryList.Count > 0)
            //{
            //    SetChildren(rootCategory, categoryList);
            //    var model = new List<UserTree>();
            //    data = new[] { rootCategory };


            //  IEnumerable<UserTree> data = getUserTreeList();
            if (Request.IsAjaxRequest())
            {
                // return PartialView(USERSTREE, data);
                return PartialView(USERSTREE);
            }
            else
            {
                return View();
            }
            //  }           
            //   return View(data);
        }

        //public IEnumerable<UserTree> getUserTreeList()
        //{
        //    IEnumerable<UserTree> data = null;
        //    try
        //    {
        //        List<UserTree> categoryList = new List<UserTree>();

        //        using (var db = new SalesManageDataContext())
        //        {

        //            categoryList = db.getUserTree();

        //        }

        //        var rootCategory = categoryList.Where(x => x.level_v == 0).FirstOrDefault();

        //        if (categoryList.Count > 0)
        //        {

        //            SetChildren(rootCategory, categoryList);

        //            var model = new List<UserTree>();

        //            data = new[] { rootCategory };

        //        }
        //    }
        //    catch
        //    {
        //        data = null;
        //    }
        //    return data;
        //}


        public IEnumerable<UserTree> getUserTreeList()
        {
            IEnumerable<UserTree> data = null;






            try
            {
                List<UserTree> categoryList = new List<UserTree>();

                int vid = 0;
                using (var db = new SalesManageDataContext())
                {

                    categoryList = db.getUserTree();
                    vid = db.getViewer_Id();
                }

                //var rootCategory = categoryList.Where(x => x.level_v == 0 ).FirstOrDefault();

                var rootCategory = categoryList.Where(x => x.level_v == 0).FirstOrDefault();

                if (rootCategory == null)
                {
                    rootCategory = categoryList.Where(x => x.user_id == vid).FirstOrDefault();
                }

                if (categoryList.Count > 0)
                {



                    SetChildren(rootCategory, categoryList);

                    var model = new List<UserTree>();


                    data = new[] { rootCategory };


                }
            }
            catch
            {
                data = null;
            }
            return data;
        }
        private void SetChildren(UserTree model, List<UserTree> catList)
        {

            model.left_v = catList.Where(c => c.parent_id == model.parent_id).Select(a => a.left_v).Single();
            model.right_v = catList.Where(c => c.parent_id == model.parent_id).Select(a => a.right_v).Single();
            model.level_v = catList.Where(c => c.parent_id == model.parent_id).Select(a => a.level_v).Single();
            model.entity = catList.Where(c => c.parent_id == model.parent_id).Select(a => a.entity).Single();


            var childs = catList.Where(c => c.left_v > model.left_v && c.right_v < model.right_v).OrderBy(x => x.level_v).ToList();

            if (childs.Count > 0)
            {


                var min = childs.Min(t => t.level_v);


                var newlist = childs.Where(c => c.level_v == min);


                foreach (var child in newlist)
                {


                    SetChildren(child, catList);

                    if (model.Childs.Contains(child))
                    {
                        continue;
                    }

                    model.Childs.Add(child);
                }

            }

        }
        //private void SetChildren(UserTree model, List<UserTree> catList)
        //{

        //    model.left_v = catList.Where(c => c.parent_id == model.parent_id).Select(a => a.left_v).Single();
        //    model.right_v = catList.Where(c => c.parent_id == model.parent_id).Select(a => a.right_v).Single();
        //    model.level_v = catList.Where(c => c.parent_id == model.parent_id).Select(a => a.level_v).Single();
        //    model.entity = catList.Where(c => c.parent_id == model.parent_id).Select(a => a.entity).Single();



        //    var childs = catList.Where(c => c.left_v > model.left_v && c.right_v < model.right_v).OrderBy(x => x.level_v).ToList();

        //    if (childs.Count > 0)
        //    {


        //        var min = childs.Min(t => t.level_v);


        //        var newlist = childs.Where(c => c.level_v == min);


        //        foreach (var child in newlist)
        //        {

        //            SetChildren(child, catList);


        //            model.Childs.Add(child);
        //        }
        //    }

        //}

        public JsonResult UserTreeList()
        {

            var lst = getUserTreeList();


            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult AddChild(string paret_user_name,string new_user_name)
        //{

        //    int insertId = 0;
        //    using (var db = new SalesManageDataContext())
        //    {
        //        insertId= db.InsertUserTree(paret_user_name, new_user_name);
        //    }



        //    return Json(insertId, JsonRequestBehavior.AllowGet);
        //}
        public PartialViewResult AddChild(string paret_user_name, string new_user_name, string ParentId)
        {

            int insertId = 0;// SaveMaster(master);

            using (var db = new SalesManageDataContext())
            {
                insertId = db.InsertUserTree(paret_user_name, new_user_name, Convert.ToInt32(ParentId));
            }
            if (insertId > 0)
            {
                IEnumerable<UserTree> data = getUserTreeList();
                return PartialView(USERTREEVIEW, data);
            }

            return PartialView();
        }


        //public JsonResult DeleteChild(string user_name)
        //{
        //    int deleteId = 0;// SaveMaster(master);

        //    using (var db = new SalesManageDataContext())
        //    {
        //        deleteId= db.DeleteUserTree(user_name);
        //    }

        //    return Json(deleteId, JsonRequestBehavior.AllowGet);
        //}

        public PartialViewResult DeleteChild(string user_name, string parent)
        {

            int deleteId = 0;// SaveMaster(master);

            using (var db = new SalesManageDataContext())
            {
                deleteId = db.DeleteUserTree(user_name, Convert.ToInt32(parent));
            }
            if (deleteId > 0)
            {
                IEnumerable<UserTree> data = getUserTreeList();
                return PartialView(USERTREEVIEW, data);
            }

            return PartialView();
        }
    }
}
