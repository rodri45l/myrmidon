using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyrmidonAPI.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUserMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ip_address",
                table: "session_tokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ip_address",
                table: "session_tokens",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
