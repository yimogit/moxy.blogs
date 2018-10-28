import { getInfo } from '@/api/modules/account'
const beforeEach = (to, from, next) => {
  if (!to.meta.auth) {
    return next()
  }
  if (!localStorage.token) {
    return next('/login')
  }
  //测试登录即可
  // if (to.path.indexOf('/test') === 0) {
  //   return next()
  // }
  if (checkAuth(to.name)) return next()
  getInfo().then(res => {
    window.authInfo = {
      info: res.data.info,
      menus: res.data.menus,
      modules: res.data.modules
    }
    if (checkAuth(to.name)) {
      next()
    } else {
      window.__currentApp.$ui.pages.info('无访问权限')
      if (history.length < 2) next('/login')
    }
  })
}
const afterEach = (to, from) => {}

function checkAuth(code) {
  return (
    window.authInfo &&
    window.authInfo.modules &&
    window.authInfo.modules.indexOf(code) > -1
  )
}

export default {
  beforeEach,
  afterEach
}
