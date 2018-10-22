using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login([FromBody]AdminAccoutInputDto input)
        {
            if (!ModelState.IsValid)
            {
                return Ok(OperateResult.Error("参数错误"));
            }
            var result = _systemService.Login(input);
            if (result.Status == ResultStatus.Error)
            {
                return Ok(result);
            }
            result.Data = result.GetData<AdminAccoutOutputDto>().AdminToken;
            return Ok(result);
        }
        #endregion


    }
}