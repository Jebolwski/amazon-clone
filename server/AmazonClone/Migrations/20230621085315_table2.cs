using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class table2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategoryProduct",
                schema: "AmazonClone",
                table: "ProductCategoryProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCart",
                schema: "AmazonClone",
                table: "ProductCart");

            migrationBuilder.RenameTable(
                name: "ProductCategoryProduct",
                schema: "AmazonClone",
                newName: "ProductProductCategory",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "ProductCart",
                schema: "AmazonClone",
                newName: "CartProduct",
                newSchema: "AmazonClone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductCategory",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartProduct",
                schema: "AmazonClone",
                table: "CartProduct",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductCategory",
                schema: "AmazonClone",
                table: "ProductProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartProduct",
                schema: "AmazonClone",
                table: "CartProduct");

            migrationBuilder.RenameTable(
                name: "ProductProductCategory",
                schema: "AmazonClone",
                newName: "ProductCategoryProduct",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "CartProduct",
                schema: "AmazonClone",
                newName: "ProductCart",
                newSchema: "AmazonClone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategoryProduct",
                schema: "AmazonClone",
                table: "ProductCategoryProduct",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCart",
                schema: "AmazonClone",
                table: "ProductCart",
                column: "id");
        }
    }
}
