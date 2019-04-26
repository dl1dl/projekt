using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt.Migrations
{
    public partial class ModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_WebAppUser_AuthId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AuthId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "DiffLevelId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Recipes",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Recipes",
                newName: "AuthorId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevelID",
                table: "Recipes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<string>(nullable: true),
                    RecipeID = table.Column<int>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_WebAppUser_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "WebAppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CategoryID",
                table: "Recipes",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_DifficultyLevelID",
                table: "Recipes",
                column: "DifficultyLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_RecipeID",
                table: "Comment",
                column: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_WebAppUser_AuthorId",
                table: "Recipes",
                column: "AuthorId",
                principalTable: "WebAppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Categories_CategoryID",
                table: "Recipes",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_DifficultyLevels_DifficultyLevelID",
                table: "Recipes",
                column: "DifficultyLevelID",
                principalTable: "DifficultyLevels",
                principalColumn: "DifficultyLevelID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_WebAppUser_AuthorId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Categories_CategoryID",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_DifficultyLevels_DifficultyLevelID",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CategoryID",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_DifficultyLevelID",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "DifficultyLevelID",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Recipes",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Recipes",
                newName: "AuthorID");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthId",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiffLevelId",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AuthId",
                table: "Recipes",
                column: "AuthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_WebAppUser_AuthId",
                table: "Recipes",
                column: "AuthId",
                principalTable: "WebAppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
