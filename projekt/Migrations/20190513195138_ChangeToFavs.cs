using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt.Migrations
{
    public partial class ChangeToFavs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeID",
                table: "FavoriteRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_AspNetUsers_UserId",
                table: "FavoriteRecipes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoriteRecipes",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipes_UserId",
                table: "FavoriteRecipes",
                newName: "IX_FavoriteRecipes_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeID",
                table: "FavoriteRecipes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeID",
                table: "FavoriteRecipes",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_AspNetUsers_UserID",
                table: "FavoriteRecipes",
                column: "UserID",
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
                name: "FK_FavoriteRecipes_AspNetUsers_UserID",
                table: "FavoriteRecipes");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "FavoriteRecipes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipes_UserID",
                table: "FavoriteRecipes",
                newName: "IX_FavoriteRecipes_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeID",
                table: "FavoriteRecipes",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
