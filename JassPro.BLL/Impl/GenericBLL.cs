using JassPro.DAL;
using JassPro.Model;
using JassPro.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassPro.BLL
{
    public class GenericBLL : IGenericBLL
    {
        private GenericDAL _GenericDAL;

        public GenericBLL()
        {
            _GenericDAL = new GenericDAL();
        }

        public T Find<T>(int id) where T : BaseModel
        {
            return _GenericDAL.Find<T>(id);
        }

        public T Add<T>(T entity) where T : BaseModel
        {
            return _GenericDAL.Add<T>(entity);
        }

        public IList<T> Add<T>(IList<T> entityList) where T : BaseModel
        {
            return _GenericDAL.Add<T>(entityList);
        }

        public bool Update<T>(T entity) where T : BaseModel
        {
            return _GenericDAL.Update<T>(entity);
        }

        public bool Update<T>(IList<T> entityList) where T : BaseModel
        {
            return _GenericDAL.Update<T>(entityList);
        }

        public bool Delete<T>(int id) where T : BaseModel
        {
            T entity = _GenericDAL.Find<T>(id);
            return _GenericDAL.Delete<T>(entity);
        }

        public bool Delete<T>(IList<T> entityList) where T : BaseModel
        {
            return _GenericDAL.Delete<T>(entityList);
        }

        /*public bool Delete<T>(IList<int> ids) where T : BaseModel
        {
            IList<T> entityList = new List<T>();
            foreach (int id in ids)
            {
                T entity = Activator.CreateInstance<T>();
                entity.Id = id;
                entityList.Add(entity);
            }
            return _GenericDAL.Delete<T>(entityList);
        }*/

        public IList<T> FindAllWithoutPaging<T>(System.Linq.Expressions.Expression<Func<T, bool>> conditions = null) where T : BaseModel
        {
            return _GenericDAL.FindAllWithoutPaging<T>(conditions);
        }
        /*
        public IList<T> FindAll<T, S>(System.Linq.Expressions.Expression<Func<T, bool>> conditions, System.Linq.Expressions.Expression<Func<T, S>> orderBy, int pageSize, int pageIndex, out long total) where T : BaseModel
        {
            using (HContext dbContext = new HContext())
            {
                return dbContext.FindAll<T, S>(conditions, orderBy, pageSize, pageIndex, out total);
            }
        }*/

        public void InsertOperateLog(OperateLogModel log)
        {
            log.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _GenericDAL.InsertLog(log);
        }

        
    }
}
