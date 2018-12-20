using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JassPro.Model.Account;

namespace JassPro.BLL.Interface.Account
{
    public interface IUserBLL : IGenericBLL
    {
        UserModel Login(UserModel user);

        UserModel AjaxLogin(UserModel user);

        UserModel Login(int userId);

        UserModel SaveUser(UserModel user);

        UserModel ModifyPassword(UserModel user);

        IList<UserModel> GetPagingList(UserRequest request = null);
        IList<UserModel> GetStaffList(UserRequest request = null);

        UserModel GetUserRoleByUserId(int userId);

        /// <summary>
        /// 获取用户和用户角色、菜单(快速登陆)
        /// </summary>
        /// <returns></returns>
        IList<UserModel> GetUserLoginList();

        bool DeleteUser(IList<UserModel> users);

        
        //bool ClearMemberCached();

    }
}
