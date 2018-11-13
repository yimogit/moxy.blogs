<template>
  <div>
    <v-category-edit @submit="e=>{$store.dispatch('removeNavTab',this.$route.name);$router.back()}" :id="$route.params.id" />
  </div>
</template>

<script>
import VCategoryEdit from './components/Edit'
export default {
  components: {
    VCategoryEdit
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
