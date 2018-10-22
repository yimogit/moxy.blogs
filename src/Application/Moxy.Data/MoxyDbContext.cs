using Microsoft.EntityFrameworkCore;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Moxy.Data.Extensions;

namespace Moxy.Data
{
    public class MoxyDbContext : DbContext
    {
        public DbSet<CmsArticle> Article { get; set; }
        public DbSet<CmsCategory> ArticleCategory { get; set; }
        public DbSet<SysAdmin> SysAdmin { get; set; }
        public DbSet<SysConfig> SysConfig { get; set; }

        public MoxyDbContext(DbContextOptions<MoxyDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var currentAssembly = GetType().Assembly;
            //modelBuilder.ApplyConfigurationsFromAssembly(currentAssembly, $"{currentAssembly.GetName().Name}.Mapping");
            modelBuilder.ApplyDeletedFilter();
        }
    }
}
