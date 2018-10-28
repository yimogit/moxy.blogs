using Moxy.Services.System.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.System
{
    public interface ISystemService
    {
        /// <summary>
        /// 管理员验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OperateResult AuthCheck(AdminAccoutInputDto input);
        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <param name="adminName"></param>
        /// <returns></returns>
        OperateResult InitSystem(string adminName);
        /// <summary>
        /// 获取账号模块编码集合
        /// </summary>
        /// <param name="authName"></param>
        /// <param name="authKey"></param>
        OperateResult GetAuthModuleCodes(string authName, string authKey);
    }
}
