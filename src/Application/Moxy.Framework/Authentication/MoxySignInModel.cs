using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Framework.Authentication
{
    public class MoxySignInModel
    {
        public string AuthName { get; set; }
        public string AuthKey { get; set; }
        public DateTime? Expires { get; set; }
    }
}
