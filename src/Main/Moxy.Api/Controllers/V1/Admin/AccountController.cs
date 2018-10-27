using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moxy.Framework.Filters;
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
        public AccountController(ISystemService systemService)
        {
            _systemService = systemService;
        }
        #region 登录
        /// <summary>
        /// 后台登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Login(AdminAccoutInputDto input)
        {
            var result = _systemService.Login(input);
            if (result.Status == ResultStatus.Error)
            {
                return Ok(result);
            }
            result.Data = new { token = result.GetData<AdminAccoutOutputDto>().AdminToken };
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
            return Ok(OperateResult.Error("ok"));
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        [Route("init")]
        [HttpGet]
        public IActionResult Init()
        {
            _systemService.InitSystem();
            return Ok("初始化成功");
        }
        #endregion


    }
}