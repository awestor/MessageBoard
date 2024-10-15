using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderBoard.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class add_file_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OrderPrice",
                table: "OrderItem",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EntUser_Email",
                table: "EntUser",
                column: "Email");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EntUser_Login",
                table: "EntUser",
                column: "Login");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EntUser_Password",
                table: "EntUser",
                column: "Password");

            migrationBuilder.CreateTable(
                name: "FileContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: false),
                    ContentType = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContent", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileContent");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EntUser_Email",
                table: "EntUser");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EntUser_Login",
                table: "EntUser");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EntUser_Password",
                table: "EntUser");

            migrationBuilder.AlterColumn<decimal>(
                name: "OrderPrice",
                table: "OrderItem",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
