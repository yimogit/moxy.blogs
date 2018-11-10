<template>
  <el-row>
    <el-col :span="20">
      <el-form ref="form" :model="form" label-width="200px" v-loading="submit_loading">
        <el-form-item label="管理员名称">
          <el-input v-model="form.adminName"></el-input>
        </el-form-item>
        <el-form-item label="管理员密码">
          <el-input v-model="form.adminPwd" :placeholder="form.id?'为空则不修改':''"></el-input>
        </el-form-item>
        <el-form-item label="是否启用">
          <el-switch v-model="form.isEnable"></el-switch>
        </el-form-item>
        <el-form-item label="模块编码">
          <el-input multiple placeholder="模块编码" v-model="form.moduleCodes"></el-input>
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
      this.$api.system
        .getAdminItem({ id: this.id })
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
      this.$api.system[this.id ? 'editAdmin' : 'createAdmin'](this.form)
        .then(res => {
          if (res.status === 0) return
          this.submit_loading = false
          this.$ui.pages.success(res.msg)
          this.submitCallback(res)
        })
        .catch(() => {
          this.submit_loading = false
        })
    },
    submitCallback(r) {
      this.$emit('submit', r)
    }
  }
}
</script>
