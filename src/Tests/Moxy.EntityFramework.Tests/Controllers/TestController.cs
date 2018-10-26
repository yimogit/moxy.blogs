using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Moxy.EntityFramework.Domain;

namespace Moxy.EntityFramework.Tests.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        /// <测试>
        /// 获取文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Test(TestModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(model);
            }
            return Ok("验证失败");
        }
    }
    public class TestModel
    {
        [Required(ErrorMessage = "测试验证")]
        public string Abc { get; set; }
    }
}
