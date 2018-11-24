using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Cms.Dtos.Article
{
    public class ArticleTopSearch : PagedCriteria
    {
        public bool? FilterBySetTop { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
