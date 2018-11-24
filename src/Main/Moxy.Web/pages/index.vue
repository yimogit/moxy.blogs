<template>
  <div>
    <div class="blank"></div>
    <div class="blogs">
      <v-art-top></v-art-top>
    </div>
    <aside>
      <v-category />
      <v-friend />
    </aside>
  </div>
</template>

<script>
import VArtTop from '@/components/partial/ArtTopList'
import VCategory from '@/components/partial/Category'
import VFriend from '@/components/partial/Friend'
export default {
  components: {
    VArtTop,
    VCategory,
    VFriend
  },
  head() {
    return {
      title: '首页 - ' + this.$store.state.appInfo.siteTitle
    }
  },
  data() {
    return {}
  }
}
</script>
