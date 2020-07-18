using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickMeals.Migrations.QuickMeals
{
    public partial class depens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Username = table.Column<string>(maxLength: 15, nullable: false),
                    Password = table.Column<string>(maxLength: 15, nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Username);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });
            //added drop table because it already existed
            migrationBuilder.DropTable("Recipes");
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    CookTime = table.Column<int>(nullable: false),
                    Ingredients = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Username1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_Recipes_User_Username1",
                        column: x => x.Username1,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "CookTime", "Description", "Ingredients", "Title", "Username", "Username1" },
                values: new object[] { 1, 30, "1. Coat chicken breasts with onion and garlic powders and herbs. Season generously with salt and pepper. 2. Heat1 Tablespoon of oil and cook chicken breasts. Transfer to plate. 3. Heat oil and saute garlic, parsley, thyme and rosemary. 4. Stir in milk or cream. 5. Bring to a boil. Add cornstarch and stir until thickened. 6. Return chicken to skillet and sprinkle extra herbs if desired.", "Chicken, garlic, oil, parsley, thyme, rosemary and milk", "Creamy Herb Chicken", "", null });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "CookTime", "Description", "Ingredients", "Title", "Username", "Username1" },
                values: new object[] { 2, 15, "1. Heat oil over medium heat. 2. Lightly season the cubed chicken with salt and pepper. 3. Add the chicken to the skillet and brown on one side. 4. Make the glaze. Whisk the honey, soy sauce, garlic, and red pepper flakes in a small bowl. 5. Add the sauce to the pan and toss to coat chicken. Cook until chicken is done. 6. Serve on steamed rice.", "Olive oil, chicken breast, honey, soy sauce, garlic, red pepper flakes, salt and pepper", "Garlic Honey Chicken", "", null });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Username1",
                table: "Recipes",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
