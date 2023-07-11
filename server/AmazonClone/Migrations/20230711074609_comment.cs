using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stars",
                schema: "AmazonClone",
                table: "Comment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "title",
                schema: "AmazonClone",
                table: "Comment",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stars",
                schema: "AmazonClone",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "title",
                schema: "AmazonClone",
                table: "Comment");
        }
    }
}
