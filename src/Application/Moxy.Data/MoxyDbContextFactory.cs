using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Moxy.Data
{
    /// <summary>
    /// CodeFirst 生成数据库的配置
    /// </summary>
    public class MoxyDbContextFactory : IDesignTimeDbContextFactory<MoxyDbContext>
    {
        private IConfiguration Configuration { get; }
        public MoxyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MoxyDbContext>();

            MoxyDbContextConfigurer.ConfigureMySql(builder, "server=127.0.0.1;uid=root;pwd=123456;database=moxy_blogs_db");
            return new MoxyDbContext(builder.Options);
        }
    }

}
