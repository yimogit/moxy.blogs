using Microsoft.EntityFrameworkCore;
using Moxy.Data.Domain;
using Moxy.Services.Cms.Dtos;
using Moxy.Services.Cms.Dtos.Article;
using Moxy.Services.Cms.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Services.Cms
{
    public interface IArticleService
    {
        #region 文章分类管理
        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <returns></returns>
        IPagedList<CategoryListDto> GetCategoryList(CategorySearch search);
        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <returns></returns>
        CategoryItemDto GetCategoryItem(int id);

        /// <summary>
        /// 创建文章分类
        /// </summary>
        /// <returns></returns>
        OperateResult CreateCategory(CategoryInputDto input);

        /// <summary>
        /// 修改文章分类
        /// </summary>
        /// <returns></returns>
        OperateResult UpdateCategory(CategoryInputDto input);

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <returns></returns>
        OperateResult DeleteCategory(List<int> ids);

        #endregion

        #region 文章管理
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        IPagedList<ArticleListDto> GetArticleList(ArticleSearch search);
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        ArticleItemDto GetArticleItem(int id);

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <returns></returns>
        OperateResult CreateArticle(ArticleInputDto input);

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <returns></returns>
        OperateResult UpdateArticle(ArticleInputDto input);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <returns></returns>
        OperateResult DeleteArticle(List<int> ids);
        /// <summary>
        /// 置顶文章操作
        /// </summary>
        /// <returns></returns>
        OperateResult SetTopArticle(ArticleSetTopInputDto input);

        #endregion

        #region 前台接口
        /// <summary>
        /// 获取推荐文章
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        List<ArticleTopListOutputDto> GetArticleTopList(ArticleTopSearch search);
        /// <summary>
        /// 分类汇总接口
        /// </summary>
        /// <returns></returns>
        List<CategorySummaryListOutputDto> GetCategorySummaryList();
        #endregion
    }
}
