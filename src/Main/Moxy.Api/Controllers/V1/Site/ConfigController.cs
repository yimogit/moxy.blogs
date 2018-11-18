using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moxy.Core;
using Moxy.Data.Domain;
using Moxy.Services.Cms;

namespace Moxy.Api.Controllers.V1.Site
{

    /// <summary>
    /// 通用接口
    /// </summary>
    public class ConfigController : BaseSiteController
    {
        private readonly IWebContext _webContext;
        public ConfigController(IWebContext webContext)
        {
            _webContext = webContext;
        }
        /// <summary>
        /// pc配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("pc")]
        public IActionResult Pc()
        {
            var menus = new List<dynamic>()
            {
                new { menuName="首页",menuUrl="/"},
                new { menuName="关于",menuUrl="/about"},
                new { menuName="MeTools",menuUrl="http://tools.yimo.link"},
            };
            var result = new
            {
                siteName = "墨玄涯个人博客",
                footer = "备案号：蜀ICP备11002373号-1",
                menus,
            };
            return Ok(OperateResult.Succeed("ok", result));
        }
    }
}