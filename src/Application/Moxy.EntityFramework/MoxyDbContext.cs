using Microsoft.EntityFrameworkCore;
using Moxy.EntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.EntityFramework
{
    public class MoxyDbContext : DbContext
    {
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleCategory> ArticleCategory { get; set; }
        public MoxyDbContext(DbContextOptions<MoxyDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
