using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderBoard.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class UserConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EntUser_Email",
                table: "EntUser",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_EntUser_Login",
                table: "EntUser",
                column: "Login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntUser_Email",
                table: "EntUser");

            migrationBuilder.DropIndex(
                name: "IX_EntUser_Login",
                table: "EntUser");
        }
    }
}
