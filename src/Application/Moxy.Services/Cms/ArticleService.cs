using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Moxy.Data;
using Moxy.Data.Domain;
using Moxy.Services.Cms.Dtos;
using Moxy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moxy.Services.Cms
{
    public class ArticleService : IArticleService
    {
        /// <summary>
        /// ArticleService
        /// </summary>
        private readonly MoxyDbContext _dbContext;
        private readonly IUnitOfWork<MoxyDbContext> _unitOfWork;
        public ArticleService(MoxyDbContext dbContext
            , IUnitOfWork<MoxyDbContext> unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        #region 文章分类管理
        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <returns></returns>
        public IPagedList<CategoryListDto> GetCategoryList(CategorySearchRequest request)
        {
            var query = _unitOfWork.GetRepository<CmsCategory>().Table;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(s => s.CategoryName.Contains(request.Keyword));
            }
            var result = query.ProjectTo<CategoryListDto>().ToPagedList(request);
            return result;
        }

        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <returns></returns>
        public CategoryItemDto GetCategoryItem(int id)
        {
            var query = _unitOfWork.GetRepository<CmsCategory>().Table
                .Where(s => s.Id == id)
                .ProjectTo<CategoryItemDto>();
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 创建文章分类
        /// </summary>
        /// <returns></returns>
        public OperateResult CreateCategory(CategoryInputDto input)
        {
            if (_unitOfWork.GetRepository<CmsCategory>().Table.Any(s => s.CategoryName == input.CategoryName))
            {
                return OperateResult.Error("已存在此文章分类");
            }
            var entity = AutoMapper.Mapper.Map<CmsCategory>(input);
            _unitOfWork.GetRepository<CmsCategory>().Insert(entity);
            var row = _unitOfWork.SaveChanges();
            return row > 0 ? OperateResult.Succeed("创建成功") : OperateResult.Error("创建失败");
        }

        /// <summary>
        /// 修改文章分类
        /// </summary>
        /// <returns></returns>
        public OperateResult UpdateCategory(CategoryInputDto input)
        {
            if (_unitOfWork.GetRepository<CmsCategory>().Table.Any(s => s.Id != input.Id && s.CategoryName == input.CategoryName))
            {
                return OperateResult.Error("已存在此文章分类");
            }
            var existItem = _unitOfWork.GetRepository<CmsCategory>().Table.FirstOrDefault(s => s.Id == input.Id);
            if (existItem == null)
                return OperateResult.Error("数据不存在");
            existItem.CategoryName = input.CategoryName;
            existItem.CategoryDesc = input.CategoryDesc;
            var row = _unitOfWork.SaveChanges();
            return OperateResult.Succeed("修改成功");
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <returns></returns>
        public OperateResult DeleteCategory(List<int> ids)
        {
            var delItems = _unitOfWork.GetRepository<CmsCategory>().Table.Where(s => ids.Contains(s.Id));
            _unitOfWork.GetRepository<CmsCategory>().Delete(delItems);
            _unitOfWork.SaveChanges();
            return OperateResult.Succeed("删除成功");
        }
        #endregion

        #region 文章管理
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public IPagedList<ArticleListDto> GetArticleList(ArticleSearchRequest request)
        {
            var query = _unitOfWork.GetRepository<CmsArticle>().Table;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(s => s.ArtTitle.Contains(request.Keyword));
            }
            var result = query.ProjectTo<ArticleListDto>().ToPagedList(request);
            return result;
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ArticleItemDto GetArticleItem(int id)
        {
            var query = _unitOfWork.GetRepository<CmsArticle>().Table
                .Where(s => s.Id == id)
                .ProjectTo<ArticleItemDto>();
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <returns></returns>
        public OperateResult CreateArticle(ArticleInputDto input)
        {
            if (_unitOfWork.GetRepository<CmsArticle>().Table.Any(s => s.ArtTitle == input.ArtTitle))
            {
                return OperateResult.Error("已存在此文章");
            }
            input.EntryName = string.IsNullOrEmpty(input.EntryName) ? RandomHelper.NewGuid() : input.EntryName;
            if (_unitOfWork.GetRepository<CmsArticle>().Table.Any(s => s.EntryName == input.EntryName))
            {
                return OperateResult.Error("已存在此友好地址名");
            }
            var entity = AutoMapper.Mapper.Map<CmsArticle>(input);

            _unitOfWork.GetRepository<CmsArticle>().Insert(entity);
            var row = _unitOfWork.SaveChanges();
            return row > 0 ? OperateResult.Succeed("创建成功") : OperateResult.Error("创建失败");
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <returns></returns>
        public OperateResult UpdateArticle(ArticleInputDto input)
        {
            if (_unitOfWork.GetRepository<CmsArticle>().Table.Any(s => s.Id != input.Id && s.ArtTitle == input.ArtTitle))
            {
                return OperateResult.Error("已存在此文章");
            }
            input.EntryName = string.IsNullOrEmpty(input.EntryName) ? RandomHelper.NewGuid() : input.EntryName;
            if (_unitOfWork.GetRepository<CmsArticle>().Table.Any(s => s.Id != input.Id && s.EntryName == input.EntryName))
            {
                return OperateResult.Error("已存在此友好地址名");
            }
            var existItem = _unitOfWork.GetRepository<CmsArticle>().Table.FirstOrDefault(s => s.Id == input.Id);
            if (existItem == null)
                return OperateResult.Error("数据不存在");
            existItem.ArtTitle = input.ArtTitle;
            existItem.EntryName = input.EntryName;
            existItem.ArtContent = input.ArtContent;
            existItem.ArtDesc = input.ArtDesc;
            existItem.CategoryId = input.CategoryId;
            existItem.ReleaseTime = input.ReleaseTime;
            existItem.Tags = input.Tags;

            existItem.UpdatedAt = DateTime.Now;
            var row = _unitOfWork.SaveChanges();
            return row > 0 ? OperateResult.Succeed("修改成功") : OperateResult.Error("修改失败");
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <returns></returns>
        public OperateResult DeleteArticle(List<int> ids)
        {
            var delItems = _unitOfWork.GetRepository<CmsArticle>().Table.Where(s => ids.Contains(s.Id));
            _unitOfWork.GetRepository<CmsArticle>().Delete(delItems);
            _unitOfWork.SaveChanges();
            return OperateResult.Succeed("执行成功");
        }

        #endregion
    }
}
