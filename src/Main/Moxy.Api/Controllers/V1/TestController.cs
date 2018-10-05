using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moxy.Api.Controllers.V1
{

    /// <summary>
    /// 测试接口
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/test")]
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

            return Ok(new { version = "list-v1" });
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
    }
}