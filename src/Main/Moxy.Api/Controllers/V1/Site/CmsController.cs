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
using Moxy.Services.Cms.Dtos.Article;

namespace Moxy.Api.Controllers.V1.Site
{

    /// <summary>
    /// 内容接口
    /// </summary>
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
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("article/list")]
        public IActionResult GetArticleList(ArticleTopSearch search)
        {
            var topList = _articleService.GetArticleOutputList(search);
            return Ok(OperateResult.Succeed("ok", topList));
        }
        /// <summary>
        /// 获取文章信息
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("article/detail")]
        public IActionResult GetArticleDetail(string entryName)
        {
            var model = _articleService.GetDisplayArticleDetail(entryName);
            if (model == null)
                return Ok(OperateResult.Error("数据不存在"));
            return Ok(OperateResult.Succeed("ok", model));
        }
        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("category/list")]
        public IActionResult GetCategoryList()
        {
            var categoryList = _articleService.GetCategorySummaryList();
            return Ok(OperateResult.Succeed("ok", categoryList));
        }
    }
}