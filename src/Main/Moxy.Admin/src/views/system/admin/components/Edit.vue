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
        <el-form-item label="权限设置">
          <el-switch v-model="supportAdmin" active-text="超级管理员" inactive-text="权限分配"></el-switch>
          <el-checkbox-group v-model="checkedModules" v-if="!supportAdmin">
            <template v-for="(modules,key,keyIndex) in moduleDics">
              <template v-for="(item,index) in modules">
                <br v-if="item.isPage&&keyIndex>0" :key="key+'_'+index" />
                <el-checkbox class="module_checkbox" @change="e=>changeModule(e,item)" :label="item.moduleCode" :key="item.moduleCode">{{item.moduleName}}</el-checkbox>
              </template>
            </template>
          </el-checkbox-group>
        </el-form-item>
        <el-form-item label="菜单设置">
          <el-switch v-model="menusModel.editJson" active-text="JSON编辑" inactive-text="可视化编辑"></el-switch>
          <el-input type="textarea" :rows="5" placeholder="JSON" v-model="form.menus" v-if="menusModel.editJson">
          </el-input>
          <div v-else>
            <v-table-tree :data="menusModel.tableData" v-if="menusModel.tableData" expand-all ref="mytreetable" :columns="menusModel.columns">
              <el-table-column prop="menuCode" label="菜单编码">
              </el-table-column>
              <el-table-column prop="menuSort" label="菜单排序">
              </el-table-column>
              <el-table-column prop="menuUrl" label="菜单路径">
              </el-table-column>
              <el-table-column prop="menuIcon" label="菜单图标">
              </el-table-column>
              <el-table-column label="操作" :render-header="renderHeader" width="200">
                <template slot-scope="prop">
                  <v-btn-create text="" @click="addMenuItem(prop.row.menuId)">
                  </v-btn-create>
                  <v-btn-edit text="" @click="editMenuItem(prop.row)"></v-btn-edit>
                  <v-btn-del text="" @click="removeMenuItem(prop.row)"></v-btn-del>
                </template>
              </el-table-column>
            </v-table-tree>
          </div>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit">保存</el-button>
          <el-button @click="submitCallback(false)">取消</el-button>
        </el-form-item>
      </el-form>

      <el-dialog :title="menusModel.editModel.menuId>0?'菜单编辑':'添加菜单'" append-to-body :visible.sync="menusModel.editDialog">
        <el-form :model="menusModel.editModel" label-width="100px">
          <el-form-item label="菜单名称">
            <el-input v-model="menusModel.editModel.menuName"></el-input>
          </el-form-item>
          <el-form-item label="菜单编码">
            <el-input v-model="menusModel.editModel.menuCode"></el-input>
          </el-form-item>
          <el-form-item label="菜单排序">
            <el-input v-model="menusModel.editModel.menuSort"></el-input>
          </el-form-item>
          <el-form-item label="菜单路径">
            <el-input v-model="menusModel.editModel.menuUrl"></el-input>
          </el-form-item>
          <el-form-item label="菜单图标">
            <el-input v-model="menusModel.editModel.menuIcon"></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="menusModel.editDialog = false">取 消</el-button>
          <el-button type="primary" @click="dialogSubmit">确 定</el-button>
        </div>
      </el-dialog>
    </el-col>
  </el-row>
</template>
<script>
export default {
  props: ['id'],
  data() {
    return {
      supportAdmin: false,
      menusModel: {
        editJson: true,
        editDialog: false,
        columns: [
          {
            text: '菜单名称',
            value: 'menuName',
            width: 200
          }
        ],
        tableData: null,
        editModel: {}
      },
      form: {},
      submit_loading: false,
      moduleDics: {},
      checkedModules: []
    }
  },
  watch: {
    'menusModel.editJson': {
      handler(val) {
        if (!val) {
          this.menusModel.tableData = JSON.parse(this.form.menus || '[]')
        }
      }
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
          this.menusModel.editJson = false
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
    },
    changeModule(val, item) {
      if (val && item.isPage) {
        //添加一级菜单
        this.changeItem(null, items => {
          if (items.find(s => s.menuCode === item.moduleCode)) return true
          var nItem = {}
          nItem.menuId = Date.now()
          nItem.menuName = item.moduleName
          nItem.menuCode = item.moduleCode
          items.push(nItem)
          return true
        })
      }
      // console.log(this.menusModel.tableData)
    },
    renderHeader(createElement, { column, $index }) {
      return createElement('v-btn-create', {
        attrs: { text: '' },
        on: {
          click: s => {
            this.addMenuItem()
          }
        }
      })
    },
    addMenuItem(parentId) {
      this.menusModel.editModel = { parentId: parentId }
      this.menusModel.editDialog = true
    },
    editMenuItem(row) {
      this.menusModel.editModel = Object.assign({}, row)
      this.menusModel.editDialog = true
    },
    changeItem(menuId, checkNext) {
      var oldItems = JSON.parse(this.form.menus || '[]')
      var newItems = []
      if (!menuId) checkNext(oldItems)
      else update(oldItems, newItems)
      this.menusModel.tableData = sort(menuId ? newItems : oldItems)
      this.form.menus = JSON.stringify(this.menusModel.tableData)
      function update(items, nItems) {
        items.forEach(item => {
          var nItem = Object.assign({}, item, { children: [] })
          if (item.menuId === menuId && !checkNext(nItem)) {
            return
          }
          if (item.children && item.children.length > 0) {
            update(item.children, nItem.children)
          }
          nItems.push(nItem)
        })
      }
      //降序 menuSort
      function sort(items) {
        items.forEach(item => {
          if (item.children && item.children.length > 0) {
            item.children = sort(item.children)
          }
        })
        return items.sort(compareDown('menuSort'))
      }
      function compareDown(propertyName) {
        return function(obj1, obj2) {
          var value1 = Number(obj1[propertyName]) || 0
          var value2 = Number(obj2[propertyName]) || 0
          return value2 - value1
        }
      }
    },
    dialogSubmit() {
      var asyncKeys = [
        'menuName',
        'menuCode',
        'menuIcon',
        'menuUrl',
        'menuSort'
      ]
      var model = this.menusModel.editModel
      if (model.menuId) {
        //修改菜单
        this.changeItem(model.menuId, item => {
          asyncKeys.forEach(e => {
            item[e] = model[e]
          })
          return true
        })
      } else if (model.parentId) {
        //添加一级子菜单
        this.changeItem(model.parentId, item => {
          var nItem = {}
          nItem.menuId = Date.now()
          asyncKeys.forEach(e => {
            nItem[e] = model[e]
          })
          item.children.push(nItem)
          return true
        })
      } else if (!model.parentId && !model.menuId) {
        //添加一级菜单
        this.changeItem(null, items => {
          var nItem = {}
          nItem.menuId = Date.now()
          asyncKeys.forEach(e => {
            nItem[e] = model[e]
          })
          items.push(nItem)
          return true
        })
      }
      this.menusModel.editDialog = false
    },
    removeMenuItem(row) {
      this.changeItem(row.menuId, item => {
        return false
      })
    }
  }
}
</script>
<style>
.module_checkbox {
  min-width: 120px;
}
</style>
