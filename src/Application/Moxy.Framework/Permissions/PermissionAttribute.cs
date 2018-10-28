using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Framework.Permissions
{
    public class PermissionAttribute : Attribute, IFilterMetadata
    {
        public string ModuleCode { get; set; }
        public bool IsPage { get; set; }
        public string ModuleName { get; set; }
        public PermissionAttribute(string code, string moduleName = null, bool isPage = false)
        {
            ModuleCode = code;
            IsPage = isPage;
            ModuleName = moduleName ?? code;
        }
    }
}
