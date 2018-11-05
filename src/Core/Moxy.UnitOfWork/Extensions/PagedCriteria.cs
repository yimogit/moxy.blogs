using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy
{
    public class PagedCriteria
    {
        public PagedCriteria(int pageSize = int.MaxValue, int pageIndex = 1)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
        }


        public PagedCriteria()
        {
            PageSize = int.MaxValue;
            PageIndex = 1;
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
    }
}
