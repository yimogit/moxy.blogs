## 注意

- 手动创建数据并设置编码为：`utf8mb4`   ！！CodeFirst创建的数据字符编不一样
- 连接字符串示例：`server=127.0.0.1;uid=root;pwd=123456;database=moxy_blogs_db`   
- 查看数据库编码：`show variables like 'character_set_database';`  
- 修改数控编码:`alter database moxy_blogs_db character set utf8mb4;`
- [Pomelo.EntityFrameworkCore.MySql 组件仓库](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)  

## 迁移命令

- 在当前项目中启用数据迁移： `Enable-Migrations`

- 添加迁移版本： `Add-Migration 版本名称`

- 更新数据库： `Update-Database`

