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
    /// 内容接口
    /// </summary>
    [ControllerName("cms")]
    public class CmsController : BaseSiteController
    {
        private readonly IWebContext _webContext;
        private readonly IArticleService _articleService;
        public CmsController(IWebContext webContext, IArticleService articleService)
        {
            _webContext = webContext;
            _articleService = articleService;
        }
        /// <summary>
        /// 文章推荐列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("article/top/list")]
        public IActionResult GetArticleTopList()
        {
            var topList = _articleService.GetArticleTopList(new Services.Cms.Dtos.Article.ArticleTopSearch()
            {
                FilterBySetTop = true
            });
            return Ok(OperateResult.Succeed("ok", topList));
        }
        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("article/category/list")]
        public IActionResult GetCategoryList()
        {
            var categoryList = _articleService.GetCategorySummaryList();
            return Ok(OperateResult.Succeed("ok", categoryList));
        }
    }
}