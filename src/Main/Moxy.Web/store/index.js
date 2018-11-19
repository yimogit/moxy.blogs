import Vue from 'vue'
import Vuex from 'vuex'
import $api from '@/api'

Vue.use(Vuex)

const store = () =>
  new Vuex.Store({
    state: {
      appInfo: {}
    },
    mutations: {
      setAppInfo(state, data) {
        state.appInfo = data
      }
    },
    actions: {
      async nuxtServerInit({ state, commit }, { req }) {
        // 获取全局信息
        const res = await $api.common.getAppInfo()
        commit('setAppInfo', res.data)
      }
    }
  })

export default store
