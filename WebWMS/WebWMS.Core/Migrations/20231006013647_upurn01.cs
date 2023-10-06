using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebWMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class upurn01 : Migration
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

            migrationBuilder.CreateTable(
                name: "RoleInfoUserAndRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfoUserAndRoles", x => new { x.RoleId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleInfoUserAndRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleInfoUserAndRoles_UserAndRoles_UsersId",
                        column: x => x.UsersId,
                        principalTable: "UserAndRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleInfoUserAndRoles_UsersId",
                table: "RoleInfoUserAndRoles",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleInfoUserAndRoles");

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
