using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebWMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class addURN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAndRoles_UserInfos_UsersId",
                table: "UserAndRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserAndRoles_UsersId",
                table: "UserAndRoles");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "UserAndRoles");

            migrationBuilder.CreateIndex(
                name: "IX_UserAndRoles_UserId",
                table: "UserAndRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndRoles_UserInfos_UserId",
                table: "UserAndRoles",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAndRoles_UserInfos_UserId",
                table: "UserAndRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserAndRoles_UserId",
                table: "UserAndRoles");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "UserAndRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAndRoles_UsersId",
                table: "UserAndRoles",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndRoles_UserInfos_UsersId",
                table: "UserAndRoles",
                column: "UsersId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
