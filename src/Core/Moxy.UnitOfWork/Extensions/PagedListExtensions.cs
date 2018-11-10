using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxy
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> superset, PagedCriteria pagedCriteria)
        {
            return superset.ToPagedList<T>(Math.Max(pagedCriteria.PageIndex, 0), pagedCriteria.PageSize, 0);
        }

        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, PagedCriteria pagedCriteria)
        {
            return await source.ToPagedListAsync<T>(Math.Max(pagedCriteria.PageIndex, 0) , pagedCriteria.PageSize, 0);
        }
    }
}
