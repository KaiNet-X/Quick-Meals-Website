using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickMeals.Migrations.QuickMeals
{
    public partial class fileuploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Recipes");
        }
    }
}
