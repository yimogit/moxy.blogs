<template>
    <div class="paihang">
        <h2>友情链接</h2>
        <ul>
            <li v-for="(item,index) in list" :key="index">
                <a :href="item.link" v-text="item.text" target="_blank">
                </a>
            </li>
        </ul>
    </div>
</template>

<script>
export default {
  computed: {
    list() {
      return this.$store.state.friendList
    }
  }
}
</script>

<style>
</style>
