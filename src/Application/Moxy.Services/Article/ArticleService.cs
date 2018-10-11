using Moxy.EntityFramework;
using Moxy.EntityFramework.Domain;
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
        public ArticleCategory CreateCategory(ArticleCategory input)
        {
            _dbContext.Add<ArticleCategory>(input);
            _dbContext.SaveChanges();
            return input;
            //return _articleRepository.Insert(input);
        }
    }
}
