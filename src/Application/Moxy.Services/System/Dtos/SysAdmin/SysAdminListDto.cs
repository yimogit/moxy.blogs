using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.System.Dtos
{
    public class SysAdminListDto
    {
        public SysAdminListDto() { }
        public SysAdminListDto(SysAdmin sysAdmin)
        {
            this.AdminName = sysAdmin.AdminName;
            this.AdminKey = sysAdmin.AdminKey;
            this.IsEnable = sysAdmin.IsEnable;
        }
        /// <summary>
        /// 管理员账号
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 管理员Key
        /// </summary>
        public string AdminKey { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
