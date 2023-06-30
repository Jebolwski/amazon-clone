using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class table4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
