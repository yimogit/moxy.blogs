using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.System.Dtos
{
    public class SysAdminSearchRequest: PagedCriteria
    {
        public string Keyword { get; set; }
    }
}
