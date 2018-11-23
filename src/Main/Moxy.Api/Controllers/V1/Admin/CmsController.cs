using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moxy.Core;
using Moxy.Framework.Authentication;
using Moxy.Framework.Filters;
using Moxy.Framework.Permissions;
using Moxy.Services.Cms;
using Moxy.Services.Cms.Dtos;
using Moxy.Services.Cms.Dtos.Article;
using Moxy.Services.System;
using Moxy.Services.System.Dtos;

namespace Moxy.Api.Controllers.V1.Admin
{
    /// <summary>
    /// 内容管理
    /// </summary>
    [MoxyModule(Order = 20, ModuleName = "内容管理")]
    public class CmsController : BaseAdminController
    {
        /// <summary>
        /// ArticleController
        /// </summary>
        private readonly IWebContext _webContext;
        private readonly IArticleService _articleService;
        public CmsController(
            IWebContext webContext
            , IArticleService articleService
            )
        {
            _webContext = webContext;
            _articleService = articleService;
        }
        #region 文章管理
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("article/list")]
        [Permission("cms_article_list", "文章列表", true)]
        public IActionResult ArticleList(ArticleSearch search)
        {
            var result = _articleService.GetArticleList(search);
            return Ok(OperateResult.Succeed("ok", result));
        }
        /// <summary>
        /// 文章信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("article/item")]
        [RelyPermission("cms_article_create", "cms_article_edit")]
        public IActionResult ArticleItem(int id)
        {
            var result = _articleService.GetArticleItem(id);
            return Ok(OperateResult.Succeed("ok", result));
        }
        /// <summary>
        /// 文章创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("article/create")]
        [Permission("cms_article_create", "文章创建")]
        [ModelValid]
        public IActionResult ArticleCreate([FromBody]ArticleInputDto input)
        {
            var result = _articleService.CreateArticle(input);
            return Ok(result);
        }
        /// <summary>
        /// 文章编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("article/edit")]
        [Permission("cms_article_edit", "文章编辑")]
        public IActionResult ArticleEdit([FromBody]ArticleInputDto input)
        {
            var result = _articleService.UpdateArticle(input);
            return Ok(result);
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("article/delete")]
        [Permission("cms_article_del", "文章删除")]
        public IActionResult DeleteArticle([FromBody]IdsRequest<int> request)
        {
            var result = _articleService.DeleteArticle(request.Ids);
            return Ok(result);
        }
        /// <summary>
        /// 置顶文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("article/settop")]
        [Permission("cms_article_settop", "文章置顶")]
        public IActionResult SetTopArticle([FromBody]ArticleSetTopInputDto input)
        {
            var result = _articleService.SetTopArticle(input);
            return Ok(result);
        }
        #endregion
        #region 下拉项

        /// <summary>
        /// 分类下拉选项
        /// </summary>
        /// <returns></returns>
        [RelyPermission("cms_article_create", "cms_article_edit")]
        [HttpGet]
        [Route("category/options")]
        public IActionResult CategoryOptions()
        {
            var result = _articleService.GetCategoryList(new CategorySearch()).Items.Select(e => new SelectOptionItemModel(e.CategoryName, e.Id)).ToList();
            return Ok(OperateResult.Succeed("ok", result));
        }
        /// <summary>
        /// 标签下拉选项
        /// </summary>
        /// <returns></returns>
        [RelyPermission("cms_article_create", "cms_article_edit")]
        [HttpGet]
        [Route("tags/options")]
        public IActionResult ArticleTagsOptions()
        {
            var result = _articleService.GetArticleTagList().Select(e => new SelectOptionItemModel(e, e)).ToList();
            return Ok(OperateResult.Succeed("ok", result));
        }
        #endregion
        #region 文章分类管理
        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("category/list")]
        [Permission("cms_category_list", "文章分类列表", true)]
        public IActionResult CategoryList(CategorySearch search)
        {
            var result = _articleService.GetCategoryList(search);
            return Ok(OperateResult.Succeed("ok", result));
        }
        /// <summary>
        /// 文章分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("category/item")]
        [RelyPermission("cms_category_create", "cms_category_edit")]
        public IActionResult CategoryItem(int id)
        {
            var result = _articleService.GetCategoryItem(id);
            return Ok(OperateResult.Succeed("ok", result));
        }
        /// <summary>
        /// 文章分类创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("category/create")]
        [Permission("cms_category_create", "文章分类创建")]
        [ModelValid]
        public IActionResult CategoryCreate([FromBody]CategoryInputDto input)
        {
            var result = _articleService.CreateCategory(input);
            return Ok(result);
        }
        /// <summary>
        /// 文章分类编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("category/edit")]
        [Permission("cms_category_edit", "文章分类编辑")]
        public IActionResult CategoryEdit([FromBody]CategoryInputDto input)
        {
            var result = _articleService.UpdateCategory(input);
            return Ok(result);
        }
        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("category/delete")]
        [Permission("cms_category_delete", "文章分类删除")]
        public IActionResult DeleteCategory([FromBody]IdsRequest<int> request)
        {
            var result = _articleService.DeleteCategory(request.Ids);
            return Ok(result);
        }
        #endregion

    }
}