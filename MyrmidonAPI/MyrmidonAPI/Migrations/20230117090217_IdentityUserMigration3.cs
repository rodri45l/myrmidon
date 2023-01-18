using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyrmidonAPI.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUserMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "session_tokens",
                columns: table => new
                {
                    tokenid = table.Column<Guid>(name: "token_id", type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    expirationtime = table.Column<DateTime>(name: "expiration_time", type: "timestamp", nullable: false),
                    ipaddress = table.Column<string>(name: "ip_address", type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.tokenid);
                    table.ForeignKey(
                        name: "session_tokens_ibfk_1",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "token_id",
                table: "session_tokens",
                column: "token_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "userId",
                table: "session_tokens",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "session_tokens");
        }
    }
}
