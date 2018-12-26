using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassPro.Model.Log
{
    [Serializable]
    [Table("system_operate_log")]
    public class OperateLogModel : BaseModel
    {
        
        [Column("store_id")]
        /// <summary>
        /// 店铺id
        /// </summary>
        public int StoreId { get; set; }

        [Column("user_id")]
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        [Column("user_name")]
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        [Column("description")]
        /// <summary>
        /// 操作内容
        /// </summary>
        public string Description { get; set; }

        [Column("extra")]
        /// <summary>
        /// 额外参数(json)
        /// </summary>
        public string Extra { get; set; }

        [Column("add_time")]
        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { get; set; }
    }

    public class OperateLogRequest : BaseQueryModel
    {        
        public int StoreId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
