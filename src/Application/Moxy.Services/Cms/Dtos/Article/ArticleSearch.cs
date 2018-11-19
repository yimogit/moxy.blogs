using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Cms.Dtos.Article
{
    public class ArticleSearch : PagedCriteria
    {
        public string Keyword { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool? IsSetTop { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool? IsRelease { get; set; }
    }
}
