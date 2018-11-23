<template>
  <div>
    <el-row>
      <el-col :span="18">
        <el-form :inline="true" @submit.native.prevent>
          <el-form-item label="文章分类名称">
            <el-input type="text" v-model="search.keyword" clearable>
              <el-button slot="append" icon="el-icon-search" @click="e=>this.$refs.mytable.search()"></el-button>
            </el-input>
          </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="6" class="text-right">
        <v-btn-create @click="showDialog({})" auth="cms_category_create">添加文章分类</v-btn-create>
      </el-col>
    </el-row>
    <v-table-pager :loadAction="$api.cms.getCategoryList" :loadSearch="search" ref="mytable" show-checkbox :hide-pager="false" @handle-checkbox="e=>checkList=e">
      <el-table-column prop="categoryName" label="文章分类名称">
      </el-table-column>
      <el-table-column prop="createdAt" label="创建时间">
      </el-table-column>
      <el-table-column width="180" label="操作">
        <template slot-scope="prop">
          <v-btn-edit @click="showDialog(prop.row)" auth="cms_category_edit" icon="el-icon-document">编辑</v-btn-edit>
          <v-btn-del @click="delCategory([prop.row.id])" auth="cms_category_delete">删除</v-btn-del>
        </template>
      </el-table-column>
    </v-table-pager>

    <el-dialog width="60%" :title="editDialog.title" v-if="editDialog.show" :visible.sync="editDialog.show" :close-on-click-modal="false">
      <v-category-edit @submit="submitCallback" :id="editDialog.editId" />
    </el-dialog>
  </div>
</template>

<script>
import VCategoryEdit from './components/Edit'
export default {
  components: {
    VCategoryEdit
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
      this.editDialog.title = '文章分类' + (row.id > 0 ? '编辑' : '创建')
      this.editDialog.editId = row.id
      this.editDialog.show = true
    },
    submitCallback(result) {
      this.editDialog.show = false
      if (!result) return
      this.$refs.mytable.showLoading()
      this.$refs.mytable.loadData()
    },
    delCategory(ids) {
      this.$ui.pages.confirm('确认删除？').then(res => {
        this.$refs.mytable.showLoading()
        this.$api.cms
          .delCategory({ ids: ids })
          .then(res => {
            if (res.status !== 1) return
            this.$ui.pages.success(res.msg)
          })
          .finally(_ => {
            this.$refs.mytable.loadData()
          })
      })
    }
  }
}
</script>
