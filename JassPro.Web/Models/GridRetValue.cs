using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JassPro.Web.Models
{
    public class GridRetValue<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount
        {
            get
            {
                if (PageSize <= 0)
                    return 1;
                return int.Parse(Math.Ceiling(((decimal)Total / (decimal)PageSize)).ToString());
            }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> Data { get; set; }

        /// <summary>
        /// 合计输出对象
        /// </summary>
        public object OutParam { get; set; }
    }
}