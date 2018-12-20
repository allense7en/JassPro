using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace JassPro.DAL.Extension
{
    public static class PageLinqExtention
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            int count = (pageIndex - 1) * pageSize;
            List<T> items = allItems.Skip<T>(count).Take<T>(pageSize).ToList<T>();
            return new PagedList<T>(items, pageIndex, pageSize);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool ascending) where T : class
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
                //throw new ArgumentException("propertyName", "Not Exist");
                //property = type.GetProperty("Id");
                return source;

            ParameterExpression param = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        /// <summary>
        /// 扩展Between 操作符
        /// 使用 var query = db.People.Between(person => person.Age, 18, 21);
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static IQueryable<TSource> Between<TSource, TKey>
            (this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector,
            TKey low, TKey high) where TKey : IComparable<TKey>
        {
            Expression key = Expression.Invoke(keySelector,
                    keySelector.Parameters.ToArray());
            Expression lowerBound = Expression.GreaterThanOrEqual
                (key, Expression.Constant(low));
            Expression upperBound = Expression.LessThanOrEqual
                (key, Expression.Constant(high));
            Expression and = Expression.AndAlso(lowerBound, upperBound);
            Expression<Func<TSource, bool>> lambda =
                Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return source.Where(lambda);
        }
    }
}
