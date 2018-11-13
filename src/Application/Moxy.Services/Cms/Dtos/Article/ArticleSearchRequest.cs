using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Cms.Dtos
{
    public class ArticleSearchRequest : PagedCriteria
    {
        public string Keyword { get; set; }
    }
}
