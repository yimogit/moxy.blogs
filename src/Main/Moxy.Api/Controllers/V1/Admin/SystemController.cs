using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moxy.Core;
using Moxy.Framework.Authentication;
using Moxy.Framework.Filters;
using Moxy.Framework.Permissions;
using Moxy.Services.System;
using Moxy.Services.System.Dtos;

namespace Moxy.Api.Controllers.V1.Admin
{
    /// <summary>
    /// 系统给管理接口
    /// </summary>
    public class SystemController : BaseAdminController
    {
        /// <summary>
        /// AccountController
        /// </summary>
        private readonly ISystemService _systemService;
        private readonly IMoxyAuth _moxyAuth;
        private readonly IWebContext _webContext;
        public SystemController(ISystemService systemService
            , IMoxyAuth moxyAuth
            , IWebContext webContext
            )
        {
            _systemService = systemService;
            _moxyAuth = moxyAuth;
            _webContext = webContext;
        }
        #region 管理员管理
        /// <summary>
        /// 管理员搜索
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("admin/list")]
        [Permission("system_admin_list", "管理员列表", true)]
        public IActionResult AdminList()
        {
            return Ok(OperateResult.Error("ok"));
        }
        /// <summary>
        /// 管理员创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/create")]
        [Permission("system_admin_create", "管理员创建", true)]
        public IActionResult AdminCreate()
        {
            return Ok(OperateResult.Error("ok"));
        }
        /// <summary>
        /// 管理员编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/edit")]
        [Permission("system_admin_edit", "管理员编辑", false)]
        public IActionResult AdminEdit()
        {
            return Ok(OperateResult.Error("ok"));
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/delete")]
        [Permission("system_admin_delete", "管理员删除", false)]
        public IActionResult DeleteAdmin()
        {
            return Ok(OperateResult.Error("ok"));
        }
        #endregion


    }
}