<template>
  <el-select v-model="currentValue" v-loading="isloading" :collapse-tags="collapseTags" :allowCreate="allowCreate" :filterable="filterable" clearable @change="change" :multiple="multiple" :placeholder="placeholder" @focus="loadData(true)" ref="currentSelect">
    <el-option v-for="item in options" :key="item.value" :label="item.text" :value="item.value">
    </el-option>
  </el-select>
</template>

<script>
export default {
  props: {
    value: [String, Number, Array],
    multipleSplit: String,
    multiple: Boolean,
    collapseTags: Boolean,
    placeholder: String,
    allowCreate: Boolean,
    filterable: Boolean,
    api: {
      type: Function,
      required: true
    }
    // cacheKey: String
  },
  watch: {
    value: {
      handler(val) {
        if (this.multiple) {
          if (this.multipleSplit) {
            this.currentValue = (val || '')
              .split(this.multipleSplit)
              .filter(s => s.trim() !== '')
          } else {
            this.currentValue = (val || []).map(e => String(e))
          }
        } else {
          this.currentValue = String(val || '')
        }
        if (this.currentValue && this.currentValue.length > 0) {
          this.loadData()
        }
      },
      immediate: true
    }
  },
  data() {
    return {
      options: [],
      currentValue: this.multiple ? [] : null,
      isloading: false
    }
  },
  methods: {
    change(val) {
      if (typeof this.value === 'string' && this.multipleSplit) {
        this.$emit('input', val.join(','))
      } else {
        this.$emit('input', val)
      }
    },
    loadData(autoOpen) {
      if (this.isloading || this.options.length > 0 || !this.api) return
      this.isloading = true
      this.api().then(res => {
        this.options = res.data.map(s => {
          s.value = String(s.value)
          return s
        })
        this.isloading = false
        if (autoOpen) this.$refs.currentSelect.selectOption()
      })
    }
  }
}
</script>
