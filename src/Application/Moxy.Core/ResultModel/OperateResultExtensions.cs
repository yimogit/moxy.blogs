using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy
{

    public static class OperateResultExtensions
    {
        public static T GetData<T>(this OperateResult result) where T : class
        {
            return result.Data as T;
        }
    }
}
