using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickMeals.Migrations
{
    public partial class auth2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleID1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleID1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleID1",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "RoleID",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleID",
                table: "Users",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleID",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "RoleID",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RoleID1",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID1",
                table: "Users",
                column: "RoleID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleID1",
                table: "Users",
                column: "RoleID1",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
