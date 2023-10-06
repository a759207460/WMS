using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebWMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class upurn02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleInfoUserAndRoles_Roles_RoleId",
                table: "RoleInfoUserAndRoles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RoleInfoUserAndRoles",
                newName: "RoleInfosId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleInfoUserAndRoles_Roles_RoleInfosId",
                table: "RoleInfoUserAndRoles",
                column: "RoleInfosId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleInfoUserAndRoles_Roles_RoleInfosId",
                table: "RoleInfoUserAndRoles");

            migrationBuilder.RenameColumn(
                name: "RoleInfosId",
                table: "RoleInfoUserAndRoles",
                newName: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleInfoUserAndRoles_Roles_RoleId",
                table: "RoleInfoUserAndRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
