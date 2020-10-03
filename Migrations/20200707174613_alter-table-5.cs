using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class altertable5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Product_ProductId",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_ProductId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Brand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Brand",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ProductId",
                table: "Brand",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Product_ProductId",
                table: "Brand",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
