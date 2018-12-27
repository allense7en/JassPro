using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JassPro.Model.Account;

namespace JassPro.DAL.Interface.Account
{
    public interface IUserDAL : IGenericDAL
    {
        UserModel Login(UserModel user);

        UserModel Login(int userId);

        UserModel SaveUser(UserModel user);

        UserModel ModifyPassword(UserModel user);

        IList<UserModel> GetPagingList(UserRequest request);
        IList<UserModel> GetStaffList(UserRequest request);

        UserModel GetUserRoleByUserId(int userId);

        /// <summary>
        /// 获取用户和用户角色、菜单(快速登陆)
        /// </summary>
        /// <returns></returns>
        IList<UserModel> GetUserLoginList();

        bool DeleteUser(IList<UserModel> users);

        bool UpdateUserCover(UserModel user);

    }
}
