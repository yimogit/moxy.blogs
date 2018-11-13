using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moxy.Framework.Filters
{
    public class ModelValidAttribute : ActionFilterAttribute
    {
        private string[] _ignoreAttr { get; set; }
        public ModelValidAttribute() { }
        public ModelValidAttribute(params string[] ignoreAttr)
        {
            _ignoreAttr = ignoreAttr;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_ignoreAttr != null && _ignoreAttr.Length > 0)
            {
                foreach (var item in _ignoreAttr)
                {
                    context.ModelState.Remove(item);
                }
            }
            if (!context.ModelState.IsValid)
            {
                var errorMsgs = (from item in context.ModelState
                                 where item.Value.Errors.Any() && !string.IsNullOrEmpty(item.Value.Errors[0].ErrorMessage)
                                 select item.Value.Errors[0].ErrorMessage).ToList();
                if (errorMsgs.Count > 0)
                {
                    context.Result = new JsonResult(OperateResult.Error(string.Join("<br/>", errorMsgs)));
                }
            }
        }
    }
}
