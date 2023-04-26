using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAndCategories.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoryProductsProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoriesCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoryProductsProductId, x.ProductCategoriesCategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_ProductCategoriesCategoryId",
                        column: x => x.ProductCategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_CategoryProductsProductId",
                        column: x => x.CategoryProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductCategoriesCategoryId",
                table: "CategoryProduct",
                column: "ProductCategoriesCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");
        }
    }
}
