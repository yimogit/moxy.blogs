<template>
  <el-row>
    <el-col :span="24">
      <el-form ref="form" :model="form" label-width="200px" v-loading="submit_loading">
        <el-form-item label="文章标题">
          <el-input v-model="form.artTitle"></el-input>
        </el-form-item>
        <el-form-item label="文章分类">
          <el-input v-model="form.categoryId"></el-input>
        </el-form-item>
        <el-form-item label="友好地址名">
          <el-input v-model="form.entryName"></el-input>
        </el-form-item>
        <el-form-item label="文章简介">
          <el-input v-model="form.artDesc" type="textarea"></el-input>
        </el-form-item>
        <el-form-item label="文章标签">
          <el-select v-model="tags" multiple filterable allow-create default-first-option placeholder="请选择文章标签">
          </el-select>
        </el-form-item>
        <el-form-item label="是否发布">
          <el-switch v-model="form.isRelease"></el-switch>
        </el-form-item>
        <el-form-item label="发布时间" v-if="form.isRelease">
          <el-date-picker v-model="form.releaseTime" type="datetime" placeholder="选择发布时间">
          </el-date-picker>
        </el-form-item>
        <el-form-item label="文章内容">
          <v-editor v-model="form.artContent"></v-editor>
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
import VEditor from 'yimo-vue-editor'
export default {
  props: ['id'],
  components: { VEditor },
  data() {
    return {
      form: {},
      tags: [],
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
        .getArticleItem({ id: this.id })
        .then(res => {
          if (res.status === 0) return
          this.submit_loading = false
          this.form = res.data
          this.tags = (res.data.tags || '').split(',')
        })
        .catch(() => {
          this.submit_loading = false
        })
    },
    onSubmit() {
      this.submit_loading = true
      this.form.tags = this.tags.join(',')
      this.$api.cms[this.id ? 'editArticle' : 'createArticle'](this.form)
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
