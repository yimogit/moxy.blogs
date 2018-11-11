using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moxy.Core;
using Moxy.Framework.Authentication;
using Moxy.Services.System.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Framework
{
    public class WebContext : IWebContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMoxyAuth _moxyAuth;
        public WebContext(IHttpContextAccessor httpContextAccessor
            , IMoxyAuth moxyAuth)
        {
            _httpContextAccessor = httpContextAccessor;
            _moxyAuth = moxyAuth;
        }


        private string _authName;
        public string AuthName
        {
            get
            {
                if (!string.IsNullOrEmpty(_authName))
                    return _authName;
                var signResult = _moxyAuth.CheckSign();
                if (signResult.Status == ResultStatus.Error)
                    return null;
                _authName = signResult.GetData<MoxySignInModel>()?.AuthName;
                return _authName;
            }
        }
    }
}
