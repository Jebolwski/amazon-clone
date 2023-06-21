using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class table1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Product_Productid",
                schema: "AmazonClone",
                table: "Cart");

            migrationBuilder.DropTable(
                name: "ProductProductCategory",
                schema: "AmazonClone");

            migrationBuilder.DropIndex(
                name: "IX_Cart_Productid",
                schema: "AmazonClone",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Productid",
                schema: "AmazonClone",
                table: "Cart");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductCategoryid",
                schema: "AmazonClone",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryid",
                schema: "AmazonClone",
                table: "Product",
                column: "ProductCategoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryid",
                schema: "AmazonClone",
                table: "Product",
                column: "ProductCategoryid",
                principalSchema: "AmazonClone",
                principalTable: "ProductCategory",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryid",
                schema: "AmazonClone",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductCategoryid",
                schema: "AmazonClone",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductCategoryid",
                schema: "AmazonClone",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "Productid",
                schema: "AmazonClone",
                table: "Cart",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductProductCategory",
                schema: "AmazonClone",
                columns: table => new
                {
                    productCategoriesid = table.Column<Guid>(type: "uuid", nullable: false),
                    productsid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductCategory", x => new { x.productCategoriesid, x.productsid });
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_ProductCategory_productCategoriesid",
                        column: x => x.productCategoriesid,
                        principalSchema: "AmazonClone",
                        principalTable: "ProductCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_Product_productsid",
                        column: x => x.productsid,
                        principalSchema: "AmazonClone",
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Productid",
                schema: "AmazonClone",
                table: "Cart",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductCategory_productsid",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                column: "productsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Product_Productid",
                schema: "AmazonClone",
                table: "Cart",
                column: "Productid",
                principalSchema: "AmazonClone",
                principalTable: "Product",
                principalColumn: "id");
        }
    }
}
