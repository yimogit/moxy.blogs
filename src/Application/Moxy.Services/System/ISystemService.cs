using Moxy.Services.System.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.System
{
    public interface ISystemService
    {
        /// <summary>
        /// 后台登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperateResult Login(AdminAccoutInputDto input);
        OperateResult InitSystem();
    }
}
