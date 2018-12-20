using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JassPro.DAL.Extension;
using JassPro.Model;
using JassPro.Model.Account;

namespace JassPro.DAL
{
    public class HContext : BaseContext, IDisposable
    {
        #region 实体模型
        #region 基本信息
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<UserModel> Users { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<RoleModel> Roles { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<MenuModel> Menus { get; set; }
        #endregion


        #region 实体映射关系
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<HContext>(null);

            #region 基本信息
            modelBuilder.Entity<UserModel>()   //用户->权限
                .HasMany(a => a.Roles)
                .WithMany(b => b.Users)
                .Map(m =>
                {
                    m.MapLeftKey("user_id").MapRightKey("role_id").ToTable("system_user_role");
                });
            modelBuilder.Entity<RoleModel>()   //权限->菜单
                .HasMany(a => a.Menus)
                .WithMany(b => b.Roles)
                .Map(m =>
                {
                    m.MapLeftKey("role_id").MapRightKey("menu_id").ToTable("system_role_menu");
                });
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region 扩展基本方法

        public T Find<T>(params object[] keyValues) where T : BaseModel
        {
            return this.Set<T>().Find(keyValues);
        }

        public T Add<T>(T entity) where T : BaseModel
        {
            this.Set<T>().Attach(entity);
            this.Entry<T>(entity).State = EntityState.Added;
            entity.ErrorCode = this.SaveChanges();
            if (entity.ErrorCode < 0)
            {
                entity.ErrorMsg = "添加失败。";
            }
            return entity;
        }
        public IList<T> Add<T>(IList<T> entityList) where T : BaseModel
        {
            foreach (T entity in entityList)
            {
                this.Set<T>().Attach(entity);
                this.Entry<T>(entity).State = EntityState.Added;
            }
            this.SaveChanges();
            return entityList;
        }

        public bool Update<T>(T entity) where T : BaseModel
        {
            this.Set<T>().Attach(entity);
            this.Entry<T>(entity).State = EntityState.Modified;
            int result = this.SaveChanges();
            return result > 0;
        }

        public bool Delete<T>(T entity) where T : BaseModel
        {
            this.Set<T>().Attach(entity);
            this.Entry<T>(entity).State = EntityState.Deleted;
            int result = this.SaveChanges();
            return result > 0;
        }

        public bool Delete<T>(IList<T> entityList) where T : BaseModel
        {
            foreach (T entity in entityList)
            {
                this.Set<T>().Attach(entity);
                this.Entry<T>(entity).State = EntityState.Deleted;
            }
            int result = this.SaveChanges();
            return result > 0;
        }

        public IList<T> FindAllWithoutPaging<T>(Expression<Func<T, bool>> conditions = null) where T : BaseModel
        {
            IList<T> entityList = null;
            if (conditions == null)
            {
                entityList = this.Set<T>().ToList();
            }
            else
                entityList = this.Set<T>().Where(conditions).ToList<T>();
            return entityList;
        }

        public IList<T> FindAll<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex, out long total) where T : BaseModel
        {
            IQueryable<T> source = conditions == null ? this.Set<T>() : this.Set<T>().Where(conditions);
            total = source == null ? 0 : source.Count();
            return source.OrderByDescending<T, S>(orderBy).ToPagedList(pageIndex, pageSize).ToList<T>();
        }

        public void InsertLog<T>(T log) where T : BaseModel
        {
            Add(log);
        }

        #endregion


        #endregion
    }
}
