using AutoMapper.Attributes;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Cms.Dtos.Article
{
    [MapsFrom(typeof(CmsArticle), ReverseMap = true)]
    public class ArticleTopListOutputDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 友好地址名
        /// </summary>
        public string EntryName { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArtTitle { get; set; }
        /// <summary>
        /// 文章简介
        /// </summary>
        public string ArtDesc { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }
    }
}
