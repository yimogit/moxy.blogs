<template>
  <div>
    <div class="place">
      <nuxt-link :to="{path:'/category/'+item.categoryName}" v-for="item in $store.state.categoryList" :class="{'active':categoryName==item.categoryName}" :key="item.id" v-html="item.categoryName"></nuxt-link>
    </div>
    <v-art-list class="blogs" :list="list">
      <div class="pagelist">
        <nuxt-link :to="{path:'/category/'+categoryName,query:{page:nextPageIndex}}" v-if="nextPageIndex>0">下一页</nuxt-link>
        <a href="javascript:void(0)" v-else>已全部加载完毕</a>
      </div>
    </v-art-list>
    <aside>
      <v-category></v-category>
      <v-friend></v-friend>
    </aside>
  </div>
</template>

<script>
import VArtList from '@/components/partial/ArtList'
import VCategory from '@/components/partial/Category'
import VFriend from '@/components/partial/Friend'
export default {
  components: {
    VCategory,
    VFriend,
    VArtList
  },
  data() {
    return {
      categoryName: '',
      list: [],
      nextPageIndex: 0
    }
  },
  watchQuery: ['id', 'page'],
  async asyncData(context) {
    var categoryName = context.params.id
    var pageIndex = Math.max(context.query.page || 0, 0)
    var pageSize = 10
    var artRes = await context.app.$api.cms.getCategoryArtList({
      categoryName,
      pageIndex,
      pageSize
    })
    return {
      categoryName: categoryName,
      list: artRes.data.items,
      hasNext: artRes.data.hasNextPage,
      nextPageIndex: artRes.data.hasNextPage ? pageIndex + 1 : 0
    }
  },
  methods: {}
}
</script>

<style>
</style>
