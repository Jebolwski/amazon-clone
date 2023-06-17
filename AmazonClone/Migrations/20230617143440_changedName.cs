using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class changedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_cart_cartId",
                schema: "ShoppingList",
                table: "product");

            migrationBuilder.EnsureSchema(
                name: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "ShoppingList",
                newName: "user",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "role",
                schema: "ShoppingList",
                newName: "role",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "ProductProductCategory",
                schema: "ShoppingList",
                newName: "ProductProductCategory",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "product_photo",
                schema: "ShoppingList",
                newName: "product_photo",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "product_category",
                schema: "ShoppingList",
                newName: "product_category",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "product",
                schema: "ShoppingList",
                newName: "product",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "comment_photo",
                schema: "ShoppingList",
                newName: "comment_photo",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "comment",
                schema: "ShoppingList",
                newName: "comment",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "cart",
                schema: "ShoppingList",
                newName: "cart",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "ShoppingList",
                newName: "AspNetUserTokens",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "ShoppingList",
                newName: "AspNetUsers",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "ShoppingList",
                newName: "AspNetUserRoles",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "ShoppingList",
                newName: "AspNetUserLogins",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "ShoppingList",
                newName: "AspNetUserClaims",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "ShoppingList",
                newName: "AspNetRoles",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "ShoppingList",
                newName: "AspNetRoleClaims",
                newSchema: "AmazonClone");

            migrationBuilder.RenameColumn(
                name: "cartId",
                schema: "AmazonClone",
                table: "product",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_product_cartId",
                schema: "AmazonClone",
                table: "product",
                newName: "IX_product_CartId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                schema: "AmazonClone",
                table: "product",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_product_cart_CartId",
                schema: "AmazonClone",
                table: "product",
                column: "CartId",
                principalSchema: "AmazonClone",
                principalTable: "cart",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_cart_CartId",
                schema: "AmazonClone",
                table: "product");

            migrationBuilder.EnsureSchema(
                name: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "AmazonClone",
                newName: "user",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "role",
                schema: "AmazonClone",
                newName: "role",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "ProductProductCategory",
                schema: "AmazonClone",
                newName: "ProductProductCategory",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "product_photo",
                schema: "AmazonClone",
                newName: "product_photo",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "product_category",
                schema: "AmazonClone",
                newName: "product_category",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "product",
                schema: "AmazonClone",
                newName: "product",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "comment_photo",
                schema: "AmazonClone",
                newName: "comment_photo",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "comment",
                schema: "AmazonClone",
                newName: "comment",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "cart",
                schema: "AmazonClone",
                newName: "cart",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "AmazonClone",
                newName: "AspNetUserTokens",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "AmazonClone",
                newName: "AspNetUsers",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "AmazonClone",
                newName: "AspNetUserRoles",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "AmazonClone",
                newName: "AspNetUserLogins",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "AmazonClone",
                newName: "AspNetUserClaims",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "AmazonClone",
                newName: "AspNetRoles",
                newSchema: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "AmazonClone",
                newName: "AspNetRoleClaims",
                newSchema: "ShoppingList");

            migrationBuilder.RenameColumn(
                name: "CartId",
                schema: "ShoppingList",
                table: "product",
                newName: "cartId");

            migrationBuilder.RenameIndex(
                name: "IX_product_CartId",
                schema: "ShoppingList",
                table: "product",
                newName: "IX_product_cartId");

            migrationBuilder.AlterColumn<Guid>(
                name: "cartId",
                schema: "ShoppingList",
                table: "product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_product_cart_cartId",
                schema: "ShoppingList",
                table: "product",
                column: "cartId",
                principalSchema: "ShoppingList",
                principalTable: "cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
