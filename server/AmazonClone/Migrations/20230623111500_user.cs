using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                schema: "AmazonClone",
                table: "User",
                newName: "RefreshToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                schema: "AmazonClone",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                schema: "AmazonClone",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                schema: "AmazonClone",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                schema: "AmazonClone",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenCreated",
                schema: "AmazonClone",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                schema: "AmazonClone",
                table: "User");

            migrationBuilder.DropColumn(
                name: "passwordHash",
                schema: "AmazonClone",
                table: "User");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                schema: "AmazonClone",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                schema: "AmazonClone",
                table: "User",
                newName: "password");
        }
    }
}
