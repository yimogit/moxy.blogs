using Moxy.Data;
using Moxy.Services.System.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moxy.Data.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Moxy.Services.System
{
    public class SystemService : ISystemService
    {
        /// <summary>
        /// SystemService
        /// </summary>
        private readonly MoxyDbContext _dbContext;
        private readonly IUnitOfWork<MoxyDbContext> _unitOfWork;
        public SystemService(MoxyDbContext dbContext
            , IUnitOfWork<MoxyDbContext> unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <param name="adminName"></param>
        /// <returns></returns>
        public OperateResult InitSystem(string adminName)
        {
            if (_dbContext.SysAdmin.Count() > 0)
            {
                return OperateResult.Succeed("系统已初始化");
            }
            if (string.IsNullOrEmpty(adminName))
                return OperateResult.Error("初始化账号不能为空");
            Dictionary<string, object> model = new Dictionary<string, object>();
            string adminPwd = new Random().Next(100000, 999999).ToString();
            var admin = new SysAdmin()
            {
                AdminName = adminName,
                AdminPwd = Moxy.Utils.SecurityHelper.EncryptDES(adminPwd),
                AdminKey = new Random().Next(0, int.MaxValue).ToString(),
                IsEnable = true,
                ModuleCodes = "*"
            };
            _dbContext.SysAdmin.Add(admin);
            _dbContext.SaveChanges();
            model.Add("管理员账号：", adminName);
            model.Add("管理员密码：", adminPwd);
            model.Add("管理员列表：", "/system/admin/list");
            return OperateResult.Succeed("初始化系统成功", model);
        }

        /// <summary>
        /// 管理员验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OperateResult AuthCheck(AdminAccoutInputDto input)
        {
            var existItem = _dbContext.SysAdmin.FirstOrDefault(e => e.AdminName == input.AdminName && e.AdminPwd == input.AdminSecurityPwd);
            if (existItem == null)
            {
                return OperateResult.Error("用户名或密码错误");
            }
            if (!existItem.IsEnable)
            {
                return OperateResult.Error("账号未启用");
            }
            var output = new AdminAccoutOutputDto()
            {
                AdminName = existItem.AdminName,
                AdminKey = existItem.AdminKey,
            };
            return OperateResult.Succeed("登录成功", output);
        }
        /// <summary>
        /// 获取账号模块编码集合
        /// </summary>
        /// <param name="authName"></param>
        /// <param name="authKey"></param>
        public OperateResult GetAuthModuleCodes(string authName, string authKey)
        {
            var existItem = _dbContext.SysAdmin.FirstOrDefault(e => e.AdminName == authName && e.AdminKey == authKey);
            if (existItem == null)
                return OperateResult.Error("获取账号信息失败");
            if (!existItem.IsEnable)
                return OperateResult.Error("账号未启用");
            if (string.IsNullOrEmpty(existItem.ModuleCodes))
                return OperateResult.Error("账号未分配任何权限");

            return OperateResult.Succeed("ok", existItem.ModuleCodes);
        }

        public OperateResult GetAdminAuthInfo(string authName)
        {
            var existItem = _dbContext.SysAdmin.FirstOrDefault(e => e.AdminName == authName);
            if (existItem == null)
                return OperateResult.Error("获取账号信息失败");
            if (!existItem.IsEnable)
                return OperateResult.Error("账号未启用");
            if (string.IsNullOrEmpty(existItem.ModuleCodes))
                return OperateResult.Error("账号未分配任何权限");

            return OperateResult.Succeed("ok", new
            {
                authName = existItem.AdminName,
                menus = Utils.JsonHelper.Deserialize(existItem.Menus),
                modules = existItem.ModuleCodes.Split(',').ToList()
            });
        }
        #region 管理员管理
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        public IPagedList<SysAdminListDto> GetAdminList(SysAdminSearchRequest request)
        {
            var query = _unitOfWork.GetRepository<SysAdmin>().Table;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(s => s.AdminName.Contains(request.Keyword));
            }
            var result = query.ProjectTo<SysAdminListDto>().ToPagedList(request);
            return result;
        }

        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        public SysAdminItemDto GetAdminItem(int id)
        {
            var query = _unitOfWork.GetRepository<SysAdmin>().Table
                .Where(s => s.Id == id)
                .ProjectTo<SysAdminItemDto>();
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <returns></returns>
        public OperateResult CreateAdmin(SysAdminInputDto input)
        {
            if (_unitOfWork.GetRepository<SysAdmin>().Table.Any(s => s.AdminName == input.AdminName))
            {
                return OperateResult.Error("已存在此管理员");
            }
            var entity = AutoMapper.Mapper.Map<SysAdmin>(input);
            entity.AdminKey = new Random().Next(0, int.MaxValue).ToString();
            entity.AdminPwd = Moxy.Utils.SecurityHelper.EncryptDES(input.AdminPwd);
            _unitOfWork.GetRepository<SysAdmin>().Insert(entity);
            var row = _unitOfWork.SaveChanges();
            return row > 0 ? OperateResult.Succeed("创建成功") : OperateResult.Error("创建失败");
        }

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <returns></returns>
        public OperateResult UpdateAdmin(SysAdminInputDto input)
        {
            if (_unitOfWork.GetRepository<SysAdmin>().Table.Any(s => s.Id != input.Id && s.AdminName == input.AdminName))
            {
                return OperateResult.Error("已存在此管理员");
            }
            var existItem = _unitOfWork.GetRepository<SysAdmin>().Table.FirstOrDefault(s => s.Id == input.Id);
            if (existItem == null)
                return OperateResult.Error("数据不存在");
            existItem.AdminName = input.AdminName;
            if (!string.IsNullOrEmpty(input.AdminPwd))
            {
                existItem.AdminPwd = Utils.SecurityHelper.EncryptDES(input.AdminPwd);
                existItem.AdminKey = new Random().Next(0, int.MaxValue).ToString();
            }
            existItem.IsEnable = input.IsEnable;
            existItem.ModuleCodes = input.ModuleCodes;
            existItem.Menus = input.Menus;
            existItem.UpdatedAt = DateTime.Now;
            var row = _unitOfWork.SaveChanges();
            return row > 0 ? OperateResult.Succeed("修改成功") : OperateResult.Error("修改失败");
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <returns></returns>
        public OperateResult DeleteAdmin(List<int> ids)
        {
            var delItems = _unitOfWork.GetRepository<SysAdmin>().Table.Where(s => ids.Contains(s.Id));
            _unitOfWork.GetRepository<SysAdmin>().Delete(delItems);
            _unitOfWork.SaveChanges();
            return OperateResult.Succeed("执行成功");
        }


        #endregion
    }
}
