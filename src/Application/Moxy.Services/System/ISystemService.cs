using Moxy.Services.System.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.System
{
    public interface ISystemService
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperateResult Login(AdminAccoutInputDto input);
        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <param name="adminName"></param>
        /// <returns></returns>
        OperateResult InitSystem(string adminName = "admin");
    }
}
