import Layout from '@/components/VLayout/Index.vue'
const _import = require('../_import_' + process.env.NODE_ENV)

export default {
  path: '/cms',
  component: Layout,
  children: [
    {
      path: 'category/list',
      name: 'cms_category_list',
      component: _import('cms/category/list'),
      meta: {
        auth: true,
        title: '文章分类列表'
      }
    },
    {
      path: 'category/create',
      name: 'cms_category_create',
      component: _import('cms/category/edit'),
      meta: {
        auth: true,
        title: '文章分类创建',
        pname: 'cms_category_list'
      }
    },
    {
      path: 'category/edit/:id',
      name: 'cms_category_edit',
      component: _import('cms/category/edit'),
      meta: {
        auth: true,
        title: '文章分类编辑',
        pname: 'cms_category_list'
      }
    },

    {
      path: 'article/list',
      name: 'cms_article_list',
      component: _import('cms/article/list'),
      meta: {
        auth: true,
        title: '文章列表'
      }
    },
    {
      path: 'article/create',
      name: 'cms_article_create',
      component: _import('cms/article/edit'),
      meta: {
        auth: true,
        title: '文章创建',
        pname: 'cms_article_list'
      }
    },
    {
      path: 'article/edit/:id',
      name: 'cms_article_edit',
      component: _import('cms/article/edit'),
      meta: {
        auth: true,
        title: '文章编辑',
        pname: 'cms_article_list'
      }
    }
  ]
}
