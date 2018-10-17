using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Moxy.EntityFramework.Domain
{
    [Table("sys_config")]
    public class SysConfig
    {
        [Key, Column("code"), MaxLength(200)]
        public string Code { get; set; }
        [Column("value")]
        public string Value { get; set; }
    }
}
