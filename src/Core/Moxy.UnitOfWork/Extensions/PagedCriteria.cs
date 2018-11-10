using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy
{
    public class PagedCriteria
    {
        public PagedCriteria(int pageSize = int.MaxValue, int pageIndex = 0)
        {
            this.PageSize = pageSize;
            this.PageIndex = Math.Max(0, pageIndex);
        }


        public PagedCriteria()
        {
            PageSize = int.MaxValue;
            PageIndex = 0;
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
    }
}
