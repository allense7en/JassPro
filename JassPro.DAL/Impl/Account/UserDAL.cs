using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JassPro.DAL.Interface.Account;
using JassPro.Model.Account;
using JassPro.DAL.Extension;

namespace JassPro.DAL.Impl.Account
{
    public class UserDAL : GenericDAL, IUserDAL
    {
        public UserModel Login(UserModel user)
        {
            using (HContext dbContext = new HContext())
            {
                user = dbContext.Users.Include("Roles").Include("Roles.Menus")
                    .Where(a => a.UserName == user.UserName && a.Password == user.Password && a.Status == 0)
                    .FirstOrDefault();
                return user;
            }
        }

        public UserModel Login(int userId)
        {
            using (HContext dbContext = new HContext())
            {
                UserModel user = dbContext.Users.Include("Roles").Include("Roles.Menus")
                    .Where(a => a.Id == userId && a.Status == 0).FirstOrDefault();
                return user;
            }
        }

        public UserModel SaveUser(UserModel user)
        {
            try
            {
                using (HContext dbContext = new HContext())
                {
                    UserModel exUser = null;
                    if (user.Id > 0)
                    {
                        exUser = dbContext.Users.Where(a => a.UserName == user.UserName && a.Id != user.Id).FirstOrDefault();
                    }
                    else
                    {
                        exUser = dbContext.Users.Where(a => a.RealName == user.RealName || a.UserName == user.UserName).FirstOrDefault();
                    }
                    if (exUser != null)
                    {
                        user.ErrorCode = -1;
                        user.ErrorMsg = "用户名已存在,请更换后再保存。";
                        return user;
                    }
                    if (user.Id > 0)
                    {
                        UserModel preUser = dbContext.Users.Include("Roles").Where(a => a.Id == user.Id).First();
                        preUser.PositionId = user.PositionId;
                        preUser.PositionName = user.PositionName;
                        //preUser.Code = user.Code;
                        preUser.UserName = user.UserName;
                        preUser.RealName = user.RealName;
                        preUser.Phone = user.Phone;
                        preUser.Sex = user.Sex;
                        preUser.DeptId = user.DeptId;
                        preUser.DeptName = user.DeptName;
                        preUser.PositionId = user.PositionId;
                        preUser.PositionName = user.PositionName;
                        preUser.StoreId = user.StoreId;
                        preUser.StoreName = user.StoreName;
                        preUser.Status = user.Status;
                        preUser.RoleIds = user.RoleIds;
                        preUser.EntryDate = user.EntryDate;
                        preUser.Cover = user.Cover;
                        UserModel u = dbContext.Users.Include("Roles").Where(a => a.Id == user.Id).First();
                        if (user != null)
                        {
                            IList<RoleModel> roles = new List<RoleModel>();
                            if (user.RoleIds != null && user.RoleIds.Count > 0)
                            {
                                roles = dbContext.Roles.Where(a => user.RoleIds.Contains(a.Id)).ToList();
                            }
                            user.Roles = roles;
                            preUser.Roles = roles;

                            bool flag = dbContext.SaveChanges() > 0;
                        }
                        //dbContext.Update(user);
                    }
                    else
                    {
                        IList<RoleModel> roles = new List<RoleModel>();
                        if (user.RoleIds != null && user.RoleIds.Count > 0)
                        {
                            roles = dbContext.Roles.Where(a => user.RoleIds.Contains(a.Id)).ToList();
                        }
                        user.Roles = roles;


                        Random ran = new Random();
                        int num = ran.Next(1000, 9999);
                        string code = num.ToString();

                        UserModel userM = dbContext.Users.Where(a => a.Code == code).FirstOrDefault();
                        while (userM != null)
                        {
                            num = ran.Next(1000, 9999);
                            code = num.ToString();
                            userM = dbContext.Users.Where(a => a.Code == code).FirstOrDefault();
                        }
                        user.Code = code;
                        dbContext.Add(user);

                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                user.ErrorCode = -1;
                user.ErrorMsg = ex.Message;
                return user;
            }
        }

        public UserModel ModifyPassword(UserModel user)
        {
            try
            {
                using (HContext dbContext = new HContext())
                {
                    if (user.Id > 0)
                    {
                        UserModel model = dbContext.Users.Find(user.Id);
                        if (model != null)
                        {
                            model.Password = user.Password;
                            dbContext.SaveChanges();
                            return user;
                        }
                    }
                    user.ErrorCode = -2;
                    user.ErrorMsg = "没有找到该用户";
                    return user;
                }
            }
            catch (Exception ex)
            {
                user.ErrorCode = -1;
                user.ErrorMsg = ex.Message;
                return user;
            }
        }

        public IList<UserModel> GetPagingList(UserRequest request)
        {
            using (HContext dbContext = new HContext())
            {
                request = request ?? new UserRequest();
                //IQueryable<UserModel> entityList = dbContext.Users.Include("Roles").AsNoTracking().Where(a => a.Status == 0 || a.Status==2);
                IQueryable<UserModel> entityList = dbContext.Users.Include("Roles").AsNoTracking();
                if (!string.IsNullOrEmpty(request.UserName))
                    entityList = entityList.Where(u => u.UserName.Contains(request.UserName));
                if (!string.IsNullOrEmpty(request.RealName))
                    entityList = entityList.Where(u => u.RealName.Contains(request.RealName));
                if (!string.IsNullOrEmpty(request.Phone))
                    entityList = entityList.Where(u => u.Phone.Contains(request.Phone));
                if (request.StoreId > 0)
                    entityList = entityList.Where(u => u.StoreId == request.StoreId);
                if (request.RoleId > 0)
                    entityList = entityList.Where(u => u.Roles.Select(s => s.Id).Contains(request.RoleId));
                if (request.PositionId > 0)
                    entityList = entityList.Where(a => a.PositionId == request.PositionId);
                if (!string.IsNullOrEmpty(request.PositionName))
                    entityList = entityList.Where(a => a.PositionName == request.PositionName);
                if (request.Status > -1)
                    entityList = entityList.Where(a => a.Status == request.Status);
                request.TotalRecord = entityList.LongCount();
                if (request.PageSize > 0)
                    entityList = entityList.OrderBy(request.Sidx, request.Sord == "asc").Skip(request.StartRecord).Take(request.PageSize);
                return entityList.ToList();
            }
        }

        public IList<UserModel> GetStaffList(UserRequest request)
        {
            using (HContext dbContext = new HContext())
            {
                request = request ?? new UserRequest();
                IQueryable<UserModel> entityList = dbContext.Users.Include("Roles").AsNoTracking().Where(a => a.Status == 0);
                if (request.StoreId > -1)
                    entityList = entityList.Where(u => u.StoreId == request.StoreId);
                request.TotalRecord = entityList.LongCount();
                return entityList.ToList();
            }
        }

        public UserModel GetUserRoleByUserId(int userId)
        {
            using (HContext dbContext = new HContext())
            {
                UserModel user = dbContext.Users.Include("Roles").Where(a => a.Id == userId).FirstOrDefault();
                return user;
            }
        }

        public IList<UserModel> GetUserLoginList()
        {
            using (HContext dbContext = new HContext())
            {
                return dbContext.Users.Include("Roles").Include("Roles.Menus").Where(a => a.Status != 1).ToList();
            }
        }

        public bool DeleteUser(IList<UserModel> users)
        {
            using (HContext dbContext = new HContext())
            {
                List<int> ids = new List<int>();
                foreach (UserModel m in users)
                {
                    ids.Add(m.Id);
                }
                IList<UserModel> models = dbContext.Users.Where(a => ids.Contains(a.Id)).ToList();
                foreach (UserModel m in models)
                {
                    m.Status = 1;
                }
                return dbContext.SaveChanges() > 0;
            }
        }

    }
        
}
