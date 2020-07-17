using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickMeals.Migrations
{
    public partial class authusersupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "Password", "RoleID" },
                values: new object[] { "", "", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserName",
                keyValue: "");
        }
    }
}
