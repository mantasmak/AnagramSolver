using Microsoft.EntityFrameworkCore.Migrations;

namespace MainApp.EF.CodeFirst.Migrations
{
    public partial class AddDeleteConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words",
                table: "CachedWords");

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words",
                table: "CachedWords",
                column: "AnagramId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words",
                table: "CachedWords");

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words",
                table: "CachedWords",
                column: "AnagramId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
