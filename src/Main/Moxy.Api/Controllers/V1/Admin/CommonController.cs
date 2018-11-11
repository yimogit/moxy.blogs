using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moxy.Core;
using Moxy.Data.Domain;
using Moxy.Framework.Authentication;
using Moxy.Framework.Filters;
using Moxy.Framework.Permissions;
using Moxy.Services.System;
using Moxy.Services.System.Dtos;

namespace Moxy.Api.Controllers.V1.Admin
{
    /// <summary>
    /// 通用接口
    /// </summary>
    [MoxyModule(Order = 10)]
    public class CommonController : BaseAdminController
    {
        /// <summary>
        /// CommonController
        /// </summary>
        private readonly ISystemService _systemService;
        private readonly IWebContext _webContext;
        public CommonController(ISystemService systemService
            , IWebContext webContext
            )
        {
            _systemService = systemService;
            _webContext = webContext;
        }
        /// <summary>
        /// 桌面信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("home")]
        [Permission("home", "控制台", true)]
        public IActionResult Home()
        {
            return Ok();
        }


    }
}