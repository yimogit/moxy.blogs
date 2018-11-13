using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Utils
{
    public class RandomHelper
    {
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
