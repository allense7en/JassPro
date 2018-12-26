using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JassPro.Model.Account
{
    [Serializable]
    //[DataContract]
    [Table("system_menu")]
    public class MenuModel
    {
        //[DataMember(Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        //[DataMember(Order = 14)]
        [Column("area_name")]
        public string AreaName { get; set; }

        /// <summary>
        /// Action名称
        /// </summary>
        //[DataMember(Order = 2)]
        [Column("action_name")]
        public string ActionName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        //[DataMember(Order = 3)]
        [Column("controller_name")]
        public string ControllerName { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        //[DataMember(Order = 4)]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        //[DataMember(Order = 5)]
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        //[DataMember(Order = 6)]
        [Column("parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        //[DataMember(Order = 7)]
        [Column("tree_code")]
        public string TreeCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        //[DataMember(Order = 8)]
        [Column("sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        //[DataMember(Order = 9)]
        [Column("depth")]
        public int Depth { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        //[DataMember(Order = 10)]
        [Column("icon_cls")]
        public string IconCls { get; set; }

        /// <summary>
        /// 类型（0:菜单权限,1:功能权限）
        /// </summary>
        //[DataMember(Order = 11)]
        [Column("type")]
        public int Type { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        //[DataMember(Order = 12)]
        [Column("Operate")]
        public string operate { get; set; }

        /// <summary>
        /// 状态(0:正常,1:停用)
        /// </summary>
        //[DataMember(Order = 13)]
        [Column("status")]
        public int Status { get; set; }

        [JsonIgnore]
        public IList<RoleModel> Roles { get; set; }
    }
}
