<template>
    <div v-if="list&&list.length>0">
        <div class="bloglist" v-for="item in list" :key="item.id">
            <h2><a :href="'/detail/'+item.entryName" :title="item.artTitle" v-html="item.artTitle"></a></h2>
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
