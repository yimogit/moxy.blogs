<template>
  <div>
    <v-article-edit @submit="e=>{$store.dispatch('removeNavTab',this.$route.name);$router.back()}" :id="$route.params.id" />
  </div>
</template>

<script>
import VArticleEdit from './components/Edit'
export default {
  components: {
    VArticleEdit
  },
  data() {
    return {}
  },
  beforeCreate() {},
  methods: {
    handleClick(tab, event) {
      console.log(tab, event)
    }
  }
}
</script>
