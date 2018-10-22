using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Data
{
    public interface IUpdatable
    {
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}
