using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class tablechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productPrice",
                table: "Product",
                newName: "ProductPrice");

            migrationBuilder.RenameColumn(
                name: "ProductImage",
                table: "Product",
                newName: "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "Product",
                newName: "productPrice");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Product",
                newName: "ProductImage");
        }
    }
}
