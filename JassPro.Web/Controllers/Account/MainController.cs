﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JassPro.Web.Models;
using JassPro.Web.Models.Attr;
using JassPro.BLL.Impl.Account;
using JassPro.BLL.Interface.Account;
using JassPro.Model.Account;
using JassPro.Model.Log;
using JassPro.Utility.Util;
using JassPro.Web.Models.Filter;
using JassPro.Web.Models.Helper;

namespace JassPro.Web.Controllers.Account
{
    public class MainController : BaseController
    {
        private readonly IUserBLL _UserBLL;

        public MainController()
        {
            _UserBLL = new UserBLL();
        }


        [Description("登陆页面")]
        [ViewUI]
        [AuthorizeIgnore]
        public ActionResult Login(UserModel model)
        {
            if (!model.IsError) model.ErrorMsg = "";
            return View(model);
        }





        [Description("主页")]
        [ViewUI]
        [AuthorizeIgnore]
        public ActionResult Index()
        {
            return View();
        }



        [Description("主页")]
        [ViewUI]
        [AuthorizeIgnore]
        public ActionResult PhoneIndex()
        {
            IList<MenuModel> menuList = CurrentAdmin.MenuPermission;
            ViewBag.UserId = CurrentAdmin.UserInfo.Id;
            ViewBag.RealName = CurrentAdmin.UserInfo.RealName;
            ViewBag.StoreId = CurrentAdmin.UserInfo.StoreId;
            ViewBag.StoreName = CurrentAdmin.UserInfo.StoreName;
            return View(menuList);
        }

        [Description("主页")]
        [ViewUI]
        [AuthorizeIgnore]
        public ActionResult PcIndex()
        {
            IList<MenuModel> menuList = CurrentAdmin.MenuPermission;
            ViewBag.UserId = CurrentAdmin.UserInfo.Id;
            ViewBag.RealName = CurrentAdmin.UserInfo.RealName;
            ViewBag.StoreId = CurrentAdmin.UserInfo.StoreId;
            ViewBag.StoreName = CurrentAdmin.UserInfo.StoreName;
            ViewBag.Cover = CurrentAdmin.UserInfo.Cover;
            ViewBag.RoleName = CurrentAdmin.UserInfo.Roles.FirstOrDefault().Name;
            return View(menuList);
        }



        [Description("主页内容")]
        [ViewUI]
        [AuthorizeIgnore]
        public ActionResult IndexContext()
        {
            return View();
        }


        [Description("头像上传")]
        [ViewUI]
        [AuthorizeIgnore]
        public ActionResult UploadCover()
        {
            return View();
        }


