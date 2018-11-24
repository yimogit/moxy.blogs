import fetch from '@/api/fetch'
const prefix = '/v1/cms'

export function getArticleTopList() {
  return fetch({
    url: `${prefix}/article/list`,
    params: { filterBySetTop: true }
  })
}
export function getArticleDetail(entryName) {
  return fetch({
    url: `${prefix}/article/detail`,
    params: { entryName }
  })
}
export function getCategoryList() {
  return fetch.get(`${prefix}/category/list`)
}
export function getCategoryArtList(params) {
  return fetch({
    url: `${prefix}/article/list`,
    params: params
  })
}
