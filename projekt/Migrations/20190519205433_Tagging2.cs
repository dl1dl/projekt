using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt.Migrations
{
    public partial class Tagging2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Taggings_TagID",
                table: "Taggings");

            migrationBuilder.CreateIndex(
                name: "IX_Taggings_TagID",
                table: "Taggings",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Taggings_TagID",
                table: "Taggings");

            migrationBuilder.CreateIndex(
                name: "IX_Taggings_TagID",
                table: "Taggings",
                column: "TagID",
                unique: true);
        }
    }
}