        [Description("用户登陆")]
        [VJsonResult]
        [HttpPost]
        [AuthorizeIgnore]
        public ActionResult UserLogin()
        {
            UserModel user = new UserModel();
            this.TryUpdateModel<UserModel>(user);
            if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
            {
                user.Password = Utils.ToMd5(user.Password);
            }
            else
            {
                user.ErrorCode = -2;
                user.ErrorMsg = "用户名或密码不能为空!";
                return View("Login", user);
                //return Json(new VResult() { success = false, msg = "用户名或密码不能为空!" });
            }
            string uName = user.UserName;
            user = _UserBLL.Login(user);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), false); // 加入form验证票据
                SessionUser loginUser = new SessionUser();
                foreach (RoleModel role in user.Roles)
                {
                    loginUser.Roles.Add(role);
                    foreach (MenuModel menu in role.Menus)
                    {
                        loginUser.MenuPermission.Add(menu);
                    }
                }
                loginUser.MenuPermission = loginUser.MenuPermission.OrderBy(a => a.TreeCode).OrderBy(a => a.Sort).ToList();
                loginUser.UserInfo = user;
                System.Web.HttpContext.Current.Session.Add(SessionUserKey, loginUser);
                {//操作日志
                    OperateLogModel log = new OperateLogModel()
                    {
                        StoreId = user.StoreId,
                        UserId = user.Id,
                        UserName = user.RealName,
                        Extra = "[{\"UserId\":" + user.Id + "}]"
                    };
                    log.Description = "用户【" + user.RealName + "】从[" + UIHelper.IP + "]登陆。";
                    _UserBLL.InsertOperateLog(log);
                }
                string url = Request.UrlReferrer.ToString();
                string[] rU = url.Split('?');
                if (rU.Length == 2)
                {
                    string returnUrl = HttpUtility.UrlDecode(rU[1].Replace("ReturnUrl=", ""));
                    if (!rU.Contains("/Main/Login"))
                        return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Main");
            }
            //return Json(new VResult() { success = user != null });
            //return View("Login");
            user = new UserModel() { ErrorCode = -2, ErrorMsg = "用户名或密码不正确。", UserName = uName };
            return View("Login", user);
        }

        [Description("用户Ajax登陆")]
        [VJsonResult]
        [HttpPost]
        [AuthorizeIgnore]
        public ActionResult UserAjaxLogin()
        {
            //Stream file = Request.InputStream;
            //StreamReader sr = new StreamReader(file, Encoding.UTF8);
            //string restOfStream = sr.ReadToEnd();
            //sr.Close();
            //StreamWriter sw = new StreamWriter(Server.MapPath("/upload/GoodsStorageReport/login.txt"), true, Encoding.UTF8);
            //string w = restOfStream;
            //sw.Write(w);
            //sw.Close();

            UserModel user = new UserModel();
            this.TryUpdateModel<UserModel>(user);
            if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
            {
                user.Password = Utils.ToMd5(user.Password);
            }
            else
            {
                user.ErrorCode = -2;
                user.ErrorMsg = "用户名或密码不能为空!";
                return Json(new VResult() { success = false, msg = "用户名或密码不能为空" });
                //return Json(new VResult() { success = false, msg = "用户名或密码不能为空!" });
            }
            string uName = user.UserName;
            user = _UserBLL.AjaxLogin(user);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), false); // 加入form验证票据
                SessionUser loginUser = new SessionUser();
                foreach (RoleModel role in user.Roles)
                {
                    loginUser.Roles.Add(role);
                    foreach (MenuModel menu in role.Menus)
                    {
                        if (menu.Status == 0)
                            loginUser.MenuPermission.Add(menu);
                    }
                }
                loginUser.MenuPermission = loginUser.MenuPermission.OrderBy(a => a.TreeCode).OrderBy(a => a.Sort).ToList();
                loginUser.UserInfo = user;
                System.Web.HttpContext.Current.Session.Add(SessionUserKey, loginUser);
                {//操作日志
                    OperateLogModel log = new OperateLogModel()
                    {
                        StoreId = user.StoreId,
                        UserId = user.Id,
                        UserName = user.RealName,
                        Extra = "[{\"UserId\":" + user.Id + "}]"
                    };
                    log.Description = "用户【" + user.RealName + "】从[IPAD]登陆。";
                    _UserBLL.InsertOperateLog(log);
                }
                return Json(new { success = true, user = user });
            }
            return Json(new VResult() { success = false, msg = "用户名或密码不正确" });
        }

        [VJsonResult]
        [Description("用户注销")]
        [AuthorizeIgnore]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            System.Web.HttpContext.Current.Session.Remove(SessionUserKey);

            return RedirectToAction("Login");
        }

        [Description("员工头像上传")]
        [VJsonResult]
        [AuthorizeIgnore]
        public ActionResult UploadUserImage()
        {
            HttpPostedFileBase image = Request.Files[0];
            //保存路径
            string folder = "/upload/userimg/",
                //服务器路径
                absFolder = Server.MapPath(folder),
                extends = image.FileName.Substring(image.FileName.LastIndexOf('.') + 1),
                fileName = Guid.NewGuid().ToString() + "." + extends;

            //ImageHelper iHelper = new ImageHelper();
            //iHelper.CreateSmallPhoto(image.FileName, 90, 90, "/upload/SmallgoodsStyleImage/");
            if (!System.IO.Directory.Exists(absFolder))
            {
                System.IO.Directory.CreateDirectory(absFolder);
            }

            image.SaveAs(absFolder + fileName);
            System.Drawing.Bitmap objPic, objNewPic;

            try
            {
                objPic = new System.Drawing.Bitmap(absFolder + fileName);
                objNewPic = new System.Drawing.Bitmap(objPic, 480, 480);
                string sThumbFile = "thumb_" + System.IO.Path.GetFileNameWithoutExtension(fileName);
                string smallImage = System.Web.HttpContext.Current.Server.MapPath(folder) + sThumbFile;
                objNewPic.Save(smallImage + ".jpg");
            }
            catch (Exception exp)
            {

                throw exp;
            }
            finally
            {
                objPic = null;
                objNewPic = null;
            }

            //string extendsuo = smallImagePath.Substring(smallImagePath.LastIndexOf('.') + 1);
            //string suofileName = Guid.NewGuid().ToString() + "." + extendsuo;
            //HttpPostedFileBase imagesuo = new HttpPostedFile();
            //imagesuo.SaveAs(folder + suofileName);            
            return Json(new VResult() { success = true, msg = folder + fileName });
        }
    }
}