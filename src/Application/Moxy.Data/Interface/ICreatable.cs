using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Data
{
    public interface ICreatable
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }
}
