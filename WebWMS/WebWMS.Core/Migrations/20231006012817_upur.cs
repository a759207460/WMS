using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebWMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class upur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserInfos_UserInfoId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserInfoId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserInfoId",
                table: "Roles",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserInfos_UserInfoId",
                table: "Roles",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id");
        }
    }
}
