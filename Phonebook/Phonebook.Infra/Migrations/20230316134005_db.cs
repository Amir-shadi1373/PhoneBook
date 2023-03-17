using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.Infra.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Person",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "Nvarchar(200)", nullable: false),
                    LastName = table.Column<string>(type: "Nvarchar(200)", nullable: true),
                    NikName = table.Column<string>(type: "Nvarchar(200)", nullable: true),
                    Company = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Notes = table.Column<string>(type: "Nvarchar(200)", nullable: true),
                    UserPicture = table.Column<string>(type: "Nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "Nvarchar(500)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Addresses_tbl_Person_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Email",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Email_tbl_Person_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Phone",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "varchar(13)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Phone_tbl_Person_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SignificantDate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SignificantDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_SignificantDate_tbl_Person_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Addresses_UserId",
                table: "tbl_Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Email_UserId",
                table: "tbl_Email",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Phone_UserId",
                table: "tbl_Phone",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SignificantDate_UserId",
                table: "tbl_SignificantDate",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Addresses");

            migrationBuilder.DropTable(
                name: "tbl_Email");

            migrationBuilder.DropTable(
                name: "tbl_Phone");

            migrationBuilder.DropTable(
                name: "tbl_SignificantDate");

            migrationBuilder.DropTable(
                name: "tbl_Person");
        }
    }
}
