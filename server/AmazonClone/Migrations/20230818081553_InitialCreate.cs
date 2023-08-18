using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bougth",
                schema: "AmazonClone",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timeBought = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bougth", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BougthProduct",
                schema: "AmazonClone",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    boughtId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BougthProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_BougthProduct_Bougth_boughtId",
                        column: x => x.boughtId,
                        principalSchema: "AmazonClone",
                        principalTable: "Bougth",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BougthProduct_boughtId",
                schema: "AmazonClone",
                table: "BougthProduct",
                column: "boughtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BougthProduct",
                schema: "AmazonClone");

            migrationBuilder.DropTable(
                name: "Bougth",
                schema: "AmazonClone");
        }
    }
}
