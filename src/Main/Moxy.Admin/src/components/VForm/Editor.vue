<template>
  <div>
    <div v-if="isMd"><textarea v-model="currentValue"></textarea></div>
    <yimo-editor v-model="currentValue" :uploadHandler="uploadHandler" v-else></yimo-editor>
  </div>
</template>

<script>
import YimoEditor from 'yimo-vue-editor'
export default {
  name: 'editor',
  components: {
    YimoEditor
  },
  props: {
    value: String,
    isMd: Boolean
  },
  data() {
    return {
      currentValue: this.value
    }
  },
  watch: {
    value(val) {
      this.currentValue = val
    },
    currentValue(val) {
      this.$emit('input', val)
    }
  },
  methods: {
    uploadHandler(type, resTxt) {
      return resTxt
    }
  }
}
</script>
