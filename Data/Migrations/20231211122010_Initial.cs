using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp2._1._7.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdateUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PostAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Word_AspNetUsers_PostUserId",
                        column: x => x.PostUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Word_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Word_PostUserId",
                table: "Word",
                column: "PostUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Word_UpdateUserId",
                table: "Word",
                column: "UpdateUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
