<template>
  <div>
    <div class="blank"></div>
    <div class="blogs">
      <v-art-top />
    </div>
    <aside>
      <v-category />
      <v-friend />
    </aside>
  </div>
</template>

<script>
import VArtTop from '../components/partial/ArtTopList'
import VCategory from '../components/partial/Category'
import VFriend from '../components/partial/Friend'
export default {
  components: {
    VArtTop,
    VCategory,
    VFriend
  },
  data() {
    return {
      indexList: [],
      categoryList: []
    }
  }
  // async asyncData(context) {
  //   const res = await context.app.$api.cms.getArticleTopList()
  //   const categoryRes = await context.app.$api.cms.getCategoryList()
  //   return { indexList: res.data, categoryList: categoryRes.data }
  // }
}
</script>
