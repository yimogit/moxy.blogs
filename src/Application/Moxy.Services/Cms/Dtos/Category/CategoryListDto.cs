using AutoMapper.Attributes;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Cms.Dtos
{
    [MapsFrom(typeof(CmsCategory), ReverseMap = true)]
    public class CategoryListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string CategoryDesc { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
