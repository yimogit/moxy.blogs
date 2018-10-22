using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moxy.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cms_article",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<string>(maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<string>(maxLength: 100, nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    deleted_by = table.Column<string>(maxLength: 100, nullable: true),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    display_code = table.Column<string>(maxLength: 100, nullable: true),
                    art_title = table.Column<string>(maxLength: 100, nullable: true),
                    art_content = table.Column<string>(nullable: true),
                    art_desc = table.Column<string>(maxLength: 300, nullable: true),
                    tags = table.Column<string>(maxLength: 300, nullable: true),
                    is_release = table.Column<bool>(nullable: false),
                    release_time = table.Column<DateTime>(nullable: true),
                    category_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_article", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cms_category",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<string>(maxLength: 100, nullable: true),
                    category_name = table.Column<string>(maxLength: 100, nullable: true),
                    category_desc = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_admin",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<string>(maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(nullable: true),
                    updated_by = table.Column<string>(maxLength: 100, nullable: true),
                    is_deleted = table.Column<bool>(nullable: true),
                    deleted_by = table.Column<string>(maxLength: 100, nullable: true),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    admin_name = table.Column<string>(maxLength: 100, nullable: true),
                    admin_pwd = table.Column<string>(maxLength: 100, nullable: true),
                    admin_key = table.Column<string>(maxLength: 100, nullable: true),
                    is_enable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_admin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_config",
                columns: table => new
                {
                    code = table.Column<string>(maxLength: 200, nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_config", x => x.code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cms_article");

            migrationBuilder.DropTable(
                name: "cms_category");

            migrationBuilder.DropTable(
                name: "sys_admin");

            migrationBuilder.DropTable(
                name: "sys_config");
        }
    }
}
