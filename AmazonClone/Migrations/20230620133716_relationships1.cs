using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class relationships1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentPhoto_Comment_commentid",
                schema: "AmazonClone",
                table: "CommentPhoto");

            migrationBuilder.RenameColumn(
                name: "commentid",
                schema: "AmazonClone",
                table: "CommentPhoto",
                newName: "commentId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentPhoto_commentid",
                schema: "AmazonClone",
                table: "CommentPhoto",
                newName: "IX_CommentPhoto_commentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentPhoto_Comment_commentId",
                schema: "AmazonClone",
                table: "CommentPhoto",
                column: "commentId",
                principalSchema: "AmazonClone",
                principalTable: "Comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentPhoto_Comment_commentId",
                schema: "AmazonClone",
                table: "CommentPhoto");

            migrationBuilder.RenameColumn(
                name: "commentId",
                schema: "AmazonClone",
                table: "CommentPhoto",
                newName: "commentid");

            migrationBuilder.RenameIndex(
                name: "IX_CommentPhoto_commentId",
                schema: "AmazonClone",
                table: "CommentPhoto",
                newName: "IX_CommentPhoto_commentid");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentPhoto_Comment_commentid",
                schema: "AmazonClone",
                table: "CommentPhoto",
                column: "commentid",
                principalSchema: "AmazonClone",
                principalTable: "Comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
