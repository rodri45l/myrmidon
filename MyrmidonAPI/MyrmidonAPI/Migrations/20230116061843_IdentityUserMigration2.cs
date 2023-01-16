using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyrmidonAPI.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUserMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "AppointmentUsers_ibfk_2",
                table: "AppointmentUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_User_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_User_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_User_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_User_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "Journal_entry_ibfk_1",
                table: "Journal_entry");

            migrationBuilder.DropForeignKey(
                name: "Mood_ibfk_1",
                table: "Mood");

            migrationBuilder.DropForeignKey(
                name: "Patient_ibfk_1",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "Tension_ibfk_1",
                table: "Tension");

            migrationBuilder.DropForeignKey(
                name: "Therapist_ibfk_1",
                table: "Therapist");

            migrationBuilder.DropTable(
                name: "session_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "User");

            migrationBuilder.DropIndex(
                name: "userId7",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Therapist",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "userId6",
                table: "Therapist",
                newName: "Id5");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Tension",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "userId5",
                table: "Tension",
                newName: "Id4");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Patient",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "userId3",
                table: "Patient",
                newName: "Id3");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Mood",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "userId2",
                table: "Mood",
                newName: "Id2");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Journal_entry",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "userId1",
                table: "Journal_entry",
                newName: "Id1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AppointmentUser",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "userId",
                table: "AppointmentUser",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUserLogins",
                keyColumn: "ProviderKey",
                keyValue: null,
                column: "ProviderKey",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.UpdateData(
                table: "AspNetUserLogins",
                keyColumn: "LoginProvider",
                keyValue: null,
                column: "LoginProvider",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "Id6",
                table: "User",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "AppointmentUsers_ibfk_2",
                table: "AppointmentUser",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_User_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_User_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_User_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_User_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Journal_entry_ibfk_1",
                table: "Journal_entry",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Mood_ibfk_1",
                table: "Mood",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Patient_ibfk_1",
                table: "Patient",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Tension_ibfk_1",
                table: "Tension",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Therapist_ibfk_1",
                table: "Therapist",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "AppointmentUsers_ibfk_2",
                table: "AppointmentUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_User_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_User_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_User_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_User_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "Journal_entry_ibfk_1",
                table: "Journal_entry");

            migrationBuilder.DropForeignKey(
                name: "Mood_ibfk_1",
                table: "Mood");

            migrationBuilder.DropForeignKey(
                name: "Patient_ibfk_1",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "Tension_ibfk_1",
                table: "Tension");

            migrationBuilder.DropForeignKey(
                name: "Therapist_ibfk_1",
                table: "Therapist");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "User");

            migrationBuilder.DropIndex(
                name: "Id6",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Therapist",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "Id5",
                table: "Therapist",
                newName: "userId6");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tension",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "Id4",
                table: "Tension",
                newName: "userId5");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patient",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "Id3",
                table: "Patient",
                newName: "userId3");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Mood",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "Id2",
                table: "Mood",
                newName: "userId2");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Journal_entry",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "Id1",
                table: "Journal_entry",
                newName: "userId1");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AppointmentUser",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "Id",
                table: "AppointmentUser",
                newName: "userId");

            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "User",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "User",
                column: "userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                column: "UserId");

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
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "userId7",
                table: "User",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "token_id",
                table: "session_tokens",
                column: "token_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "userId4",
                table: "session_tokens",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "AppointmentUsers_ibfk_2",
                table: "AppointmentUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_User_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_User_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_User_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_User_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Journal_entry_ibfk_1",
                table: "Journal_entry",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "Mood_ibfk_1",
                table: "Mood",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "Patient_ibfk_1",
                table: "Patient",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Tension_ibfk_1",
                table: "Tension",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "Therapist_ibfk_1",
                table: "Therapist",
                column: "userId",
                principalTable: "User",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
