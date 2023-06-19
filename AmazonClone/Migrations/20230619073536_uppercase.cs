using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class uppercase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_cart_cartsId",
                schema: "AmazonClone",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_product_productsId",
                schema: "AmazonClone",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_comment_photo_comment_commentId",
                schema: "AmazonClone",
                table: "comment_photo");

            migrationBuilder.DropForeignKey(
                name: "FK_product_photo_product_productId",
                schema: "AmazonClone",
                table: "product_photo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_product_category_productCategoriesId",
                schema: "AmazonClone",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_product_productsId",
                schema: "AmazonClone",
                table: "ProductProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                schema: "AmazonClone",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role",
                schema: "AmazonClone",
                table: "role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product",
                schema: "AmazonClone",
                table: "product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comment",
                schema: "AmazonClone",
                table: "comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart",
                schema: "AmazonClone",
                table: "cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_photo",
                schema: "AmazonClone",
                table: "product_photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_category",
                schema: "AmazonClone",
                table: "product_category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comment_photo",
                schema: "AmazonClone",
                table: "comment_photo");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "AmazonClone",
                newName: "User",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "role",
                schema: "AmazonClone",
                newName: "Role",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "product",
                schema: "AmazonClone",
                newName: "Product",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "comment",
                schema: "AmazonClone",
                newName: "Comment",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "cart",
                schema: "AmazonClone",
                newName: "Cart",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "product_photo",
                schema: "AmazonClone",
                newName: "ProductPhoto",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "product_category",
                schema: "AmazonClone",
                newName: "ProductCategory",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "comment_photo",
                schema: "AmazonClone",
                newName: "CommentPhoto",
                newSchema: "AmazonClone");

            migrationBuilder.RenameColumn(
                name: "productsId",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                newName: "productsid");

            migrationBuilder.RenameColumn(
                name: "productCategoriesId",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                newName: "productCategoriesid");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductCategory_productsId",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                newName: "IX_ProductProductCategory_productsid");

            migrationBuilder.RenameColumn(
                name: "productsId",
                schema: "AmazonClone",
                table: "CartProduct",
                newName: "productsid");

            migrationBuilder.RenameColumn(
                name: "cartsId",
                schema: "AmazonClone",
                table: "CartProduct",
                newName: "cartsid");

            migrationBuilder.RenameIndex(
                name: "IX_CartProduct_productsId",
                schema: "AmazonClone",
                table: "CartProduct",
                newName: "IX_CartProduct_productsid");

            migrationBuilder.RenameIndex(
                name: "IX_product_photo_productId",
                schema: "AmazonClone",
                table: "ProductPhoto",
                newName: "IX_ProductPhoto_productId");

            migrationBuilder.RenameColumn(
                name: "commentId",
                schema: "AmazonClone",
                table: "CommentPhoto",
                newName: "commentid");

            migrationBuilder.RenameIndex(
                name: "IX_comment_photo_commentId",
                schema: "AmazonClone",
                table: "CommentPhoto",
                newName: "IX_CommentPhoto_commentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "AmazonClone",
                table: "User",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "AmazonClone",
                table: "Role",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                schema: "AmazonClone",
                table: "Product",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                schema: "AmazonClone",
                table: "Comment",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                schema: "AmazonClone",
                table: "Cart",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPhoto",
                schema: "AmazonClone",
                table: "ProductPhoto",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                schema: "AmazonClone",
                table: "ProductCategory",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentPhoto",
                schema: "AmazonClone",
                table: "CommentPhoto",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Cart_cartsid",
                schema: "AmazonClone",
                table: "CartProduct",
                column: "cartsid",
                principalSchema: "AmazonClone",
                principalTable: "Cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Product_productsid",
                schema: "AmazonClone",
                table: "CartProduct",
                column: "productsid",
                principalSchema: "AmazonClone",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentPhoto_Comment_commentid",
                schema: "AmazonClone",
                table: "CommentPhoto",
                column: "commentid",
                principalSchema: "AmazonClone",
                principalTable: "Comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhoto_Product_productId",
                schema: "AmazonClone",
                table: "ProductPhoto",
                column: "productId",
                principalSchema: "AmazonClone",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_ProductCategory_productCategoriesid",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                column: "productCategoriesid",
                principalSchema: "AmazonClone",
                principalTable: "ProductCategory",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_Product_productsid",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                column: "productsid",
                principalSchema: "AmazonClone",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Cart_cartsid",
                schema: "AmazonClone",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Product_productsid",
                schema: "AmazonClone",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentPhoto_Comment_commentid",
                schema: "AmazonClone",
                table: "CommentPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhoto_Product_productId",
                schema: "AmazonClone",
                table: "ProductPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_ProductCategory_productCategoriesid",
                schema: "AmazonClone",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_Product_productsid",
                schema: "AmazonClone",
                table: "ProductProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "AmazonClone",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "AmazonClone",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                schema: "AmazonClone",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                schema: "AmazonClone",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                schema: "AmazonClone",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPhoto",
                schema: "AmazonClone",
                table: "ProductPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                schema: "AmazonClone",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentPhoto",
                schema: "AmazonClone",
                table: "CommentPhoto");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "AmazonClone",
                newName: "user",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "AmazonClone",
                newName: "role",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "AmazonClone",
                newName: "product",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "Comment",
                schema: "AmazonClone",
                newName: "comment",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "Cart",
                schema: "AmazonClone",
                newName: "cart",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "ProductPhoto",
                schema: "AmazonClone",
                newName: "product_photo",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "ProductCategory",
                schema: "AmazonClone",
                newName: "product_category",
                newSchema: "AmazonClone");

            migrationBuilder.RenameTable(
                name: "CommentPhoto",
                schema: "AmazonClone",
                newName: "comment_photo",
                newSchema: "AmazonClone");

            migrationBuilder.RenameColumn(
                name: "productsid",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                newName: "productsId");

            migrationBuilder.RenameColumn(
                name: "productCategoriesid",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                newName: "productCategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductCategory_productsid",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                newName: "IX_ProductProductCategory_productsId");

            migrationBuilder.RenameColumn(
                name: "productsid",
                schema: "AmazonClone",
                table: "CartProduct",
                newName: "productsId");

            migrationBuilder.RenameColumn(
                name: "cartsid",
                schema: "AmazonClone",
                table: "CartProduct",
                newName: "cartsId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProduct_productsid",
                schema: "AmazonClone",
                table: "CartProduct",
                newName: "IX_CartProduct_productsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPhoto_productId",
                schema: "AmazonClone",
                table: "product_photo",
                newName: "IX_product_photo_productId");

            migrationBuilder.RenameColumn(
                name: "commentid",
                schema: "AmazonClone",
                table: "comment_photo",
                newName: "commentId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentPhoto_commentid",
                schema: "AmazonClone",
                table: "comment_photo",
                newName: "IX_comment_photo_commentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                schema: "AmazonClone",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role",
                schema: "AmazonClone",
                table: "role",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product",
                schema: "AmazonClone",
                table: "product",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment",
                schema: "AmazonClone",
                table: "comment",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart",
                schema: "AmazonClone",
                table: "cart",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_photo",
                schema: "AmazonClone",
                table: "product_photo",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_category",
                schema: "AmazonClone",
                table: "product_category",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment_photo",
                schema: "AmazonClone",
                table: "comment_photo",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_cart_cartsId",
                schema: "AmazonClone",
                table: "CartProduct",
                column: "cartsId",
                principalSchema: "AmazonClone",
                principalTable: "cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_product_productsId",
                schema: "AmazonClone",
                table: "CartProduct",
                column: "productsId",
                principalSchema: "AmazonClone",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_product_category_productCategoriesId",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                column: "productCategoriesId",
                principalSchema: "AmazonClone",
                principalTable: "product_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_product_productsId",
                schema: "AmazonClone",
                table: "ProductProductCategory",
                column: "productsId",
                principalSchema: "AmazonClone",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
