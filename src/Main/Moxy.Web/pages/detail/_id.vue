<template>
  <div>
    <div class="blank"></div>
    <div class="infosbox">
      <div class="newsview">
        <h3 class="news_title">{{model.artTitle}}</h3>
        <div class="bloginfo" v-if="model.releaseTime">
          <ul>
            <li class="author" v-if="model.artAuthor"><a href="/">{{model.artAuthor}}</a></li>
            <li class="lmname" v-if="model.categoryName"><a href="/">{{model.categoryName}}</a></li>
            <li class="timer">{{model.releaseTime}}</li>
            <li class="view">阅读数(<span id="busuanzi_value_page_pv">...</span>)</li>
          </ul>
        </div>
        <div class="tags">
          <nuxt-link :to="'/tags/'+item" v-for="(item,index) in model.tagList" :key="'tag_'+index">{{item}}</nuxt-link>
        </div>
        <div class="news_about" v-if="model.artDesc" v-html="model.artDesc"></div>
        <div class="news_con markdown-body" v-html="model.artContent">
        </div>
        <!-- <p class="diggit"><a href="javascript:void(0)">赞<b id="diggnum">(0)</b> </a></p> -->
      </div>
    </div>
    <aside>
      <v-category></v-category>
      <v-friend></v-friend>
    </aside>
    <div class="blank"></div>
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
      title: this.model.artTitle + ' - ' + this.$store.state.appInfo.siteTitle
    }
  },
  data() {
    return {
      model: {}
    }
  },
  async asyncData(context) {
    var artRes = await context.app.$api.cms.getArticleDetail(context.params.id)
    return { model: artRes.data }
  },
  mounted() {
    this.$utils.dynamicLoadJs(
      '//busuanzi.ibruce.info/busuanzi/2.3/busuanzi.pure.mini.js'
    )
  }
}
</script>

<style>
</style>
