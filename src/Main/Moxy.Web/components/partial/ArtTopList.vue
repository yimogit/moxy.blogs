<template>
  <div>
    <div class="bloglist" v-for="item in list" :key="item.artTitle">
      <h2>
        <nuxt-link :to="{path:'/detail/'+item.entryName}" :title="item.artTitle" v-html="item.artTitle"></nuxt-link>
      </h2>
      <p v-html="item.artDesc"></p>
    </div>
    <slot></slot>
  </div>
</template>

<script>
export default {
  computed: {
    list() {
      return this.$store.state.artTopList
    }
  }
}
</script>
