import fetch from '@/api/fetch'

export function getAppInfo() {
  return fetch.get('/v1/config/pc')
}
