using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moxy.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Framework.Authentication
{
    public class MoxyAuth : IMoxyAuth
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MoxyAuth(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private string Default_Header_Name = "Authorization";
        private string Default_Auth_Key = "Authorization";
        public OperateResult CheckSign()
        {
            if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(Default_Header_Name, out StringValues authToken) || string.IsNullOrEmpty(authToken))
            {
                return OperateResult.Error("未登录");
            }
            var model = JsonHelper.Deserialize<MoxySignInModel>(SecurityHelper.DecryptDES(authToken));
            if (model == null || string.IsNullOrEmpty(model.AuthName))
                return OperateResult.Error("未登录");
            if (model.Expires.HasValue && model.Expires.Value > DateTime.Now)
                return OperateResult.Error("登录失效");

            return OperateResult.Succeed("已登录", model);
        }


        public OperateResult SignIn(MoxySignInModel model)
        {
            if (string.IsNullOrEmpty(model.AuthKey))
            {
                model.AuthKey = Default_Auth_Key;
            }
            var tokenJson = JsonHelper.Serialize(model);
            var token = SecurityHelper.EncryptDES(tokenJson);
            _httpContextAccessor.HttpContext.Response.Headers.Add(Default_Header_Name, token);

            return OperateResult.Succeed("登录成功", token);
        }

        public OperateResult SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
