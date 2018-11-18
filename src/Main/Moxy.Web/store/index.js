import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

const store = () =>
  new Vuex.Store({
    state: {
      apiBaseUrl: 'http://localhost:64832/api',
      appInfo: {}
    },
    mutations: {
      setAppInfo(state, data) {
        state.appInfo = data.data
      }
    },
    actions: {
      async nuxtServerInit({ state, commit }, { req }) {
        const res = await axios.get(state.apiBaseUrl + '/v1/config/pc')
        commit('setAppInfo', res.data)
      }
    }
  })

export default store
