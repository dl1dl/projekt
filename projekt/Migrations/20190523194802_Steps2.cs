using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt.Migrations
{
    public partial class Steps2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Recipes_RecipeID",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Step",
                table: "Step");

            migrationBuilder.RenameTable(
                name: "Step",
                newName: "Steps");

            migrationBuilder.RenameIndex(
                name: "IX_Step_RecipeID",
                table: "Steps",
                newName: "IX_Steps_RecipeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Steps",
                table: "Steps",
                column: "StepID");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Recipes_RecipeID",
                table: "Steps",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Recipes_RecipeID",
                table: "Steps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Steps",
                table: "Steps");

            migrationBuilder.RenameTable(
                name: "Steps",
                newName: "Step");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_RecipeID",
                table: "Step",
                newName: "IX_Step_RecipeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Step",
                table: "Step",
                column: "StepID");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Recipes_RecipeID",
                table: "Step",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
