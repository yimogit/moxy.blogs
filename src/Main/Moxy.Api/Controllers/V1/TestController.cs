using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moxy.Data.Domain;
using Moxy.Services.Cms;

namespace Moxy.Api.Controllers.V1
{

    /// <summary>
    /// 测试接口
    /// </summary>
    public class TestController : BaseSiteController
    {
        private readonly IArticleService _articleService;
        public TestController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        /// <summary>
        /// 列表页请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public IActionResult GetList([FromQuery]PagedCriteria pagedCriteria)
        {

            return Ok(new { version = "list-v1", pagedCriteria });
        }
        /// <summary>
        /// 详情页请求
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("detail")]
        public IActionResult GetDetail(int page = 0)
        {
            return Ok(new { version = "detail-v1" });
        }
        /// <summary>
        /// 修改接口
        /// </summary>
        /// <param name="value"></param>
        [Route("update")]
        [HttpPost]
        public void Post([FromBody, Required] string value)
        {
        }
        /// <summary>
        /// 创建文章分类
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryDesc"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public IActionResult CreateCategory(string categoryName, string categoryDesc)
        {
            return Ok();
            //var category = _articleService.CreateCategory(new CmsCategory()
            //{
            //    CategoryName = categoryName,
            //    CategoryDesc = categoryDesc
            //});
            //return Ok(category);
        }
    }
}