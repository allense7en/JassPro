using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JassPro.Web.Models;
using JassPro.Web.Models.Attr;
using JassPro.BLL.Impl.Account;
using JassPro.BLL.Interface.Account;
using JassPro.Model.Account;
using JassPro.Web.Models.Filter;

namespace JassPro.Web.Controllers
{
    [GzipCompressFilter]
    [AuthorizeFilter]
    [ActionExceptionFilter]
    public class BaseController : Controller
    {
        protected static readonly string SessionUserKey = "CurrentAdmin";
                
        public SessionUser CurrentAdmin
        {
            get
            {
                if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    throw new Exception("用户未登录");//必须要用户Form验证后才能使用CurrentUser
                }
                /*
                if ((User == null || !User.Identity.IsAuthenticated) && user == null)
                {
                    throw new Exception("用户未登陆!");
                }*/
                else
                {
                    object user = System.Web.HttpContext.Current.Session[SessionUserKey];
                    IUserBLL userBLL = new UserBLL();
                    SessionUser admin = new SessionUser();
                    if (user == null)
                    {
                        UserModel userModel = userBLL.Login(Int32.Parse(System.Web.HttpContext.Current.User.Identity.Name));
                        foreach (RoleModel role in userModel.Roles)
                        {
                            admin.Roles.Add(role);
                            foreach (MenuModel menu in role.Menus)
                            {
                                if (menu.Status == 0)
                                    admin.MenuPermission.Add(menu);
                            }
                        }
                        admin.MenuPermission = admin.MenuPermission.OrderBy(a => a.TreeCode).OrderBy(a => a.Sort).ToList();
                        admin.UserInfo = userModel;
                        System.Web.HttpContext.Current.Session.Add(SessionUserKey, admin);
                        return admin;
                    }
                    else
                    {
                        admin = (SessionUser)user;
                        System.Web.HttpContext.Current.Session.Add(SessionUserKey, admin);
                    }

                    return admin;
                }
            }
        }

        protected ContentResult JsonP(string callback, object data)
        {
            JsonPResult result = new JsonPResult(callback, data);
            return result;
        }

        /// <summary>
        /// 重写，Json方法，使之返回JsonNetResult类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType,
        Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected string GetAddOrUpdateMessage(string operate)
        {
            string msg = "";
            switch (operate)
            {
                case "add": msg = "添加"; break;
                case "update": msg = "修改"; break;
                case "delete": msg = "删除"; break;
                case "modifypassword": msg = "修改密码"; break;
                default: msg = "未知操作"; break;
            }
            return msg;
        }

        protected string GetSuccessOrFailureMessage(int errorCode, string errorMsg)
        {
            return errorCode == 0 ? "成功。" : "失败(" + errorMsg + ")。";
        }

        /// <summary>
        /// 根据控制器和方法名获取下面操作权限
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected List<string> GetActionMenu(string controllerName, string actionName)
        {
            IList<MenuModel> menuList = CurrentAdmin.MenuPermission;
            MenuModel model = menuList.Where(a => a.ControllerName == controllerName && a.ActionName == actionName).FirstOrDefault();
            //IList<MenuModel> operateMenu = new List<MenuModel>();
            List<string> operateMenu = new List<string>();
            if (model != null)
            {
                IList<MenuModel> operateMenuList = menuList.Where(a => a.ParentId == model.Id && a.Type == 1).ToList();
                foreach (MenuModel menu in menuList)
                {
                    if (menu.ParentId == model.Id)
                    {
                        operateMenu.Add(menu.operate);
                    }
                }
            }
            return operateMenu;
        }

        protected string CurrentTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        



        //protected WorkFlowModel GetWorkFlow()
        //{
        //    WorkFlowModel model = (WorkFlowModel)System.Web.HttpContext.Current.Session["CurrentWorkFlow"];
        //    if (model == null)
        //    {
        //        model = new Jewelry.EntityFramework.BLL.BLL.GenericBLL().Find<WorkFlowModel>(1);
        //        System.Web.HttpContext.Current.Session.Add("CurrentWorkFlow", model);
        //    }
        //    return model;
        //}

        
    }
}