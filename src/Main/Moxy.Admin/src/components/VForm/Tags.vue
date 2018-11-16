<template>
    <el-select v-model="currentTags" @change="changeHnadler" multiple filterable allow-create default-first-option placeholder="请选择文章标签">
    </el-select>
</template>

<script>
export default {
  props: {
    value: [String, Array]
  },
  data() {
    return {
      currentTags: []
    }
  },
  watch: {
    value: {
      handler(val) {
        var currentTags = []
        if (typeof val === 'string') {
          currentTags = (val || '').split(',').filter(s => s.trim() !== '')
        } else if (Array.isArray(val)) {
          currentTags = val.filter(s => s.trim() !== '')
        }
        if (JSON.stringify(currentTags) !== JSON.stringify(this.currentTags)) {
          this.currentTags = currentTags
        }
      },
      immediate: true
    }
  },
  methods: {
    changeHnadler(tags) {
      if (typeof this.value === 'string') {
        this.$emit('input', tags.join(','))
      } else {
        this.$emit('input', tags)
      }
    }
  }
}
</script>

<style>
</style>
