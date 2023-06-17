using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class relationships_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_cart_CartId",
                schema: "AmazonClone",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_CartId",
                schema: "AmazonClone",
                table: "product");

            migrationBuilder.DropColumn(
                name: "CartId",
                schema: "AmazonClone",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "product_id",
                schema: "AmazonClone",
                table: "product_photo",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "comment_id",
                schema: "AmazonClone",
                table: "comment_photo",
                newName: "commentId");

            migrationBuilder.CreateTable(
                name: "CartProduct",
                schema: "AmazonClone",
                columns: table => new
                {
                    cartsId = table.Column<Guid>(type: "uuid", nullable: false),
                    productsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.cartsId, x.productsId });
                    table.ForeignKey(
                        name: "FK_CartProduct_cart_cartsId",
                        column: x => x.cartsId,
                        principalSchema: "AmazonClone",
                        principalTable: "cart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_product_productsId",
                        column: x => x.productsId,
                        principalSchema: "AmazonClone",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_photo_productId",
                schema: "AmazonClone",
                table: "product_photo",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_comment_photo_commentId",
                schema: "AmazonClone",
                table: "comment_photo",
                column: "commentId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_productsId",
                schema: "AmazonClone",
                table: "CartProduct",
                column: "productsId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_photo_comment_commentId",
                schema: "AmazonClone",
                table: "comment_photo",
                column: "commentId",
                principalSchema: "AmazonClone",
                principalTable: "comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_photo_product_productId",
                schema: "AmazonClone",
                table: "product_photo",
                column: "productId",
                principalSchema: "AmazonClone",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_photo_comment_commentId",
                schema: "AmazonClone",
                table: "comment_photo");

            migrationBuilder.DropForeignKey(
                name: "FK_product_photo_product_productId",
                schema: "AmazonClone",
                table: "product_photo");

            migrationBuilder.DropTable(
                name: "CartProduct",
                schema: "AmazonClone");

            migrationBuilder.DropIndex(
                name: "IX_product_photo_productId",
                schema: "AmazonClone",
                table: "product_photo");

            migrationBuilder.DropIndex(
                name: "IX_comment_photo_commentId",
                schema: "AmazonClone",
                table: "comment_photo");

            migrationBuilder.RenameColumn(
                name: "productId",
                schema: "AmazonClone",
                table: "product_photo",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "commentId",
                schema: "AmazonClone",
                table: "comment_photo",
                newName: "comment_id");

            migrationBuilder.AddColumn<Guid>(
                name: "CartId",
                schema: "AmazonClone",
                table: "product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_CartId",
                schema: "AmazonClone",
                table: "product",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_cart_CartId",
                schema: "AmazonClone",
                table: "product",
                column: "CartId",
                principalSchema: "AmazonClone",
                principalTable: "cart",
                principalColumn: "id");
        }
    }
}
