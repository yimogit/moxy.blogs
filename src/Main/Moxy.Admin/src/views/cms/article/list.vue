<template>
  <div>
    <el-row>
      <el-col :span="18">
        <el-form :inline="true">
          <el-form-item label="文章标题">
            <el-input type="text" v-model="search.keyword" clearable>
              <el-button slot="append" icon="el-icon-search" @click="e=>this.$refs.mytable.search()"></el-button>
            </el-input>
          </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="6" class="text-right">
        <v-btn-create @click="$ui.pages.link({name:'cms_article_create'})" auth="cms_article_create">添加文章</v-btn-create>
      </el-col>
    </el-row>
    <v-table-pager :loadAction="$api.cms.getArticleList" :loadSearch="search" ref="mytable" :show-checkbox="true" :hide-pager="false" @handle-checkbox="e=>checkList=e">
      <el-table-column prop="artTitle" label="文章标题">
      </el-table-column>
      <el-table-column label="是否发布">
        <template slot-scope="prop">
          <el-tag :type="prop.row.isRelease?'success':'info'">{{prop.row.isRelease?'是':'否'}}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="createdAt" label="创建时间">
      </el-table-column>
      <el-table-column width="180" label="操作">
        <template slot-scope="prop">
          <v-btn-edit @click="$ui.pages.link('/cms/article/edit/'+prop.row.id)" auth="cms_article_edit" icon="el-icon-document">编辑</v-btn-edit>
          <v-btn-del @click="delArticle(prop.row.id)" auth="cms_article_delete">删除</v-btn-del>
        </template>
      </el-table-column>
    </v-table-pager>

    <el-dialog width="80%" :title="editDialog.title" v-if="editDialog.show" :visible.sync="editDialog.show" :close-on-click-modal="false">
      <v-article-edit @submit="submitCallback" :id="editDialog.editId" />
    </el-dialog>
  </div>
</template>

<script>
import VArticleEdit from './components/Edit'
export default {
  components: {
    VArticleEdit
  },
  data() {
    return {
      search: {
        keyword: ''
      },
      list: [],
      editDialog: {
        title: '',
        editId: null,
        show: false
      },
      checkList: []
    }
  },
  methods: {
    showDialog(row) {
      this.editDialog.title = '文章' + (row.id > 0 ? '编辑' : '创建')
      this.editDialog.editId = row.id
      this.editDialog.show = true
    },
    submitCallback(result) {
      this.editDialog.show = false
      if (!result) return
      this.$refs.mytable.loadData()
    },
    delArticle(id) {
      this.$ui.pages.confirm('确认删除？').then(res => {
        this.$api.cms.delArticle({ ids: [id] }).then(res => {
          if (res.status !== 1) return
          this.$ui.pages.success(res.msg)
          this.$refs.mytable.loadData()
        })
      })
    }
  }
}
</script>
