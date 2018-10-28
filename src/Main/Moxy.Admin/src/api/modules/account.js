import request from '../fetch'

export function login(data) {
  return request({
    url: '/v1/account/login',
    method: 'post',
    data: data
  }).then(res => {
    if (res.data && res.data) {
      localStorage.token = res.data
    }
  })
}

export function getInfo() {
  return request({
    url: '/v1/account/getinfo',
    method: 'get'
  })
}
