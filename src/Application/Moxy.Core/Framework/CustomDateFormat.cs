using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Core
{
    public class CustomDateFormat : IsoDateTimeConverter
    {
        public CustomDateFormat()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
