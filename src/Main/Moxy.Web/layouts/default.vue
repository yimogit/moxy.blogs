<template>
  <div>
    <header id="layout-header">
      <div class="logo"><a href="/">{{appInfo.siteName}}</a></div>
      <nav>
        <ul id="starlist">
          <li v-for="(item,index) in appInfo.menus" :key="index" :class="{'active':fullPath===item.menuUrl}">
            <a :href="item.menuUrl" v-if="item.menuUrl&&item.menuUrl.startsWith('http')">{{item.menuName}}</a>
            <nuxt-link :to="item.menuUrl" v-else>{{item.menuName}}</nuxt-link>
          </li>
        </ul>
      </nav>
    </header>
    <div id="layout-container">
      <nuxt class="box" />
    </div>
    <footer v-html="appInfo.footer" id="layout-footer" :class="{'layout-footer-block':!computedEnd}">
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
    return {
      computedEnd: false
    }
  },
  computed: {
    appInfo() {
      return this.$store.state.appInfo
    },
    fullPath() {
      return this.$route.fullPath
    }
  },
  mounted() {
    this.computedFooter()
  },
  methods: {
    computedFooter() {
      this.$nextTick(() => {
        var bodyMinHeight =
          window.innerHeight -
          document.getElementById('layout-footer').offsetHeight -
          document.getElementById('layout-header').offsetHeight
        document.getElementById('layout-container').style.minHeight =
          bodyMinHeight + 'px'
        this.computedEnd = true
      })
    }
  }
}
</script>
<style>
.layout-footer-fixed {
  position: absolute;
  bottom: 0;
}
#layout-footer {
  transition: opacity 0.3s ease-in;
  -webkt-transition: opacity 0.3s ease-in;
}
.layout-footer-block {
  opacity: 0;
}
</style>
