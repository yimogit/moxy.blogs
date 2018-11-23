<template>
  <div>
    <el-row>
      <el-col :span="24">
        <el-form ref="form" :rules="rules" :model="form" label-width="200px" v-loading="submit_loading">
          <el-form-item label="新密码" prop="newPassword" class="custom-input-small">
            <el-input v-model="form.newPassword" type="password"></el-input>
          </el-form-item>
          <el-form-item label="确认密码" prop="confirmPassword" class="custom-input-small">
            <el-input v-model="form.confirmPassword" type="password"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit">保存</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </div>
</template>
<script>
export default {
  data() {
    var validatePass = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('请输入确认密码'))
      } else if (value !== this.form.newPassword) {
        callback(new Error('两次输入密码不一致!'))
      } else {
        callback()
      }
    }
    return {
      form: {
        newPassword: '',
        confirmPassword: ''
      },
      rules: {
        newPassword: [
          { required: true, message: '请输入新密码', trigger: 'blur' }
        ],
        confirmPassword: [
          {
            required: true,
            validator: validatePass,
            trigger: 'blur'
          }
        ]
      },
      submit_loading: false
    }
  },
  methods: {
    onSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          console.log(valid)
          return false
        } else {
          this.submit_loading = true
          this.$api.system
            .updatePwd(this.form)
            .then(res => {
              if (res.status === 0) return
              this.$ui.pages.success(res.msg)
              this.submitCallback(res)
            })
            .finally(() => {
              this.submit_loading = false
            })
        }
      })
    },
    submitCallback(r) {
      this.form = {}
      this.$ui.pages
        .confirm('修改密码后需要重新登录，是否现在登录？')
        .then(res => {
          this.$ui.pages.link(this.$codes.login_path)
        })
    }
  }
}
</script>
