using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.EntityFramework
{
    public interface IUpdatable
    {
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}
