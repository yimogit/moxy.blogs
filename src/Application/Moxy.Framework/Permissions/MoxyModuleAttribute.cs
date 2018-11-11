using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Framework.Permissions
{
    /// <summary>
    /// 模块描述,用于生成模块
    /// </summary>
    public class MoxyModuleAttribute : Attribute
    {
        public int Order { get; set; }
        public string ModuleName { get; set; }
    }
}
