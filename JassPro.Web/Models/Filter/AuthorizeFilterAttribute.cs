using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JassPro.Web.Models;
using JassPro.Web.Models.Attr;

namespace JassPro.Web.Models.Filter
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            object[] loginIgnore = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizeIgnoreAttribute), true);
            string currentUrl = filterContext.HttpContext.Request.Url.ToString();
            if (loginIgnore.Length != 1)
            {
                if (filterContext.HttpContext == null)
                {
                    throw new ArgumentNullException("httpContext");
                }

                object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ViewUIAttribute), true);
                var isViewPage = attrs.Length == 1;//当前Action请求是否为具体的功能页

                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (isViewPage)
                    {
                        filterContext.Result = new HttpUnauthorizedResult();//直接URL输入的页面地址跳转到登陆页
                    }
                    else
                    {
                        var excResult = new JsonResult();
                        excResult.Data = new VResult
                        {
                            success = false,
                            msg = "当前会话(Session)已过期，请重新登录！",
                            error_code = -2
                        };

                        filterContext.Result = excResult;
                    }
                    return;
                }

                if (this.AuthorizeCore(filterContext, isViewPage) == false)//根据验证判断进行处理
                {
                    //注：如果未登录直接在URL输入功能权限地址提示不是很友好；如果登录后输入未维护的功能权限地址，那么也可以访问，这个可能会有安全问题
                    if (isViewPage == true)
                    {
                        filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Error", action = "NoAccess" }));
                    }
                    else
                    {
                        // 如果该Action属于ExtResult，返回json错误信息
                        if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(VJsonResultAttribute), true).Length == 1)
                        {

                            string actionName = filterContext.RouteData.Values["action"].ToString();

                            var excResult = new JsonResult();
                            excResult.Data = new VResult
                            {
                                success = false,
                                msg = "抱歉，您不具有当前操作的权限！",
                                error_code = 0
                            };

                            filterContext.Result = excResult;
                        }
                        else
                        {
                            filterContext.Result = new ContentResult { Content = "抱歉，您不具有当前操作的权限！" };//功能权限弹出提示框
                        }
                    }
                }
                /*
                if (isViewPage)
                {
                    string strDescription = String.Empty;
                    object[] descattrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                    if (descattrs.Length > 0)
                        strDescription = (descattrs[0] as System.ComponentModel.DescriptionAttribute).Description;

                    //ILogger logger = LogFactory.Logger();
                    //Log.Info(String.Format("访问页面：{0} ", path) + strDescription);
                }*/
            }
            else if (!currentUrl.Contains("/Main/Login") && !currentUrl.Contains("/Main/Logout") )
            {
                object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ViewUIAttribute), true);
                var isViewPage = attrs.Length == 1;//当前Action请求是否为具体的功能页

                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (isViewPage)
                    {
                        filterContext.Result = new HttpUnauthorizedResult();//直接URL输入的页面地址跳转到登陆页
                    }
                    else if (!currentUrl.Contains("/Main/UserLogin") && !currentUrl.Contains("/Main/UserAjaxLogin"))
                    {
                        var excResult = new JsonResult();
                        excResult.Data = new VResult
                        {
                            success = false,
                            msg = "当前会话(Session)已过期，请重新登录！",
                            error_code = -2
                        };

                        filterContext.Result = excResult;
                    }
                    return;
                }
            }
        }

        //权限判断业务逻辑
        protected virtual bool AuthorizeCore(ActionExecutingContext filterContext, bool isViewPage)
        {
            var user = (SessionUser)System.Web.HttpContext.Current.Session["CurrentAdmin"]; ;//获取当前用户信息
            if (user == null)
                user = new JassPro.Web.Controllers.BaseController().CurrentAdmin;//获取当前用户信
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            if (user.MenuPermission.Count(m => m.ControllerName == controllerName && m.ActionName == actionName) == 0)
                return false;
            return true;
            /*if (isViewPage && (controllerName.ToLower() != "main" && actionName.ToLower() != "masterpage"))//如果当前Action请求为具体的功能页并且不是MasterPage页
            {
                if (user.MenuPermission.Count(m => m.ControllerName == controllerName && m.ActionName == actionName) == 0)
                    return false;
            }
            else
            {

            }
            return true;*/
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeIgnoreAttribute : Attribute
    {

    }
}