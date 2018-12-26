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
    [Table("system_role")]
    public class RoleModel
    {
        public RoleModel()
        {
            this.Menus = new List<MenuModel>();
        }


        //[DataMember(Order = 1)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        //[DataMember(Order = 2)]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        //[DataMember(Order = 3)]
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        //[DataMember(Order = 4)]
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// 状态(0:正常,1:停用)
        /// </summary>
        //[DataMember(Order = 5)]
        [Column("status")]
        public int Status { get; set; }

        [JsonIgnore]
        [NotMapped]
        public IList<int> MenuIds { get; set; }

        [JsonIgnore]
        public IList<UserModel> Users { get; set; }

        //[DataMember(Order = 6)]
        public IList<MenuModel> Menus { get; set; }
    }
}
