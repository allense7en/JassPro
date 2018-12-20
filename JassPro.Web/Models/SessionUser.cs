using JassPro.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JassPro.Web.Models
{
    public class SessionUser
    {
        public SessionUser()
        {
            this.Roles = new List<RoleModel>();
            this.MenuPermission = new List<MenuModel>();
        }
        //用户信息
        public UserModel UserInfo { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public IList<RoleModel> Roles { get; set; }

        //菜单权限
        public IList<MenuModel> MenuPermission { get; set; }

        
    }
}