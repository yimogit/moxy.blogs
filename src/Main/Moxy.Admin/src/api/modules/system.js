import request from '../fetch'

export function getAdminList(params) {
  return request({
    url: '/v1/system/admin/list',
    method: 'get',
    params: params
  })
}
export function getAdminItem(params) {
  return request({
    url: '/v1/system/admin/item',
    method: 'get',
    params: params
  })
}

export function getAdminModules() {
  return request({
    url: '/v1/system/admin/modules',
    method: 'get'
  })
}

export function createAdmin(data) {
  return request({
    url: '/v1/system/admin/create',
    method: 'POST',
    data: data
  })
}
export function editAdmin(data) {
  return request({
    url: '/v1/system/admin/edit',
    method: 'POST',
    data: data
  })
}
export function delAdmin(data) {
  return request({
    url: '/v1/system/admin/delete',
    method: 'POST',
    data: data
  })
}
