using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Moxy.EntityFramework
{
    public class MoxyDbContextFactory : IDesignTimeDbContextFactory<MoxyDbContext>
    {
        public MoxyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MoxyDbContext>();

            MoxyDbContextConfigurer.ConfigureMySql(builder, "server=127.0.0.1;uid=root;pwd=123456;database=moxy_blogs_db_v2");

            return new MoxyDbContext(builder.Options);
        }
    }

}
