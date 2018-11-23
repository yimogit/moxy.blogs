using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Moxy.Services.System.Dtos
{
    public class SysAdminUpdatePwdInputDto
    {
        public string AdminName { get; set; }
        [Required(ErrorMessage = "新密码不能为空")]
        public string NewPassword { get; set; }
    }
}
