import fetch from '@/api/fetch'

export function getFriendList() {
  return fetch.get('/v1/common/friend/list')
}
