import Vue from 'vue'
import Vuex from 'vuex'
import $api from '@/api'

Vue.use(Vuex)

const store = () =>
  new Vuex.Store({
    state: {
      appInfo: {},
      artTopList: [],
      categoryList: [],
      friendList: []
    },
    mutations: {
      setAppInfo(state, data) {
        state.appInfo = data.appInfo
        state.artTopList = data.artTopList
        state.categoryList = data.categoryList
        state.friendList = data.friendList
      }
    },
    actions: {
      async nuxtServerInit({ state, commit }, { req }) {
        // 获取全局信息
        const appRes = await $api.config.getAppInfo()
        const artTopRes = await $api.cms.getArticleTopList()
        const categoryRes = await $api.cms.getCategoryList()
        const friendRes = await $api.common.getFriendList()
        commit('setAppInfo', {
          appInfo: appRes.data,
          artTopList: artTopRes.data.items,
          categoryList: categoryRes.data,
          friendList: friendRes.data
        })
      }
    }
  })

export default store
