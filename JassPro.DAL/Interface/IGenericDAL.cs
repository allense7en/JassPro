using JassPro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JassPro.DAL
{
    public interface IGenericDAL
    {
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T Find<T>(params object[] keyValues) where T : BaseModel;

        /// <summary>
        /// 保存单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add<T>(T entity) where T : BaseModel;

        /// <summary>
        /// 保存多个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        /// <returns></returns>
        IList<T> Add<T>(IList<T> entityList) where T : BaseModel;

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update<T>(T entity) where T : BaseModel;

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update<T>(IList<T> entityList) where T : BaseModel;

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete<T>(T entity) where T : BaseModel;

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        /// <returns></returns>
        bool Delete<T>(IList<T> entityList) where T : BaseModel;

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="total"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IList<T> FindAllWithoutPaging<T>(Expression<Func<T, bool>> conditions = null) where T : BaseModel;

        /// <summary>
        /// 分页查询所有记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IList<T> FindAll<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex, out long total) where T : BaseModel;

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="log"></param>
        void InsertLog<T>(T log) where T : BaseModel;

        
    }
}
