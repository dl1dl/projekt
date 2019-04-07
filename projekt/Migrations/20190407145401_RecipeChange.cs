using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projekt.Migrations
{
    public partial class RecipeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthId",
                table: "Recipes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WebAppUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebAppUser", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_WebAppUser_AuthId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "WebAppUser");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AuthId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Recipes");
        }
    }
}
