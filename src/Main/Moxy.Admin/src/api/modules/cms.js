import request from '../fetch'

//#region 文章管理
export function getArticleList(params) {
  return request({
    url: '/v1/cms/article/list',
    method: 'get',
    params: params
  })
}
export function getArticleItem(params) {
  return request({
    url: '/v1/cms/article/item',
    method: 'get',
    params: params
  })
}
export function createArticle(data) {
  return request({
    url: '/v1/cms/article/create',
    method: 'POST',
    data: data
  })
}
export function editArticle(data) {
  return request({
    url: '/v1/cms/article/edit',
    method: 'POST',
    data: data
  })
}
export function delArticle(data) {
  return request({
    url: '/v1/cms/article/delete',
    method: 'POST',
    data: data
  })
}
export function setTopArticle(data) {
  return request({
    url: '/v1/cms/article/settop',
    method: 'POST',
    data: data
  })
}

//#endregion

//#region 分类管理
export function getCategoryList(params) {
  return request({
    url: '/v1/cms/category/list',
    method: 'get',
    params: params
  })
}
export function getCategoryItem(params) {
  return request({
    url: '/v1/cms/category/item',
    method: 'get',
    params: params
  })
}

export function createCategory(data) {
  return request({
    url: '/v1/cms/category/create',
    method: 'POST',
    data: data
  })
}
export function editCategory(data) {
  return request({
    url: '/v1/cms/category/edit',
    method: 'POST',
    data: data
  })
}
export function delCategory(data) {
  return request({
    url: '/v1/cms/category/delete',
    method: 'POST',
    data: data
  })
}
//#endregion

//#region

export function getCategoryOptions(params) {
  return request({
    url: '/v1/cms/category/options',
    method: 'get',
    params: params
  })
}
export function getTagsOptions(params) {
  return request({
    url: '/v1/cms/tags/options',
    method: 'get',
    params: params
  })
}
//#endregion
