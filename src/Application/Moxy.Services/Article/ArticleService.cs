using Moxy.Data;
using Moxy.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly MoxyDbContext _dbContext;
        public ArticleService(MoxyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CmsCategory CreateCategory(CmsCategory input)
        {
            _dbContext.Add<CmsCategory>(input);
            _dbContext.SaveChanges();
            return input;
            //return _articleRepository.Insert(input);
        }
    }
}
