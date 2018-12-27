using JassPro.BLL.Interface.Account;
using JassPro.Model.Account;
using JassPro.Model.Log;
using JassPro.Web.Models;
using JassPro.Web.Models.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JassPro.Web.Controllers.Basic
{
    public class UserManagerController : BaseController
    {
        private readonly IUserBLL _UserBLL;
        public UserManagerController(IUserBLL userBLL)
        {
            _UserBLL = userBLL;
        }

        
        public ActionResult VUser()
        {
            return View();
        }

        [Description("修改头像")]
        [VJsonResult]
        public ActionResult UpdateUserCover(string ImgSrc)
        {
            UserModel user = CurrentAdmin.UserInfo;
            user.Cover = ImgSrc;
            bool flag = _UserBLL.UpdateUserCover(user);
            {//操作日志
                UserModel u = CurrentAdmin.UserInfo;
                OperateLogModel log = new OperateLogModel()
                {                    
                    StoreId = u.StoreId,
                    UserId = u.Id,
                    UserName = u.RealName,
                    Extra = "{\"UserId\":" + user.Id + "}"
                };
                log.Description = GetAddOrUpdateMessage("修改") + "账户【" + user.RealName + "】头像" + GetSuccessOrFailureMessage(user.ErrorCode, user.ErrorMsg);
                _UserBLL.InsertOperateLog(log);
            }
            return Json(new VResult() { success = !user.IsError, msg = user.ErrorMsg });
        }

    }
}