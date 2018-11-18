<template>
  <div>
    <header>
      <div class="logo"><a href="/">{{$store.state.appInfo.siteName}}</a></div>
      <nav>
        <ul id="starlist">
          <li v-for="(item,index) in $store.state.appInfo.menus" :key="index">
            <a :href="item.menuUrl" v-if="item.menuUrl&&item.menuUrl.startsWith('http')">{{item.menuName}}</a>
            <nuxt-link :to="item.menuUrl" v-else>{{item.menuName}}</nuxt-link>
          </li>
        </ul>
      </nav>
    </header>
    <nuxt class="box" />
    <footer v-html="$store.state.appInfo.footer">
    </footer>
  </div>
</template>
<script>
export default {
  data() {
    return {}
  }
}
</script>
