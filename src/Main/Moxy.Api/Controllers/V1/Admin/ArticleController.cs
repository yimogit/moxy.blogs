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
using Moxy.Services.Article;
using Moxy.Services.System;
using Moxy.Services.System.Dtos;

namespace Moxy.Api.Controllers.V1.Admin
{
    /// <summary>
    /// 文章管理接口
    /// </summary>
    [ControllerName("article")]
    [MoxyModule(Order = 20, ModuleName = "文章管理")]
    public class ArticleController : BaseAdminController
    {
        /// <summary>
        /// ArticleController
        /// </summary>
        private readonly IWebContext _webContext;
        private readonly IArticleService _articleService;
        public ArticleController(
            IWebContext webContext
            , IArticleService articleService
            )
        {
            _webContext = webContext;
            _articleService = articleService;
        }
        #region 文章管理
        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("category/list")]
        [Permission("article_category_list", "文章分类列表", true)]
        public IActionResult CategoryList()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 文章分类创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("category/create")]
        [Permission("article_category_create", "文章分类创建")]
        public IActionResult CategoryCreate()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}