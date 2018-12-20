using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JassPro.Model.Account
{
    [Serializable]
    [DataContract]
    [Table("system_user")]
    public class UserModel:BaseModel
    {
        public UserModel()
        {
            this.RoleIds = new List<int>();
            this.Roles = new List<RoleModel>();
        }

        [DataMember(Order = 1)]
        [Column("id")]
        new int Id { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        [DataMember(Order = 2)]
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DataMember(Order = 3)]
        [Column("real_name")]
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember(Order = 4)]
        [Column("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 性别（0:保密；1：男；2：女）
        /// </summary>
        [DataMember(Order = 5)]
        [Column("sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        [DataMember(Order = 6)]
        [Column("dept_id")]
        public int DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember(Order = 7)]
        [Column("dept_name")]
        public string DeptName { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        [DataMember(Order = 8)]
        [Column("store_id")]
        public int StoreId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [DataMember(Order = 9)]
        [Column("store_name")]
        public string StoreName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [DataMember(Order = 10)]
        [Column("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [DataMember(Order = 11)]
        [Column("password")]
        public string Password { get; set; }

        /// <summary>
        /// 是否管理员（0：是；1：否）
        /// </summary>
        [DataMember(Order = 12)]
        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        [DataMember(Order = 13)]
        [Column("entry_date")]
        public string EntryDate { get; set; }

        /// <summary>
        /// 账号状态（0：正常；1：停用）
        /// </summary>
        [DataMember(Order = 14)]
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [DataMember(Order = 15)]
        [Column("add_time")]
        public string AddTime { get; set; }

        /// <summary>
        /// 最后登录ip
        /// </summary>
        [DataMember(Order = 16)]
        [Column("last_login_ip")]
        public string LastLoginIp { get; set; }
        
        /// <summary>
        /// 头像
        /// </summary>
        [DataMember(Order = 17)]
        [Column("cover")]
        public string Cover { get; set; }
                
        [DataMember(Order = 18)]
        [Column("position_id")]
        /// <summary>
        /// 职位ID
        /// </summary>
        public int PositionId { get; set; }

        [DataMember(Order = 19)]
        [Column("position_name")]
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }

        [NotMapped]
        public IList<int> RoleIds { get; set; }

        [DataMember(Order = 20)]
        public IList<RoleModel> Roles { get; set; }
    }

    public class UserRequest : BaseQueryModel
    {

        //Phone Sex DeptId StoreId RealName
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string RealName { get; set; }

        public int RoleId { get; set; }

        public int StoreId { get; set; }
        public int DeptId { get; set; }
        public int Sex { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }       

        public int Status { get; set; }
    }
}
