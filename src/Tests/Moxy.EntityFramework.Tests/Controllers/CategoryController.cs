using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moxy.EntityFramework.Domain;

namespace Moxy.EntityFramework.Tests.Controllers
{
    /// <summary>
    /// 文章分类
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// CategoryController
        /// </summary>
        private readonly IUnitOfWork<MoxyDbContext> _unitOfWork;
        public CategoryController(IUnitOfWork<MoxyDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        /// <summary>
        /// 创建分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ArticleCategory entity)
        {
            _unitOfWork.GetRepository<ArticleCategory>().Insert(entity);
            _unitOfWork.SaveChanges();
            var result = entity;
            return Ok(result);
        }
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List(int pageIndex, int pageSize)
        {
            var result = _unitOfWork.GetRepository<ArticleCategory>().GetPagedList(e => new
            {
                e.Id,
                e.CategoryName,
                e.CategoryDesc
            }, pageIndex: pageIndex, pageSize: pageSize);
            return Ok(result);
        }
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var result = _unitOfWork.GetRepository<ArticleCategory>().GetFirstOrDefault(predicate: s => s.Id == id);
            return Ok(result);
        }
    }
}
