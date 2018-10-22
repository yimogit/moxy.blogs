using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Moxy.Data.Domain
{
    [Table("cms_category")]
    public partial class CmsCategory : BaseEntityCreatable<int>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [Column("category_name"), MaxLength(100)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        [Column("category_desc"), MaxLength(300)]
        public string CategoryDesc { get; set; }
    }
}