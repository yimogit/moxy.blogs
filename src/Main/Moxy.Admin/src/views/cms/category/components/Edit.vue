<template>
  <el-row>
    <el-col :span="24">
      <el-form ref="form" :model="form" label-width="200px" v-loading="submit_loading">
        <el-form-item label="文章分类名称">
          <el-input v-model="form.categoryName"></el-input>
        </el-form-item>
        <el-form-item label="文章分类描述">
          <el-input v-model="form.categoryDesc" type="textarea"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit">保存</el-button>
          <el-button @click="submitCallback(false)">取消</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </el-row>
</template>
<script>
export default {
  props: ['id'],
  data() {
    return {
      form: {},
      submit_loading: false
    }
  },
  created() {
    this.loadData()
  },
  methods: {
    loadData() {
      if (!this.id) return
      this.submit_loading = true
      this.$api.cms
        .getCategoryItem({ id: this.id })
        .then(res => {
          if (res.status === 0) return
          this.submit_loading = false
          this.form = res.data
        })
        .catch(() => {
          this.submit_loading = false
        })
    },
    onSubmit() {
      this.submit_loading = true
      this.$api.cms[this.id ? 'editCategory' : 'createCategory'](this.form)
        .then(res => {
          if (res.status === 0) return
          this.$ui.pages.success(res.msg)
          this.submitCallback(res)
        })
        .finally(() => {
          this.submit_loading = false
        })
    },
    submitCallback(r) {
      this.$emit('submit', r)
    }
  }
}
</script>
