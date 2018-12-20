using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JassPro.Web.Models;
using JassPro.Web.Models.Attr;

namespace JassPro.Web.Models.Filter
{
    /// <summary>
    /// 拦截Action的异常，输出Json给EXT捕获
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ActionExceptionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(VJsonResultAttribute), true);
                if (attrs.Length == 1)//判断是否属于ExtResult的Action
                {
                    string msgTmp;
                    if (filterContext.Exception is ValidationException)//如果是验证类异常 ，就不出详细异常信息了
                        msgTmp = "<b>验证错误:  </b>{0}";
                    else
                        msgTmp = "<b>异常消息:  </b>{0}</p><b>触发Action:  </b>{1}</p><b>异常类型:  </b>{2}";
                    var excResult = new JsonResult();
                    Exception exception = filterContext.Exception.GetBaseException();
                    string message = exception.Message;
                    int errorcode = 0;
                    List<string> extraInfor = new List<string>();
                    string detailMsg = string.Format(msgTmp,
                                message,
                                filterContext.ActionDescriptor.ActionName,
                                filterContext.Exception.GetBaseException().GetType().ToString());

                    excResult.Data = new VResult
                    {
                        success = false,
                        msg = message,
                        error_code = errorcode
                    };
                    excResult.ContentType = ((VJsonResultAttribute)attrs[0]).ContentType;

                    filterContext.Result = excResult;
                }
                else
                {
                    var excResult = new ContentResult();
                    string detailMessage = "{\"success\": false,\"Msg\": \"" + filterContext.Exception.GetBaseException().Message + "\"}";
                    //string detailMessage = "ExtHelper.ShowError('" + filterContext.Exception.GetBaseException().Message + "')";
                    excResult.Content = detailMessage;
                    filterContext.Result = excResult;
                }
            }

            filterContext.ExceptionHandled = true;
        }
    }
}