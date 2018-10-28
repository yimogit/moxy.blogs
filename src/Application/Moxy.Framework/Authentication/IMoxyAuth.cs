using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Framework.Authentication
{
    public interface IMoxyAuth
    {
        /// <summary>
        /// 登录验证 成功返回登录模型MoxySignInModel
        /// </summary>
        /// <returns></returns>
        OperateResult CheckSign();
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        OperateResult SignIn(MoxySignInModel model);
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        OperateResult SignOut();
    }
}
