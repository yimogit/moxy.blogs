using AutoMapper.Attributes;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Cms.Dtos.Category
{
    [MapsFrom(typeof(CmsCategory), ReverseMap = true)]
    public class CategorySummaryListOutputDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        public int TotalNum { get; set; }
    }
}
