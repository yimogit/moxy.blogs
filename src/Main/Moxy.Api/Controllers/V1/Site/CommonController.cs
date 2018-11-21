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
    public class CommonController : BaseSiteController
    {
        private readonly IWebContext _webContext;
        private readonly IArticleService _articleService;
        public CommonController(IWebContext webContext, IArticleService articleService)
        {
            _webContext = webContext;
            _articleService = articleService;
        }
        /// <summary>
        /// 友情链接列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("friend/list")]
        public IActionResult GetFriendList()
        {
            var list = new List<dynamic>()
            {
                new {
                    text ="易墨博客",
                    link ="https://www.yimo.link"
                }
            };
            return Ok(OperateResult.Succeed("ok", list));
        }
    }
}