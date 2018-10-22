using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Data.Mapping
{
    public partial class SysAdminMap : IEntityTypeConfiguration<SysAdmin>
    {
        public void Configure(EntityTypeBuilder<SysAdmin> modelBuilder)
        {
        }
    }
}
