using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.EntityFramework
{
    public interface ICreatable
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }
}
