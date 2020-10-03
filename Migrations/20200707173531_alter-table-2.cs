using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class altertable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Brand_BrandId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_BrandId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Brand",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_CategoryId",
                table: "Brand",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Category_CategoryId",
                table: "Brand",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Category_CategoryId",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_CategoryId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Brand");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_BrandId",
                table: "Category",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Brand_BrandId",
                table: "Category",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
