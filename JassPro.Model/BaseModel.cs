using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassPro.Model
{
    [Serializable]
    public class BaseModel
    {
        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }

        private int _errorCode = 0;
        private string _errorMsg = "操作成功！";

        [Description("错误代码")]
        [NotMapped]
        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        [Description("错误信息")]
        [NotMapped]
        public string ErrorMsg
        {
            get
            {
                if (IsError && _errorMsg == "操作成功！")
                {
                    return "操作失败！";
                }
                return _errorMsg;
            }
            set
            {
                _errorMsg = value;
            }
        }

        [Description("是否有错")]
        [NotMapped]
        public bool IsError
        {
            get
            {
                return ErrorCode < 0;
            }
        }
    }
}
