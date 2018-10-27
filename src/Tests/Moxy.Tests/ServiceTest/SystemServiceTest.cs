using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moxy.Services.System;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Moxy.Tests.ServiceTest
{
    [TestClass]
    public class SystemServiceTest
    {
        private IServiceProvider _serviceProvider;
        [TestMethod]
        public void 初始化测试()
        {
            _serviceProvider = App.Init();
            var initResult = _serviceProvider.GetService<ISystemService>()
                .InitSystem();
            Assert.IsTrue(initResult.Status == ResultStatus.Succeed);
        }

        [TestMethod]
        public void 登录测试()
        {
            _serviceProvider = App.Init();
            var initResult = _serviceProvider.GetService<ISystemService>()
                .InitSystem("test");
            Assert.IsTrue(initResult.Status == ResultStatus.Succeed);
            string adminName = initResult.GetData<Dictionary<string, string>>()["adminName"];
            string adminPwd = initResult.GetData<Dictionary<string, string>>()["adminPwd"];

            var result = _serviceProvider.GetService<ISystemService>()
                .Login(new Services.System.Dtos.AdminAccoutInputDto()
                {
                    AdminName = adminName,
                    AdminPwd = adminPwd
                });
            Assert.IsTrue(result.Status == ResultStatus.Succeed);
        }
    }
}
