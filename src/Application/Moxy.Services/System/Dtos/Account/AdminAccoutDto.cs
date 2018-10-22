using Moxy.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Moxy.Services.System.Dtos
{
    public class AdminAccoutInputDto
    {
        [Required(ErrorMessage = "请输入用户名")]
        public string AdminName { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string AdminPwd { get; set; }
        public string AdminSecurityPwd
        {
            get
            {
                return SecurityHelper.EncryptDES(AdminPwd);
            }
        }
    }
    public class AdminAccoutOutputDto
    {
        public string AdminName { get; set; }
        public string AdminKey { get; set; }
        public string AdminToken
        {
            get
            {
                return SecurityHelper.EncryptDES(AdminName + AdminKey);
            }
        }
    }
}
