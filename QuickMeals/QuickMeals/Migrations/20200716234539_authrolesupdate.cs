using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickMeals.Migrations
{
    public partial class authrolesupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 0,
                column: "RoleName",
                value: "Anonymous");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: 0,
                column: "RoleName",
                value: "Annonymous");
        }
    }
}
