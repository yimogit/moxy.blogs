using Moxy.EntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Article
{
    public interface IArticleService
    {
        ArticleCategory CreateCategory(ArticleCategory input);
    }
}
