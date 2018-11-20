import axios from 'axios'
import _config from '@/config'
// import qs from 'qs'F

const instance = axios.create({
  baseURL: _config.apiBaseUrl, // api的base_url
  timeout: process.env.NODE_ENV === 'development' ? 100000 : 10000 // 请求超时时间
  // transformRequest: data => qs.stringify(data)
})
// request拦截器
instance.interceptors.request.use(
  e => {
    e.headers['X-Requested-With'] = 'XMLHttpRequest'

    return e
  },
  error => ({ status: 0, msg: error.message })
)
// respone拦截器
instance.interceptors.response.use(
  response => {
    const resp = response.data
    return resp
  },
  error => {
    console.warn(error)
    const err = { status: 0, msg: '服务器异常' }
    if (
      error.message &&
      (error.message.indexOf('403') > -1 || error.message.indexOf('401') > -1)
    ) {
      //   err.msg = '权限校验失败，请重新登录'
    }
    return Promise.reject(err)
  }
)

export default instance
