import Vue from 'vue'
import $api from '../api'
import * as $utils from '../utils'

// import components from '../components'

// Object.keys(components).forEach(e => Vue.component(e, components[e]))
Vue.prototype.$api = $api
Vue.prototype.$utils = $utils
