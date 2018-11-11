<template>
  <el-row>
    <el-col :span="24">
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
        <el-form-item label="是否超级管理员">
          <el-switch v-model="supportAdmin"></el-switch>
        </el-form-item>
        <el-form-item label="模块权限" v-if="!supportAdmin">
          <el-checkbox-group v-model="checkedModules">
            <template v-for="(modules,key,keyIndex) in moduleDics">
              <template v-for="(item,index) in modules">
                <br v-if="item.isPage&&keyIndex>0" :key="key+'_'+index" />
                <el-checkbox :label="item.moduleCode" :key="item.moduleCode">{{item.moduleName}}</el-checkbox>
              </template>
            </template>
          </el-checkbox-group>
        </el-form-item>
        <el-form-item label="自定义菜单">
          <el-input type="textarea" :rows="5" placeholder="JSON" v-model="form.menus">
          </el-input>

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
      supportAdmin: false,
      form: {},
      submit_loading: false,
      moduleDics: {},
      checkedModules: []
    }
  },
  created() {
    this.loadData()
    this.loadModules()
  },
  methods: {
    loadModules() {
      this.$api.system.getAdminModules().then(res => {
        this.moduleDics = res.data
      })
    },
    loadData() {
      if (!this.id) return
      this.submit_loading = true
      this.$api.system
        .getAdminItem({ id: this.id })
        .then(res => {
          if (res.status === 0) return
          this.submit_loading = false
          this.form = res.data
          this.supportAdmin = res.data.moduleCodes === '*'
          this.checkedModules = res.data.moduleCodes.split(',')
        })
        .catch(() => {
          this.submit_loading = false
        })
    },
    onSubmit() {
      this.submit_loading = true
      if (this.supportAdmin) {
        this.form.moduleCodes = '*'
      } else {
        this.form.moduleCodes = this.checkedModules
          .filter(s => s !== '*')
          .join(',')
      }
      this.$api.system[this.id ? 'editAdmin' : 'createAdmin'](this.form)
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
