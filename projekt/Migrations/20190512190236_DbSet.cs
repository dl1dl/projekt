using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt.Migrations
{
    public partial class DbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipe_Recipes_RecipeID",
                table: "FavoriteRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipe_AspNetUsers_UserId",
                table: "FavoriteRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipe",
                table: "FavoriteRecipe");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipe",
                newName: "FavoriteRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipe_UserId",
                table: "FavoriteRecipes",
                newName: "IX_FavoriteRecipes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipe_RecipeID",
                table: "FavoriteRecipes",
                newName: "IX_FavoriteRecipes_RecipeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipes",
                table: "FavoriteRecipes",
                column: "FavoriteRecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeID",
                table: "FavoriteRecipes",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_AspNetUsers_UserId",
                table: "FavoriteRecipes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeID",
                table: "FavoriteRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_AspNetUsers_UserId",
                table: "FavoriteRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipes",
                table: "FavoriteRecipes");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipes",
                newName: "FavoriteRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipes_UserId",
                table: "FavoriteRecipe",
                newName: "IX_FavoriteRecipe_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipes_RecipeID",
                table: "FavoriteRecipe",
                newName: "IX_FavoriteRecipe_RecipeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipe",
                table: "FavoriteRecipe",
                column: "FavoriteRecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipe_Recipes_RecipeID",
                table: "FavoriteRecipe",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipe_AspNetUsers_UserId",
                table: "FavoriteRecipe",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
