using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Moxy.Data
{
    public static class MoxyDbContextConfigurer
    {
        public static void ConfigureMySql(DbContextOptionsBuilder<MoxyDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }
    }
}
