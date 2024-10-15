using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderBoard.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class rename_type_phoneNamber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "EntUser",
                type: "character varying(48)",
                maxLength: 48,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 48);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "EntUser",
                type: "integer",
                maxLength: 48,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(48)",
                oldMaxLength: 48);
        }
    }
}
