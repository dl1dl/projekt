using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt.Migrations
{
    public partial class Tagging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taggings_Tag_TagID",
                table: "Taggings");

            migrationBuilder.DropIndex(
                name: "IX_Taggings_TagID",
                table: "Taggings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Taggings_TagID",
                table: "Taggings",
                column: "TagID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Taggings_Tags_TagID",
                table: "Taggings",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "TagID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taggings_Tags_TagID",
                table: "Taggings");

            migrationBuilder.DropIndex(
                name: "IX_Taggings_TagID",
                table: "Taggings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Taggings_TagID",
                table: "Taggings",
                column: "TagID");

            migrationBuilder.AddForeignKey(
                name: "FK_Taggings_Tag_TagID",
                table: "Taggings",
                column: "TagID",
                principalTable: "Tag",
                principalColumn: "TagID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
