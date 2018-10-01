using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moxy.Api.Controllers.V2
{
    /// <summary>
    /// 测试接口V2
    /// </summary>
    [Route("api/v2/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 列表页请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public IActionResult GetList()
        {
            return Ok(new { version = "list-v2" });
        }
        /// <summary>
        /// 详情页请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("detail")]
        public IActionResult GetDetail()
        {
            return Ok(new { version = "detail-v2" });
        }
    }
}