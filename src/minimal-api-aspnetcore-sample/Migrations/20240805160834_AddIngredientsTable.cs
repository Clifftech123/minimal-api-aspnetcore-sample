using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace minimal_api_aspnetcore_sample.Migrations
{
    /// <inheritdoc />
    public partial class AddIngredientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "recipe");

            migrationBuilder.CreateTable(
                name: "Recipes",
                schema: "recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                schema: "recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                schema: "recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "recipe",
                table: "Recipes",
                columns: new[] { "Id", "Category", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Italian", "A classic Italian dish made with breaded chicken, marinara sauce, and mozzarella cheese", "Chicken Parmesan" },
                    { 2, "Italian", "A creamy pasta dish made with fettuccine, chicken, and Alfredo sauce", "Chicken Alfredo" },
                    { 3, "Italian", "A savory chicken dish made with Marsala wine, mushrooms, and garlic", "Chicken Marsala" },
                    { 4, "Italian", "A tangy chicken dish made with lemon, capers, and white wine", "Chicken Piccata" },
                    { 5, "Italian", "A hearty chicken stew made with tomatoes, onions, and bell peppers", "Chicken Cacciatore" },
                    { 6, "Italian", "A creamy chicken dish made with spinach, mushrooms, and Parmesan cheese", "Chicken Florentine" },
                    { 7, "Italian", "A rich pasta dish made with chicken, mushrooms, and a creamy sauce", "Chicken Tetrazzini" },
                    { 8, "Italian", "A flavorful chicken dish made with lemon, capers, and white wine", "Chicken Scallopini" },
                    { 9, "Italian", "A delicious chicken dish made with prosciutto, sage, and white wine", "Chicken Saltimbocca" },
                    { 10, "Italian", "A light and tangy chicken dish made with lemon, butter, and white wine", "Chicken Francese" }
                });

            migrationBuilder.InsertData(
                schema: "recipe",
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, "Salt", "1 tsp", 1 },
                    { 2, "Pepper", "1 tsp", 1 },
                    { 3, "Olive Oil", "1 tbsp", 1 },
                    { 4, "Chicken Breast", "1 lb", 1 },
                    { 5, "Salt", "1 tsp", 2 },
                    { 6, "Pepper", "1 tsp", 2 },
                    { 7, "Olive Oil", "1 tbsp", 2 },
                    { 8, "Chicken Breast", "1 lb", 2 },
                    { 9, "Salt", "1 tsp", 3 },
                    { 10, "Pepper", "1 tsp", 3 },
                    { 11, "Olive Oil", "1 tbsp", 3 },
                    { 12, "Chicken Breast", "1 lb", 3 }
                });

            migrationBuilder.InsertData(
                schema: "recipe",
                table: "Instructions",
                columns: new[] { "Id", "Description", "RecipeId", "StepNumber" },
                values: new object[,]
                {
                    { 1, "Preheat oven to 350 degrees", 1, 1 },
                    { 2, "Season chicken with salt and pepper", 1, 2 },
                    { 3, "Heat olive oil in a large skillet over medium heat", 1, 3 },
                    { 4, "Cook chicken until golden brown", 1, 4 },
                    { 5, "Preheat oven to 350 degrees", 2, 1 },
                    { 6, "Season chicken with salt and pepper", 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                schema: "recipe",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeId",
                schema: "recipe",
                table: "Instructions",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "Instructions",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "Recipes",
                schema: "recipe");
        }
    }
}
