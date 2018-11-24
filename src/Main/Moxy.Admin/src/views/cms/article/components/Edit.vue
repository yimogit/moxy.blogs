<template>
  <el-row>
    <el-col :span="24">
      <el-form ref="form" :model="form" label-width="200px" v-loading="submit_loading">
        <el-form-item label="文章标题" class="custom-input-small">
          <el-input v-model="form.artTitle"></el-input>
        </el-form-item>
        <el-form-item label="访问地址" class="custom-input-small">
          <el-input v-model="form.entryName"></el-input>
        </el-form-item>
        <el-form-item label="文章分类">
          <v-form-select :api="$api.cms.getCategoryOptions" v-model="form.categoryId" placeholder="请选择文章分类"></v-form-select>
        </el-form-item>
        <el-form-item label="文章简介" class="custom-input-small">
          <el-input v-model="form.artDesc" type="textarea"></el-input>
        </el-form-item>
        <el-form-item label="文章标签">
          <v-form-select multiple allow-create filterable :api="$api.cms.getTagsOptions" multipleSplit="," v-model="form.tags" placeholder="请选择文章标签"></v-form-select>
        </el-form-item>
        <el-form-item label="是否发布">
          <el-switch v-model="form.isRelease"></el-switch>
        </el-form-item>
        <el-form-item label="发布时间" v-if="form.isRelease">
          <el-date-picker v-model="form.releaseTime" type="datetime" placeholder="选择发布时间">
          </el-date-picker>
        </el-form-item>
        <el-form-item label="文章内容" class="custom-full">
          <v-form-editor v-model="form.artContent"></v-form-editor>
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
      form: {
        tags: ''
      },
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
        })
        .catch(() => {
          this.submit_loading = false
        })
    },
    onSubmit() {
      this.submit_loading = true
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
