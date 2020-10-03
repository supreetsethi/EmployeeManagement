using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class altertable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Brand",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrandProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandProduct_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrandProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ProductId",
                table: "Brand",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandProduct_BrandId",
                table: "BrandProduct",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandProduct_ProductId",
                table: "BrandProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Product_ProductId",
                table: "Brand",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Product_ProductId",
                table: "Brand");

            migrationBuilder.DropTable(
                name: "BrandProduct");

            migrationBuilder.DropIndex(
                name: "IX_Brand_ProductId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Brand");
        }
    }
}
