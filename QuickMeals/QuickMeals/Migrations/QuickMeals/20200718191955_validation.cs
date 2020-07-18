using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickMeals.Migrations.QuickMeals
{
    public partial class validation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
