using AutoMapper.Attributes;
using Moxy.Core;
using Moxy.Data.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moxy.Services.Cms.Dtos.Article
{

    [MapsFrom(typeof(CmsArticle), ReverseMap = true)]
    public class ArticleDetailOutputDto
    {
        /// <summary>
        /// 文章作者
        /// </summary>
        public string ArtAuthor { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArtTitle { get; set; }
        public string ArtDesc { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        [JsonConverter(typeof(CustomDateFormat))]
        public DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Tags { get; set; }
        public List<string> TagList { get; set; }

        public string ArtContent { get; set; }
    }
}
