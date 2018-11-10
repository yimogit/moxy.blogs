using AutoMapper.Attributes;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Moxy.Services.System.Dtos
{
    [MapsFrom(typeof(SysAdmin), ReverseMap = true)]
    public class SysAdminInputDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 管理员账号
        /// </summary>
        [Required(ErrorMessage = "管理员账号不能为空")]
        public string AdminName { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
        [Required(ErrorMessage = "管理员密码不能为空")]
        public string AdminPwd { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCodes { get; set; }
    }
}
