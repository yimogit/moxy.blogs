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
    /// 系统管理接口
    /// </summary>
    public class SystemController : BaseAdminController
    {
        /// <summary>
        /// SystemController
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
        /// 管理员列表
        /// </summary>
        /// <param name="pagedCriteria"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("admin/list")]
        [Permission("system_admin_list", "管理员列表", true)]
        public IActionResult AdminList(SysAdminSearchRequest request)
        {
            var result = _systemService.GetAdminList(request);
            return Ok(OperateResult.Succeed("ok", result));
        }
        /// <summary>
        /// 管理员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("admin/item")]
        [Permission("system_admin_item", "管理员信息", false)]
        public IActionResult AdminItem(int id)
        {
            var result = _systemService.GetAdminItem(id);
            return Ok(OperateResult.Succeed("ok", result));
        }
        /// <summary>
        /// 管理员创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/create")]
        [Permission("system_admin_create", "管理员创建", true)]
        [ModelValid]
        public IActionResult AdminCreate([FromBody]SysAdminInputDto input)
        {
            var result = _systemService.CreateAdmin(input);
            return Ok(result);
        }
        /// <summary>
        /// 管理员编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/edit")]
        [Permission("system_admin_edit", "管理员编辑", false)]
        [ModelValid("AdminPwd")]
        public IActionResult AdminEdit([FromBody]SysAdminInputDto input)
        {
            var result = _systemService.UpdateAdmin(input);
            return Ok(result);
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/delete")]
        [Permission("system_admin_del", "管理员删除", false)]
        public IActionResult DeleteAdmin([FromBody]IdsRequest<int> request)
        {
            var result = _systemService.DeleteAdmin(request.Ids);
            return Ok(result);
        }
        #endregion


    }
}