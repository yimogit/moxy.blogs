using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Cms.Dtos
{
    public class CategorySearchRequest:PagedCriteria
    {
        public string Keyword { get; set; }
    }
}
