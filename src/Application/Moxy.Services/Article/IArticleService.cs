using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Article
{
    public interface IArticleService
    {
        CmsCategory CreateCategory(CmsCategory input);
    }
}
