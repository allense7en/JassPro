using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JassPro.BLL;
using JassPro.BLL.Interface.Account;
using JassPro.DAL;
using JassPro.DAL.Impl.Account;
using JassPro.DAL.Interface.Account;
using JassPro.Model.Account;
using JassPro.Utility.Util;

namespace JassPro.BLL.Impl.Account
{
    public class UserBLL : GenericBLL, IUserBLL
    {
        private IUserDAL _UserDAL;
        private GenericDAL _GenericDAL;

        public UserBLL()
        {
            _UserDAL = new UserDAL();
            _GenericDAL = new GenericDAL();
        }

        public UserModel Login(UserModel user)
        {
            IList<UserModel> userList = null;// MemCache.RetrieveObject<IList<UserModel>>(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key);
            if (userList == null)
            {
                userList = _UserDAL.GetUserLoginList();
                //MemCache.AddObject(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key, userList);
            }
            user = userList.Where(a => a.UserName == user.UserName && a.Password == user.Password && a.Status == 0).FirstOrDefault();
            return user;
        }

        public UserModel AjaxLogin(UserModel user)
        {
            IList<UserModel> userList = null;// MemCache.RetrieveObject<IList<UserModel>>(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key);
            if (userList == null)
            {
                userList = _UserDAL.GetUserLoginList();
                //MemCache.AddObject(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key, userList);
            }
            user = userList.Where(a => a.Phone == user.UserName && a.Password == user.Password && a.Status == 0).FirstOrDefault();
            return user;
        }

        public UserModel Login(int userId)
        {
            //UserModel user = _UserDAL.Login(userId);
            IList<UserModel> userList = null;// MemCache.RetrieveObject<IList<UserModel>>(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key);
            if (userList == null)
            {
                userList = _UserDAL.GetUserLoginList();
                //MemCache.AddObject(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key, userList);
            }
            UserModel user = userList.Where(a => a.Id == userId).FirstOrDefault();
            return user;
        }

        public UserModel SaveUser(UserModel user)
        {
            if (user.Id > 0)
            {
                UserModel model = _UserDAL.Find<UserModel>(user.Id);
                user.Password = model.Password;
            }
            else if (user != null && !string.IsNullOrEmpty(user.Password))
            {
                user.Password = Utils.ToMd5(user.Password);
            }
            user = _UserDAL.SaveUser(user);
            //if (!user.IsError)
            //{
            //    MemCache.RemoveObject(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key);
            //}
            return user;
        }

        public UserModel ModifyPassword(UserModel user)
        {
            if (user != null && !string.IsNullOrEmpty(user.Password))
            {
                user.Password = Utils.ToMd5(user.Password);
            }
            user = _UserDAL.ModifyPassword(user);
            //if (!user.IsError)
            //{
            //    MemCache.RemoveObject(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key);
            //}
            return user;
        }

        public IList<UserModel> GetPagingList(UserRequest request = null)
        {
            request = request ?? new UserRequest();
            return _UserDAL.GetPagingList(request);
        }

        public IList<UserModel> GetStaffList(UserRequest request = null)
        {
            request = request ?? new UserRequest();
            return _UserDAL.GetStaffList(request);
        }

        public UserModel GetUserRoleByUserId(int userId)
        {
            if (userId > 0)
                return _UserDAL.GetUserRoleByUserId(userId);
            return null;
        }

        public IList<UserModel> GetUserLoginList()
        {
            IList<UserModel> userList = null;// = MemCache.RetrieveObject<IList<UserModel>>(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key);
            if (userList == null)
            {
                userList = _UserDAL.GetUserLoginList();
                //MemCache.AddObject(PCacheVar.Cache_Project_Name, PCacheVar.UserLogin_Key, userList);
            }
            return userList;
        }

        public bool DeleteUser(IList<UserModel> users)
        {
            return _UserDAL.DeleteUser(users);
        }

        

        //public bool ClearMemberCached()
        //{
        //    try
        //    {
        //        //MemClientCache client = new MemClientCache();
        //        //client.FlushAll();
        //        MemCache.FlushAll();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}

    }
}
