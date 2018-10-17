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
    /// 文章
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        /// <summary>
        /// ArticleController
        /// </summary>
        private readonly IUnitOfWork<MoxyDbContext> _unitOfWork;
        public ArticleController(IUnitOfWork<MoxyDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Article entity)
        {
            _unitOfWork.GetRepository<Article>().Insert(entity);
            _unitOfWork.SaveChanges();
            var result = entity;
            return Ok(result);
        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List(int pageIndex, int pageSize)
        {
            var result = _unitOfWork.GetRepository<Article>().GetPagedList(e => e, pageIndex: Math.Max(0, pageIndex - 1), pageSize: Math.Max(0, pageSize));
            return Ok(result);
        }
        /// <summary>
        /// 获取文章
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var result = _unitOfWork.GetRepository<Article>().GetFirstOrDefault(predicate: s => s.Id == id);
            return Ok(result);
        }
    }
}
