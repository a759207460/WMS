using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebWMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class upurn04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAndRoles_Roles_RoleId",
                table: "UserAndRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserAndRoles_RoleId",
                table: "UserAndRoles");

            migrationBuilder.AddColumn<int>(
                name: "RoleInfoId",
                table: "UserAndRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAndRoles_RoleInfoId",
                table: "UserAndRoles",
                column: "RoleInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndRoles_Roles_RoleInfoId",
                table: "UserAndRoles",
                column: "RoleInfoId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAndRoles_Roles_RoleInfoId",
                table: "UserAndRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserAndRoles_RoleInfoId",
                table: "UserAndRoles");

            migrationBuilder.DropColumn(
                name: "RoleInfoId",
                table: "UserAndRoles");

            migrationBuilder.CreateIndex(
                name: "IX_UserAndRoles_RoleId",
                table: "UserAndRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndRoles_Roles_RoleId",
                table: "UserAndRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
