<template>
  <div class="ztbox">
    <ul>
      <li v-for="item in list" :key="item.id">
        <nuxt-link :to="{path:'/category/'+item.categoryName}" :title="formatTitle(item)" v-html="formatTitle(item)"></nuxt-link>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  computed: {
    list() {
      return this.$store.state.categoryList
    }
  },
  methods: {
    formatTitle(item) {
      return `${item.categoryName} ( ${item.totalNum} )`
    }
  }
}
</script>
