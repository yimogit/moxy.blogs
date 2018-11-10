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
    /// 后台登录接口
    /// </summary>
    public class AccountController : BaseAdminController
    {
        /// <summary>
        /// AccountController
        /// </summary>
        private readonly ISystemService _systemService;
        private readonly IMoxyAuth _moxyAuth;
        private readonly IWebContext _webContext;
        public AccountController(ISystemService systemService
            , IMoxyAuth moxyAuth
            , IWebContext webContext
            )
        {
            _systemService = systemService;
            _moxyAuth = moxyAuth;
            _webContext = webContext;
        }
        #region 登录
        /// <summary>
        /// 后台登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]AdminAccoutInputDto input)
        {
            var result = _systemService.AuthCheck(input);
            if (result.Status == ResultStatus.Error)
            {
                return Ok(result);
            }
            var output = result.GetData<AdminAccoutOutputDto>();
            result = _moxyAuth.SignIn(new MoxySignInModel()
            {
                AuthName = output.AdminName,
                AuthKey = output.AdminKey,
            });
            return Ok(result);
        }
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getinfo")]
        public IActionResult GetInfo()
        {
            var i = 1;
            return Ok(OperateResult.Succeed("ok", new
            {
                info = new
                {
                    adminName = _webContext.AuthName
                },
                menus = new List<dynamic>()
                {
                    new {
                        menuId= i++,
                        menuName= "控制台",
                        menuIcon= "el-icon-menu",
                        menuCode="home",
                    },
                    new {
                        menuId= i++,
                        menuName= "系统管理",
                        menuIcon= "el-icon-setting",
                        children=new List<dynamic>()
                        {
                            new {
                                menuId= i++,
                                menuName= "管理员列表",
                                menuIcon= "el-icon-document",
                                menuCode="system_admin_list",
                            },
                        }
                    },
                },
                modules = new List<string>() { "*" }
            }));
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        [Route("init")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Init(string adminName)
        {
            var result = _systemService.InitSystem(adminName);
            return Ok(result);
        }
        #endregion


    }
}