<template>
  <div>
    <header>
      <div class="logo"><a href="/">{{appInfo.siteName}}</a></div>
      <nav>
        <ul id="starlist">
          <li v-for="(item,index) in appInfo.menus" :key="index">
            <a :href="item.menuUrl" v-if="item.menuUrl&&item.menuUrl.startsWith('http')">{{item.menuName}}</a>
            <nuxt-link :to="item.menuUrl" v-else>{{item.menuName}}</nuxt-link>
          </li>
        </ul>
      </nav>
    </header>
    <nuxt class="box" />
    <footer v-html="appInfo.footer">
    </footer>
  </div>
</template>
<script>
export default {
  head() {
    return {
      title: this.appInfo.siteTitle,
      meta: [
        {
          hid: 'keywords',
          name: 'keywords',
          content: this.appInfo.siteKeywords
        },
        {
          hid: 'description',
          name: 'description',
          content: this.appInfo.siteDescription
        }
      ]
    }
  },
  data() {
    return {}
  },
  computed: {
    appInfo() {
      return this.$store.state.appInfo
    }
  }
}
</script>
