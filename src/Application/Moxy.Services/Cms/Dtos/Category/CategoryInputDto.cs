using AutoMapper.Attributes;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Moxy.Services.Cms.Dtos
{
    [MapsFrom(typeof(CmsCategory), ReverseMap = true)]
    public class CategoryInputDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required(ErrorMessage = "分类名称不能为空")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string CategoryDesc { get; set; }
    }
}
