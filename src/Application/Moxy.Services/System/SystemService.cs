using Moxy.Data;
using Moxy.Services.System.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moxy.Data.Domain;

namespace Moxy.Services.System
{
    public class SystemService : ISystemService
    {
        /// <summary>
        /// SystemService
        /// </summary>
        private readonly MoxyDbContext _dbContext;
        public SystemService(MoxyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <param name="adminName"></param>
        /// <returns></returns>
        public OperateResult InitSystem(string adminName)
        {
            Dictionary<string, string> model = new Dictionary<string, string>();
            if (_dbContext.SysAdmin.Count() == 0)
            {
                if (string.IsNullOrEmpty(adminName))
                    return OperateResult.Error("初始化账号不能为空");
                string adminPwd = new Random().Next(100000, 999999).ToString();
                _dbContext.SysAdmin.Add(new SysAdmin()
                {
                    AdminName = adminName,
                    AdminPwd = Moxy.Utils.SecurityHelper.EncryptDES(adminPwd),
                    AdminKey = new Random().Next(0, int.MaxValue).ToString(),
                    IsEnable = true,
                    ModuleCodes = "*"
                });
                _dbContext.SaveChanges();
                model.Add("adminName", adminName);
                model.Add("adminPwd", adminPwd);
            }
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
    }
}
