using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JassPro.DAL.Extension;
using JassPro.Model;

namespace JassPro.DAL
{
    public class GenericDAL
    {
        static Object locker = new Object();
        //private static Random random = new Random();
        #region 扩展基本方法

        public T Find<T>(params object[] keyValues) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                return dbContext.Set<T>().Find(keyValues);
            }
        }

        public T Add<T>(T entity) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                dbContext.Set<T>().Attach(entity);
                dbContext.Entry<T>(entity).State = EntityState.Added;
                dbContext.SaveChanges();
                return entity;
            }
        }
        public IList<T> Add<T>(IList<T> entityList) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                foreach (T entity in entityList)
                {
                    dbContext.Set<T>().Attach(entity);
                    dbContext.Entry<T>(entity).State = EntityState.Added;
                }
                dbContext.SaveChanges();
                return entityList;
            }
        }

        public bool Update<T>(T entity) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                dbContext.Set<T>().Attach(entity);
                dbContext.Entry<T>(entity).State = EntityState.Modified;
                int result = dbContext.SaveChanges();
                return result > 0;
            }
        }

        public bool Update<T>(IList<T> entityList) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                foreach (var entity in entityList)
                {
                    dbContext.Set<T>().Attach(entity);
                    dbContext.Entry<T>(entity).State = EntityState.Modified;
                }
                int result = dbContext.SaveChanges();
                return result > 0;
            }
        }

        public bool Delete<T>(T entity) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                dbContext.Set<T>().Attach(entity);
                dbContext.Entry<T>(entity).State = EntityState.Deleted;
                int result = dbContext.SaveChanges();
                return result > 0;
            }
        }

        public bool Delete<T>(IList<T> entityList) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                foreach (T entity in entityList)
                {
                    dbContext.Set<T>().Attach(entity);
                    dbContext.Entry<T>(entity).State = EntityState.Deleted;
                }
                int result = dbContext.SaveChanges();
                return result > 0;
            }
        }

        public IList<T> FindAllWithoutPaging<T>(Expression<Func<T, bool>> conditions = null) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                IList<T> entityList = null;
                if (conditions == null)
                {
                    entityList = dbContext.Set<T>().ToList();
                }
                else
                    entityList = dbContext.Set<T>().Where(conditions).ToList<T>();
                return entityList;
            }
        }

        public IList<T> FindAll<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex, out long total) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                IQueryable<T> source = conditions == null ? dbContext.Set<T>() : dbContext.Set<T>().Where(conditions);
                total = source == null ? 0 : source.Count();
                return source.OrderByDescending<T, S>(orderBy).ToPagedList(pageIndex, pageSize).ToList<T>();
            }
        }

        public void InsertLog<T>(T log) where T : BaseModel
        {
            Add(log);
        }

        


        #endregion
    }
}
