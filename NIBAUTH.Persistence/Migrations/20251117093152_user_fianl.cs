using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NIBAUTH.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class user_fianl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFinals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Fullname = table.Column<string>(type: "text", nullable: true),
                    Pinfl = table.Column<string>(type: "text", nullable: false),
                    WeaponType = table.Column<string>(type: "text", nullable: true),
                    Distance = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<int>(type: "integer", nullable: true),
                    Test = table.Column<string>(type: "text", nullable: true),
                    Result = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFinals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFinals_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFinals_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFinals_CreatedById",
                table: "UserFinals",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserFinals_UpdatedById",
                table: "UserFinals",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFinals");
        }
    }
}
