using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JassPro.Web.Models.Attr
{
    /// <summary>
    /// 表示Action的返回是ExtResult类型的Json
    /// 因为非ExtResult类型的Json,在EXT中未进行处理,所以需要标识出来
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class VJsonResultAttribute : Attribute
    {
        public VJsonResultAttribute()
        {
            this.ContentType = "application/json;charset=utf-8";
        }

        public string ContentType { get; set; }
    }
}