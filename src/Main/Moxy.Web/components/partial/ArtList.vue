<template>
  <div v-if="list&&list.length>0">
    <div class="bloglist" v-for="item in list" :key="item.id">
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
  props: {
    list: Array
  }
}
</script>
