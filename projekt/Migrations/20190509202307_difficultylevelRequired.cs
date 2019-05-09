using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt.Migrations
{
    public partial class difficultylevelRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_DifficultyLevels_DifficultyLevelID",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "DifficultyLevelID",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_DifficultyLevels_DifficultyLevelID",
                table: "Recipes",
                column: "DifficultyLevelID",
                principalTable: "DifficultyLevels",
                principalColumn: "DifficultyLevelID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_DifficultyLevels_DifficultyLevelID",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "DifficultyLevelID",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_DifficultyLevels_DifficultyLevelID",
                table: "Recipes",
                column: "DifficultyLevelID",
                principalTable: "DifficultyLevels",
                principalColumn: "DifficultyLevelID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
