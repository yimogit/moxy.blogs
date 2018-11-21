import fetch from '@/api/fetch'
const prefix = '/v1/cms'
export function getArticleTopList() {
  return fetch.get(`${prefix}/article/top/list`)
}

export function getCategoryList() {
  return fetch.get(`${prefix}/article/category/list`)
}

export function getArticleDetail(entryName) {
  return fetch({
    url: `${prefix}/article/detail`,
    params: { entryName }
  })
}
