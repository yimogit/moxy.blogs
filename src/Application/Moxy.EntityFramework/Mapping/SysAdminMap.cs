using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moxy.EntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.EntityFramework.Mapping
{
    public partial class SysAdminMap : IEntityTypeConfiguration<SysAdmin>
    {
        public void Configure(EntityTypeBuilder<SysAdmin> builder)
        {
        }
    }
}
