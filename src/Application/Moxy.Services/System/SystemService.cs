using Moxy.Data;
using Moxy.Services.System.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Moxy.Services.System
{
    public class SystemService : ISystemService
    {
        /// <summary>
        /// SystemService
        /// </summary>
        private readonly MoxyDbContext _dbContext;
        public SystemService(MoxyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 后台登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OperateResult Login(AdminAccoutInputDto input)
        {
            var existItem = _dbContext.SysAdmin.FirstOrDefault(e => e.AdminName == input.AdminName && e.AdminPwd == input.AdminSecurityPwd);
            if (existItem == null)
            {
                return OperateResult.Error("用户名或密码错误");
            }
            if (!existItem.IsEnable)
            {
                return OperateResult.Error("账号未启用");
            }
            var output = new AdminAccoutOutputDto()
            {
                AdminKey = existItem.AdminKey,
                AdminName = existItem.AdminName
            };
            return OperateResult.Succeed("登录成功", output);
        }
    }
}
