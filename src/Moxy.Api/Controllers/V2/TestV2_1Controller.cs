using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moxy.Api.Controllers.V2
{
    /// <summary>
    /// 测试接口V2.1
    /// </summary>
    [ApiVersion("2.1")]
    [Route("api/v{api-version:apiVersion}/test")]
    [ApiController]
    public class TestV2_1Controller : ControllerBase
    {
        /// <summary>
        /// 列表页请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public IActionResult GetList()
        {

            return Ok(new { version = "list-v2.1" });
        }
    }
}