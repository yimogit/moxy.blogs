using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Moxy.EntityFramework.Domain
{

    [Table("sys_admin")]
    public partial class SysAdmin
    {
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 管理员账号
        /// </summary>
        [Column("admin_name"), MaxLength(100)]
        public string AdminName { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
        [Column("admin_pwd"), MaxLength(100)]
        public string AdminPwd { get; set; }
        /// <summary>
        /// 管理员Key
        /// </summary>
        [Column("admin_key"), MaxLength(100)]
        public string AdminKey { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("is_enable")]
        public bool IsEnable { get; set; }
    }
}
