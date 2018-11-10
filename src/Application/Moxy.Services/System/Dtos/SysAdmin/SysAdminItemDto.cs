using AutoMapper.Attributes;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.System.Dtos
{
    [MapsFrom(typeof(SysAdmin), ReverseMap = true)]
    public class SysAdminItemDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 管理员账号
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        public string ModuleCodes { get; set; }
    }
}
