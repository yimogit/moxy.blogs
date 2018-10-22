using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.EntityFramework
{
    public interface ISoftDeletable
    {
        bool? IsDeleted { get; set; }
        string DeletedBy { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
