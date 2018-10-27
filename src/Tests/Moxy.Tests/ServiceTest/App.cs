using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moxy.Data;
using Moxy.Services.Article;
using Moxy.Services.System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Tests.ServiceTest
{
    public class App
    {
        public static readonly LoggerFactory MyLoggerFactory
        = new LoggerFactory(new[] { new Log4NetProvider("log4net.config") });
        public static IServiceProvider Init()
        {
            var Configuration = new ConfigurationBuilder()
                                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                .AddJsonFile(path: $"appsettings.json")
                                .Build();
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(e => e.AddLog4Net("log4net.config"));

            //services.AddDbContextPool<MoxyDbContext>(
            //   options => options.UseMySql(Configuration.GetConnectionString("Default"),
            //       mysqlOptions =>
            //       {
            //           mysqlOptions.ServerVersion(new Version(5, 7, 21), ServerType.MySql);
            //       }
            //));

            services
            .AddDbContext<MoxyDbContext>(opt => opt.UseInMemoryDatabase("MoxyDB"));

            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IArticleService, ArticleService>();

            services.AddDistributedMemoryCache();

            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
